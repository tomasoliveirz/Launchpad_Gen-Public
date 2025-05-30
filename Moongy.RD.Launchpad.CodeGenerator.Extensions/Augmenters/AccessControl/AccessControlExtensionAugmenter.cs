using Moongy.RD.Launchpad.CodeGenerator.Core.Enums;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Modules;
using Moongy.RD.Launchpad.CodeGenerator.Core.Metamodels.Others;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.Ownable;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Models;
/*
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.Ownable;
using Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl.RoleBased;
*/

namespace Moongy.RD.Launchpad.CodeGenerator.Extensions.Augmenters.AccessControl;

public class AccessControlExtensionAugmenter : BaseExtensionAugmenter<AccessControlExtensionModel>
{
    public override IReadOnlyCollection<FeatureKind> Requires => Array.Empty<FeatureKind>();
    public override IReadOnlyCollection<FeatureKind> Provides => new[] { FeatureKind.AccessControl };

    public override void Augment(ContextMetamodel ctx, AccessControlExtensionModel model)
    {
        var mod = Main(ctx);

        ConfigureAccessControl(mod, model);

        if (model.HasRoles)
        {
            BuildRoleBasedAccessControl(mod, model);
        }
        else
        {
            BuildOwnableAccessControl(mod, model);
        }
    }

    private static void ConfigureAccessControl(ModuleDefinition mod, AccessControlExtensionModel model)
    {
        mod.AccessControl = new AccessControlDefinition
        {
            OwnerIdentifier = model.HasRoles ? null : model.Owner,
            Roles = model.HasRoles ? new List<string>(model.Roles ?? []) : []
        };
    }

    //                          ownable access control 

    private static void BuildOwnableAccessControl(ModuleDefinition mod, AccessControlExtensionModel model)
    {
        BuildOwnableStorage(mod);
        BuildOwnableEvents(mod);
        BuildOwnableErrors(mod);
        BuildOwnableModifiers(mod);
        BuildOwnableFunctions(mod);
    }
    
    private static void BuildOwnableStorage(ModuleDefinition mod)
    {
        // address private _owner;
        AddOnce(mod.Fields, f => f.Name == "_owner", () => new FieldDefinition
        {
            Name = "_owner",
            Type = T(PrimitiveType.Address),
            Visibility = Visibility.Private
        });
    }

    private static void BuildOwnableErrors(ModuleDefinition mod)
    {
        return;
    }

    private static void BuildOwnableEvents(ModuleDefinition mod)
    {
        // event OwnershipTransferred(address indexed previousOwner, address indexed newOwner);
        AddOnce(mod.Triggers, e => e.Name == "OwnershipTransferred", () => new TriggerDefinition
        {
            Name = "OwnershipTransferred",
            Kind = TriggerKind.Log,
            Parameters =
            [
                new() { Name = "previousOwner", Type = T(PrimitiveType.Address) },
                new() { Name = "newOwner", Type = T(PrimitiveType.Address) }
            ]
        });
    }
    
    private static void BuildOwnableModifiers(ModuleDefinition mod)
    {
        // modifier onlyOwner()
        AddOnce(mod.Modifiers, m => m.Name == "onlyOwner", () => new ModifierDefinition
        {
            Name = "onlyOwner"
        });
    }

    private static void BuildOwnableFunctions(ModuleDefinition mod)
    {
        // function owner() public view virtual returns (address)
        AddOnce(mod.Functions, f => f.Name == "owner", () => new OwnerFunction().Build());
        // function _checkOwner() internal view virtual 
        AddOnce(mod.Functions, f => f.Name == "_checkOwner", () => new CheckOwnerFunction().Build());
        /*
        // function renounceOwnership() public virtual onlyOwner
        AddOnce(mod.Functions, f => f.Name == "renounceOwnership", () => new RenounceOwnershipFunction().Build());
        // function transferOwnership(address newOwner) public virtual onlyOwner
        AddOnce(mod.Functions, f => f.Name == "transferOwnership", () => new TransferOwnershipFunction().Build());
        // function _transferOwnership(address newOwner) internal virtual
        AddOnce(mod.Functions, f => f.Name == "_transferOwnership", () => new InternalTransferOwnershipFunction().Build());
        */
    }
    
    
    
    //                          role-based access control
    private static void BuildRoleBasedAccessControl(ModuleDefinition mod, AccessControlExtensionModel model)
    {
        BuildRoleBasedStructs(mod);
        BuildRoleBasedStorage(mod);
        BuildRoleBasedEvents(mod);
        BuildRoleBasedModifiers(mod);
        BuildRoleBasedFunctions(mod);
        BuildRoleConstants(mod, model);
    }
    

    private static void BuildRoleBasedStructs(ModuleDefinition mod)
    {
        // struct RoleData { mapping(address account => bool) hasRole; bytes32 adminRole; }
        AddOnce(mod.Structs, s => s.Name == "RoleData", () => new StructDefinition
        {
            Name = "RoleData",
            Fields =
            [
                new FieldDefinition
                {
                    Name = "hasRole",
                    Type = new TypeReference
                    {
                        Kind = TypeReferenceKind.Mapping,
                        KeyType = T(PrimitiveType.Address),
                        ValueType = T(PrimitiveType.Bool)
                    },
                    Visibility = Visibility.Internal
                },
                new FieldDefinition
                {
                    Name = "adminRole",
                    Type = T(PrimitiveType.Bytes),
                    Visibility = Visibility.Internal
                }
            ]
        });
    }

