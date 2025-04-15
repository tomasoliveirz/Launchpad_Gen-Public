import { CodePreview } from "@/components/launchpad/code-preview/code-preview";
import { LaunchpadSelect } from "@/components/launchpad/select/select";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { ContractType } from "@/models/ContractType";
import { ContractVariant } from "@/models/ContractVariant";
import { useEntity } from "@/services/launchpad/entityService";
import { namedEntityToListCollection, previousGenerationToListCollection } from "@/support/adapters";
import { contractVariantsData } from "@/test-data/contract-variants";
import { previousGenerationsData } from "@/test-data/previous-generations";
import { Flex, HStack, ListCollection, Show, Spacer, useDisclosure, VStack } from "@chakra-ui/react";
import axios from "axios";
import { useEffect, useState } from "react";
import { FaCode } from "react-icons/fa";
import { Option } from "../../components/reUIsables/ControlledInput/./radio-input"
import { FungibleTokenContract } from "@/components/launchpad/contract-template/fungible-token-contract";
import { CodeEditor } from "@/components/launchpad/contract-editor/contract-editor";
import { LaunchpadGaugeComponent } from "@/components/launchpad/gauge-chart/gauge-chart";
import { TokenWizardModal } from "@/components/launchpad/dialogs/token-wizard-modal";
import { TokenWizardResponse } from "@/models/TokenWizardResponse";

