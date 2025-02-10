import { EntityWithNameAndDescription } from "./EntityWithNameAndDescription"

export type PreviousGeneration = EntityWithNameAndDescription & {createdAt:Date, contractVariantUuid:string}