    private static void BuildRoleBasedStorage(ModuleDefinition mod)
    {
        // mapping(bytes32 role => RoleData) private _roles;
        AddOnce(mod.Fields, f => f.Name == "_roles", () => new FieldDefinition
        {
            Name = "_roles",
            Type = new TypeReference
            {
                Kind = TypeReferenceKind.Mapping,
                KeyType = T(PrimitiveType.Bytes),
                ValueType = new TypeReference
                {
                    Kind = TypeReferenceKind.Custom,
                    TypeName = "RoleData"
                }
            },
            Visibility = Visibility.Private
        });
        // bytes32 public constant DEFAULT_ADMIN_ROLE = 0x00;
        AddOnce(mod.Fields, f => f.Name == "DEFAULT_ADMIN_ROLE", () => new FieldDefinition
        {
            Name = "DEFAULT_ADMIN_ROLE",
            Type = T(PrimitiveType.Bytes),
            Visibility = Visibility.Public,
            Value = "0x00"
        });
    }

    private static void BuildRoleBasedEvents(ModuleDefinition mod)
    {
        var events = new[]
        {
            new TriggerDefinition
            {
                Name = "RoleAdminChanged",
                Kind = TriggerKind.Log,
                Parameters =
                [
                    new() { Name = "role", Type = T(PrimitiveType.Bytes) },
                    new() { Name = "previousAdminRole", Type = T(PrimitiveType.Bytes) },
                    new() { Name = "adminRole", Type = T(PrimitiveType.Bytes) }
                ]
            },
            new TriggerDefinition
            {
                Name = "RoleGranted",
                Kind = TriggerKind.Log,
                Parameters =
                [
                    new() { Name = "role", Type = T(PrimitiveType.Bytes) },
                    new() { Name = "account", Type = T(PrimitiveType.Address) },
                    new() { Name = "msg.sender", Type = T(PrimitiveType.Address) }
                ]
            },
            new TriggerDefinition
            {
                Name = "RoleRevoked",
                Kind = TriggerKind.Log,
                Parameters =
                [
                    new() { Name = "role", Type = T(PrimitiveType.Bytes) },
                    new() { Name = "account", Type = T(PrimitiveType.Address) },
                    new() { Name = "msg.sender", Type = T(PrimitiveType.Address) }
                ]
            }
        };

        foreach (var evt in events)
        {
            AddOnce(mod.Triggers, e => e.Name == evt.Name, () => evt);
        }
    }

    private static void BuildRoleBasedModifiers(ModuleDefinition mod)
    {
        // modifier onlyRole(bytes32 role)
        AddOnce(mod.Modifiers, m => m.Name == "onlyRole", () => new ModifierDefinition
        {
            Name = "onlyRole",
            Arguments = [new ParameterDefinition { Name = "role", Type = T(PrimitiveType.Bytes) }]
        });
    }

    private static void BuildRoleBasedFunctions(ModuleDefinition mod)
    {
        /*
        // function hasRole(bytes32 role, address account) public view virtual returns (bool)
        AddOnce(mod.Functions, f => f.Name == "hasRole", () => new HasRoleFunction().Build());
        // function _checkRole(bytes32 role) internal view virtual
        AddOnce(mod.Functions, f => f.Name == "_checkRole", () => new CheckRoleFunction().Build());
        // function _checkRole(bytes32 role, address account) internal view virtual
        AddOnce(mod.Functions, f => f.Name == "_checkRoleAccount", () => new CheckRoleAccountFunction().Build());
        // function getRoleAdmin(bytes32 role) public view virtual returns (bytes32)
        AddOnce(mod.Functions, f => f.Name == "getRoleAdmin", () => new GetRoleAdminFunction().Build());
        // function grantRole(bytes32 role, address account) public virtual onlyRole(getRoleAdmin(role))
        AddOnce(mod.Functions, f => f.Name == "grantRole", () => new GrantRoleFunction().Build());
        // function revokeRole(bytes32 role, address account) public virtual onlyRole(getRoleAdmin(role))
        AddOnce(mod.Functions, f => f.Name == "revokeRole", () => new RevokeRoleFunction().Build());
        // function renounceRole(bytes32 role, address callerConfirmation) public virtual
        AddOnce(mod.Functions, f => f.Name == "renounceRole", () => new RenounceRoleFunction().Build());
        // function _setRoleAdmin(bytes32 role, bytes32 adminRole) internal virtual
        AddOnce(mod.Functions, f => f.Name == "_setRoleAdmin", () => new SetRoleAdminFunction().Build());
        // function _grantRole(bytes32 role, address account) internal virtual returns (bool)
        AddOnce(mod.Functions, f => f.Name == "_grantRole", () => new InternalGrantRoleFunction().Build());
        // function _revokeRole(bytes32 role, address account) internal virtual returns (bool)
        AddOnce(mod.Functions, f => f.Name == "_revokeRole", () => new InternalRevokeRoleFunction().Build());
        */
    }
    
    private static void BuildRoleConstants(ModuleDefinition mod, AccessControlExtensionModel model)
    {
        if (model.Roles?.Any() == true)
        {
            foreach (var role in model.Roles)
            {
                var roleName = $"{role.ToUpperInvariant()}_ROLE";
                AddOnce(mod.Fields, f => f.Name == roleName, () => new FieldDefinition
                {
                    Name = roleName,
                    Type = T(PrimitiveType.Bytes),
                    Visibility = Visibility.Public,
                    Value = $"keccak256(\"{role.ToUpperInvariant()}_ROLE\")"
                });
            }
        }
    }
}