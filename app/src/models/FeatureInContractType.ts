import { ContractFeature } from "./ContractFeature"
import { ContractType } from "./ContractType"
import { Entity } from "./Entity"

export type FeatureInContractType = Entity & {
    contractFeature: ContractFeature,
    contractType: ContractType,
    DefaultValue: string
}