import { SelectContent, SelectItem, SelectRoot, SelectTrigger, SelectValueText } from "@/components/ui/select";
import { ListCollection, SelectRootProps } from "@chakra-ui/react";

export interface DialogSelectProps extends Omit<Omit<SelectRootProps, "onValueChange">, "value"> {
    collection: ListCollection<any>
    ref: React.RefObject<HTMLDivElement>
    onValueChange: (a: string) => void
    value: string | undefined
    label: string
}
export function DialogSelect({ collection, ref, onValueChange, value, label, ...props }: DialogSelectProps) {
    const selected = collection.items.find(item => item.uuid === value);
    console.log(collection.items);
    return (
        <SelectRoot collection={collection} size="sm" width="320px" onValueChange={(e) => onValueChange(e.value[0])} value={value ? [value] : []} {...props}>
            <SelectTrigger>
                <SelectValueText placeholder={selected ? selected.name : label} />
            </SelectTrigger>
            <SelectContent portalRef={ref} >
                {collection.items.map((entity) => (
                    <SelectItem item={entity.uuid} key={entity.uuid} >
                        {entity.name}
                    </SelectItem>
                ))}
            </SelectContent>
        </SelectRoot>
    )
}