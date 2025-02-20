import { SelectContent, SelectItem, SelectRoot, SelectTrigger, SelectValueText } from "@/components/ui/select";
import { ContractType } from "@/models/ContractType";
import { ListCollection, SelectRootProps } from "@chakra-ui/react";

export interface ContractTypeSelectProps extends Omit<Omit<SelectRootProps, "onValueChange">, "value"> {
    collection: ListCollection<ContractType>
    ref: React.RefObject<HTMLDivElement>
    onValueChange: (a: string) => void
    value: string | undefined
}
export function ContractTypeSelect({ collection, ref, onValueChange, value, ...props }: ContractTypeSelectProps) {
    const selectedContractType = collection.items.find(item => item.uuid === value);
    return (
        <SelectRoot collection={collection} size="sm" width="320px" onValueChange={(e) => onValueChange(e.value[0])} value={value ? [value] : []} {...props}>
            <SelectTrigger>
                <SelectValueText placeholder={selectedContractType ? selectedContractType.name : "Choose Contract Type"} />
            </SelectTrigger>
            <SelectContent portalRef={ref} >
                {collection.items.map((contractType) => (
                    <SelectItem item={contractType.uuid} key={contractType.uuid} >
                        {contractType.name}
                    </SelectItem>
                ))}
            </SelectContent>
        </SelectRoot>
    )
}