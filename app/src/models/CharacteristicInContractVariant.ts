import { ContractCharacteristic } from "./ContractCharacteristic";
import { ContractVariant } from "./ContractVariant";
import { Entity } from "./Entity";

export type CharacteristicInContractVariant = Entity & {
    contractCharacteristic: ContractCharacteristic,
    contractVariant: ContractVariant,
    Value: string
}