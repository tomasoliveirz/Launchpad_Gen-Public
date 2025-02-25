import { EntityWithNameAndDescription } from "./EntityWithNameAndDescription";

export type ContractVariant = EntityWithNameAndDescription & {
    contractType: {
        uuid: string
    }
};

export type ContractVariantWithTypeName = {
    contractVariant: ContractVariant;
    contractTypeName: string;
}
