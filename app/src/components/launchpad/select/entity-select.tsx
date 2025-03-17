import { SelectContent, SelectItem, SelectRoot, SelectTrigger, SelectValueText } from "@/components/ui/select";
import { ListCollection, SelectRootProps } from "@chakra-ui/react";
import { RefObject } from "react";

export interface GenericSelectProps<T> extends Omit<Omit<SelectRootProps, "onValueChange">, "value"> {
    collection: ListCollection<T>;
    ref?: RefObject<HTMLDivElement>;
    onValueChange: (a: string) => void;
    value: string | undefined;
    label?: string;
    getItemKey: (item: T) => string;
    getItemLabel: (item: T) => string;
}

export function GenericSelect<T>({ 
    collection, 
    ref, 
    onValueChange, 
    value, 
    label = "Select an option", 
    getItemKey, 
    getItemLabel
}: GenericSelectProps<T>) {
    const selectedItem = collection.items.find(item => getItemKey(item) === value);
    return (
        <SelectRoot collection={collection} size="sm" width="320px" onValueChange={(e) => onValueChange(e.value[0])} value={value ? [value] : []}>
            <SelectTrigger>
                <SelectValueText placeholder={selectedItem ? getItemLabel(selectedItem) : label} />
            </SelectTrigger>
            <SelectContent portalRef={ref}>
                {collection.items.map((item) => (
                    <SelectItem item={getItemKey(item)} key={getItemKey(item)}>
                        {getItemLabel(item)}
                    </SelectItem>
                ))}
            </SelectContent>
        </SelectRoot>
    );
}
