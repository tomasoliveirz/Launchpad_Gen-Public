import { EntityWithNameAndDescription } from "./EntityWithNameAndDescription";

export type ContractFeature = EntityWithNameAndDescription & {
    dataType: string
};