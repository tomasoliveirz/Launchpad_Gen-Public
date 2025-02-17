import { Entity } from "./Entity"

export type EntityWithNameAndDescription = Entity & {
    name?:string,
    description?:string
}

