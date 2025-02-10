import { PreviousGeneration } from "@/models/PreviousGeneration";
import { ListCollection, createListCollection } from "@chakra-ui/react";
import { formatDate } from "./dates";
import { EntityWithNameAndDescription } from "@/models/EntityWithNameAndDescription";

export function previousGenerationToListCollection(list:PreviousGeneration[]):ListCollection<any>
{
    let result:any[] = [];
    list.forEach((x:PreviousGeneration)=>{
        result.push({label:x.name+"("+formatDate(x.createdAt)+")",value:x.uuid})
    });
    return createListCollection({items:result});
}

export function namedEntityToListCollection(list:EntityWithNameAndDescription[]) : ListCollection
{
    let result:any[] = [];
    list.forEach((x:EntityWithNameAndDescription)=>{
        result.push({label:x.name, value:x.uuid});
    })
    return createListCollection({items:result});
}

