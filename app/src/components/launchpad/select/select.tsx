import { SelectContent, SelectItem, SelectLabel, SelectRoot, SelectRootProps, SelectTrigger, SelectValueText} from "@chakra-ui/react";

export interface LaunchpadSelectProps extends Omit<Omit<SelectRootProps, "onValueChange">, "value">
{
  onValueChange:(a:string)=>void
  value:string|undefined
}

export function LaunchpadSelect({title, size, collection, onValueChange, value, ...props}:LaunchpadSelectProps)
{
    return <SelectRoot size={size??"xs"} collection={collection} onValueChange={(e)=>onValueChange(e.value[0])} value={value ? [value]:[]} {...props}>
    <SelectLabel>{title}</SelectLabel>
    <SelectTrigger>
      <SelectValueText placeholder={title} />
    </SelectTrigger>
    <SelectContent>
      {collection.items.map((item) => {
        return <SelectItem item={item} key={item.value}>
                    {item.label}
                </SelectItem>
        })}
    </SelectContent>
  </SelectRoot>
}