export default function () {
    const url = import.meta.env.VITE_API_URL

    //DATA
    const [contractsGenerated, setContractsGenerated] = useState([]);
    useEffect(() => {
        axios.get(`${url}/ContractGenerationResults`)
            .then((response) => {
                console.log(response.data)
                setContractsGenerated(response.data)
            })
            .catch((error) => console.error(error));
    }, []);

    const contractTypesApi = useEntity<ContractType>("ContractTypes");
    const contractTypesData = contractTypesApi.list().data as ContractType[] || [];

    //CONTROL
    const [contractType, setContractType] = useState<string>();
    const [contractVariant, setContractVariant] = useState<string>();
    const [previousGeneration, setPreviousGeneration] = useState<string>();

    const [contractVariants, setContractVariants] = useState<ContractVariant[]>(contractVariantsData);

    const contractTypesList: ListCollection = namedEntityToListCollection(contractTypesData)
    const contracVariantsList: ListCollection = namedEntityToListCollection(contractVariants);
    const previousGeneratedList: ListCollection = previousGenerationToListCollection(contractsGenerated);

    const [contractFeatureGroup, setContractFeatureGroup] = useState<{ label: string; value: string | undefined }[]>([]);

    const [weightValue, setWeightValue] = useState(0);

    const {
        open: isTokenWizardModalOpen,
        onOpen: onTokenWizardModalOpen,
        onClose: onTokenWizardModalClose,
    } = useDisclosure();

    const handleTokenWizardModalOpenChange = (open: boolean) =>
        open ? onTokenWizardModalOpen() : onTokenWizardModalClose();

    const [tokenWizardResponseTest, setTokenWizardResponseTest] = useState<TokenWizardResponse>();

    useEffect(() => {
        setTokenWizardResponseTest({
            token: "token",
            question: "question ?",
            possibleAnswers: ["awnser a", "answer b", "answer c"],
            previousResponses: []
        })
    }, [tokenWizardResponseTest]);

    useEffect(() => {
        if (contractType) {
            const contractTypeIdx = contractTypesData.findIndex(x => x.uuid == contractType);
            const contractTypeRecord = contractTypesData[contractTypeIdx];
            setContractFeatureGroup(prev => [
                ...prev.filter(item => item.label !== "Contract type"),
                { label: "Contract type", value: contractTypeRecord.name }
            ]);
        }
    }, [contractType]);

    useEffect(() => {
        if (contractVariant) {
            const contractVariantIdx = contractVariantsData.findIndex(x => x.uuid == contractVariant);
            const contractVariantRecord = contractVariantsData[contractVariantIdx];
            setContractFeatureGroup(prev => [
                ...prev.filter(item => item.label !== "Contract Variant"),
                { label: "Contract Variant", value: contractVariantRecord.name }
            ]);
        }
    }, [contractVariant]);

    useEffect(() => {
        if (previousGeneration) {
            const generationIdx = previousGenerationsData.findIndex(x => x.uuid == previousGeneration);
            if (generationIdx != -1) {
                const previousGenerationRecord = previousGenerationsData[generationIdx];
                setContractVariant(previousGenerationRecord.contractVariantUuid);
                const variantIdx = contractVariantsData.findIndex(x => x.uuid == previousGenerationRecord.contractVariantUuid);
                if (variantIdx != -1) {
                    const variantRecord = contractVariantsData[variantIdx];
                    setContractType(variantRecord.contractType.uuid);
                }
            }
        }
    }, [previousGeneration])

    useEffect(() => {
        if (contractVariant) {
            const idx = contractVariants.findIndex(x => x.uuid == contractVariant);
            if (idx != -1) {
                const contractVariantsRecord = contractVariants[idx];
                setContractType(contractVariantsRecord.contractType.uuid);
            }
        }
    }, [contractVariant])

    useEffect(() => {
        let variants: ContractVariant[] = [];
        contractVariantsData.forEach((x: ContractVariant) => {
            if (x.contractType.uuid == contractType) variants.push(x);
        });
        setContractVariants(variants);
    }, [contractType])


    const [name, setName] = useState<string>("")
    const [symbol, setSymbol] = useState<string>("")
    const [premint, setPremint] = useState<string>("")
    const [features, setFeatures] = useState<string>("")
    const [vote, setVote] = useState<string>("")
    const [access, setAccess] = useState<string>("")
    const [upgradeability, setUpgradeability] = useState<string>("")
    const [securityContact, setSecurityContact] = useState<string>("")
    const [license, setLicense] = useState<string>("")


    const [voteChecked, setVoteChecked] = useState(false);
    const [accessChecked, setAccessChecked] = useState(false);
    const [upgradeabilityChecked, setUpgradeabilityChecked] = useState(false);

    const [generatedCode, setGeneratedCode] = useState("");

    function onVoteCheck() {
        const newVal = !voteChecked;
        if (newVal) {
            setVote("BLOCK_NUMBER")
        }
        setVoteChecked(newVal);
    }

    function onAccessCheck() {
        const newValAccess = !accessChecked;
        if (newValAccess) {
            setAccess("OWNABLE")
        }
        setAccessChecked(newValAccess);
    }

    function onUpgradeabilityCheck() {
        const newValUpgradeability = !upgradeabilityChecked;
        if (newValUpgradeability) {
            setUpgradeability("TRANSPARENT")
        }
        setUpgradeabilityChecked(newValUpgradeability);
    }

    function onMintableCheck() {
        const isMintable = !features.includes("MINTABLE");
        if (isMintable && !accessChecked) {
            setAccess("OWNABLE");
            setAccessChecked(true);
        }
    }

    function onUupsCheck() {
        const isUups = !upgradeability.includes("UUPS");
        if (isUups && !accessChecked) {
            setAccess("OWNABLE");
            setAccessChecked(true);
        }
    }

    const handleUpgradeabilityChange = (value: string) => {
        setUpgradeability(value);
        onUupsCheck();
    }

    const handleFeaturesChange = (value: string) => {
        setFeatures(value);
        onMintableCheck();
    };

    const featuresList: Option[] = [
        { label: "Mintable", value: "MINTABLE", description: "Privileged accounts will be able to create more supply." },
        { label: "Burnable", value: "BURNABLE", description: "Token holders will be able to destroy their tokens." },
        { label: "Pausable", value: "PAUSABLE", description: "Privileged accounts will be able to pause the functionality marked as whenNotPaused. Useful for emergency response." },
        { label: "Permit", value: "PERMIT", description: "Without paying gas, token holders will be able to allow third parties to transfer from their account." },
        { label: "Flash Minting", value: "FLASH_MINTING", description: "Built-in flash loans. Lend tokens without requiring collateral as long as they're returned in the same transaction." },
    ];

    const votesList: Option[] = [
        { label: "Block Number", value: "BLOCK_NUMBER", description: "Uses voting durations expressed as block numbers." },
        { label: "Timestamp", value: "TIMESTAMP", description: "Uses voting durations expressed as timestamps." }
    ];

    const accessList: Option[] = [
        { label: "Ownable", value: "OWNABLE", description: "Simple mechanism with a single account authorized for all privileged actions." },
        { label: "Roles", value: "ROLES", description: "Flexible mechanism with a separate role for each privileged action. A role can have many authorized accounts." },
        { label: "Managed", value: "MANAGED", description: "Enables a central contract to define a policy that allows certain callers to access certain functions." }
    ];

    const upgradeabilityList: Option[] = [
        { label: "Transparent", value: "TRANSPARENT", description: "Uses more complex proxy with higher overhead, requires less changes in your contract. Can also be used with beacons." },
        { label: "UUPS", value: "UUPS", description: "Uses simpler proxy with less overhead, requires including extra code in your contract. Allows flexibility for authorizing upgrades." }
    ];

    /* const generateSolidityCode = (): string => {
        const erc20Import = (upgradeability.includes("TRANSPARENT") || upgradeability.includes("UUPS")) ? 'import {ERC20Upgradeable} from "@openzeppelin/contracts-upgradeable/token/ERC20/ERC20Upgradeable.sol";' : 'import {ERC20} from "@openzeppelin/contracts/token/ERC20/ERC20.sol";';
        const burnableImport = features.includes("BURNABLE") ? 'import {ERC20Burnable} from "@openzeppelin/contracts/token/ERC20/extensions/ERC20Burnable.sol";' : "";
        const pausableImport = features.includes("PAUSABLE") ? 'import {ERC20Pausable} from "@openzeppelin/contracts/token/ERC20/extensions/ERC20Pausable.sol";' : "";
        const permitImport = features.includes("PERMIT") ? 'import {ERC20Permit} from "@openzeppelin/contracts/token/ERC20/extensions/ERC20Permit.sol";' : "";
        const flashMintingImport = features.includes("FLASH_MINTING") ? 'import {ERC20FlashMint} from "@openzeppelin/contracts/token/ERC20/extensions/ERC20FlashMint.sol";' : "";
        const voteImport = vote ? (features.includes("PERMIT") ? "" : 'import {ERC20Permit} from "@openzeppelin/contracts/token/ERC20/extensions/ERC20Permit.sol";\n') +
            'import {ERC20Votes} from "@openzeppelin/contracts/token/ERC20/extensions/ERC20Votes.sol";' +
            '\nimport {Nonces} from "@openzeppelin/contracts/utils/Nonces.sol";' : "";
        const ownableImport = access.includes("OWNABLE") ? 'import {Ownable} from "@openzeppelin/contracts/access/Ownable.sol";' : "";
        const rolesImport = access.includes("ROLES") ? 'import {AccessControl} from "@openzeppelin/contracts/access/AccessControl.sol";' : "";
        const managedImport = access.includes("MANAGED") ? 'import {AccessManaged} from "@openzeppelin/contracts/access/manager/AccessManaged.sol";' : "";
        const trasparentImport = upgradeability.includes("TRANSPARENT") ? 'import {Initializable} from "@openzeppelin/contracts-upgradeable/proxy/utils/Initializable.sol";' : "";
        const imports = [
            burnableImport,
            pausableImport,
            permitImport,
            flashMintingImport,
            voteImport,
            ownableImport,
            rolesImport,
            managedImport,
            trasparentImport,
            "\n",
        ].filter(Boolean);

        const contractIs = [
            upgradeability.length > 0 ? "ERC20Upgradeable" : "ERC20",
            features.includes("BURNABLE") ? "ERC20Burnable" : "",
            features.includes("PAUSABLE") ? "ERC20Pausable" : "",
            features.includes("PERMIT") ? "ERC20Permit" : "",
            features.includes("FLASH_MINTING") ? "ERC20FlashMint" : "",
            vote.length > 0 ? "ERC20Votes" + (features.includes("PERMIT") ? "" : ", ERC20Permit") : "",
            access.includes("OWNABLE") ? "Ownable" : "",
            access.includes("ROLES") ? "AccessControl" : "",
            access.includes("MANAGED") ? "AccessManaged" : "",
            upgradeability.includes("TRANSPARENT") ? "Initializable, ERC20Upgradeable" : "",
            upgradeability.includes("UUPS") ? "ERC20Upgradeable" : "",
        ].filter(Boolean);

        const constants = [
            access.includes("ROLES") && features.includes("MINTABLE") ? 'bytes32 public constant MINTER_ROLE = keccak256("MINTER_ROLE");' : "",
            access.includes("ROLES") && features.includes("PAUSABLE") ? 'bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");' : "",
            access.includes("ROLES") && upgradeability.includes("UUPS") ? 'bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");' : "",
        ].filter(Boolean);

        const constructorParams = [
            premint > "0" ? "address recipient" : "",
            access.includes("OWNABLE") ? "address initialOwner" : "",
            access.includes("ROLES") ? "address defaultAdmin" : "",
            access.includes("ROLES") && features.includes("MINTABLE") ? "address minter" : "",
            access.includes("MANAGED") ? "address initialAuthority" : "",
        ].filter(Boolean);

        const methodsCalls = [
            premint > "0" ? `_mint(recipient, ${premint} * 10 ** decimals());` : "",
            features.includes("PAUSABLE") && access.includes("ROLES") ? "_grantRole(PAUSER_ROLE, pauser);" : "",
        ]

        const lines = [
            "pragma solidity ^0.8.22;",
            "",
        ];

        lines.push(erc20Import);
        lines.push(imports.join("\n"));
        lines.push(`contract ${name} is ${contractIs.join(", ")} {`)
        lines.push(constants.map(c => `\t${c}`).join("\n"));

        if (upgradeability.length > 0) {
            lines.push("constructor() {");
            lines.push("\t_disableInitializers();");
            lines.push("}");
        }

        lines.push(`\tconstructor(${constructorParams}) ERC20("${name}", "${symbol}") {`);

        if (premint > "0") {
            lines.push(`\t\t_mint(recipient, ${premint} * 10 ** decimals());`);
        }
        lines.push("\t}");

        if (features.includes("MINTABLE")) {
            lines.push("\n\tfunction mint(address to, uint256 amount) public onlyOwner {\n\t\t_mint(to, amount);\n\t}");
        }
        if (features.includes("BURNABLE")) {
            lines.push("\tfunction burn() public {}");
        }

        lines.push("}");

        return lines.join("\n");
    };

    useEffect(() => {
        const newCode = generateSolidityCode();
        setGeneratedCode(newCode);
    }, [name, symbol, premint, features, vote, access, upgradeability, securityContact, license]); */

    //VIEW
    return <PageWrapper title="Contract Generator" icon={FaCode}>
        <HStack mt="2em" w="100%">
            <Show when={previousGeneratedList.size > 0}>
                <LaunchpadSelect w="20%" collection={previousGeneratedList} title="Previous contracts" value={previousGeneration} onValueChange={setPreviousGeneration} />
            </Show>
            <Spacer maxW="100%" />
        </HStack>
        <HStack w="100%" mt="3em">
{/*             <LaunchpadSelect size="sm" w="20%" collection={contractTypesList} title="Contract Types" value={contractType} onValueChange={setContractType} />
 */}            <Show when={contractType}>
                <LaunchpadSelect size="sm" w="20%" collection={contracVariantsList} title="Contract variants" value={contractVariant} onValueChange={setContractVariant} />
            </Show>
            <Spacer />
            <VStack>
                <LaunchpadGaugeComponent width="15em" value={weightValue} arrowType="arrow" />
                <TokenWizardModal isOpen={isTokenWizardModalOpen} onOpenChange={handleTokenWizardModalOpenChange} tokenWizardResponse={tokenWizardResponseTest} />
            </VStack>
        </HStack>

        <HStack gap="8em" m="2rem">
            <FungibleTokenContract
                name={name}
                symbol={symbol}
                premint={premint}
                features={features}
                vote={vote}
                access={access}
                upgradeability={upgradeability}
                securityContact={securityContact}
                license={license}
                voteList={votesList}
                accessList={accessList}
                upgradeabilityList={upgradeabilityList}
                featuresList={featuresList}
                voteChecked={voteChecked}
                accessChecked={accessChecked}
                upgradeabilityChecked={upgradeabilityChecked}
                onNameChange={setName}
                onSymbolChange={setSymbol}
                onPremintChange={setPremint}
                onFeaturesChange={handleFeaturesChange}
                onVoteChange={setVote}
                onAccessChange={setAccess}
                onUpgradeabilityChange={handleUpgradeabilityChange}
                onSecurityContactChange={setSecurityContact}
                onLicenseChange={setLicense}
                onVoteCheck={onVoteCheck}
                onAccessCheck={onAccessCheck}
                onUpgradeabilityCheck={onUpgradeabilityCheck}
            />
            <CodeEditor code={generatedCode} readOnly={true} />
        </HStack>
        <Show when={contractVariant}>
            <Flex w="100%" gap="10em">
                <CodePreview mt="5em" />
            </Flex>
        </Show>
    </PageWrapper>
}

