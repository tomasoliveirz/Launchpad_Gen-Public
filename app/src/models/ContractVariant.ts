import { ContractType } from "./ContractType";
import { EntityWithNameAndDescription } from "./EntityWithNameAndDescription";

export type ContractVariant = EntityWithNameAndDescription & {
    contractType: ContractType};
