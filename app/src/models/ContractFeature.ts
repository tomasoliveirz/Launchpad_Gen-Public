import { EntityWithNameAndDescription } from "./EntityWithNameAndDescription";

export type ContractFeature = EntityWithNameAndDescription & {
    dataType: string,
    normalizedName: string,
    defaultValue: string
};