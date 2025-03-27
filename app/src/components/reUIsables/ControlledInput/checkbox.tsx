import { CheckboxProps } from "@/components/ui/checkbox";
import { Checkbox } from "@chakra-ui/react/checkbox";

export interface CheckboxInputProps extends CheckboxProps
{
    setCheck:(x:boolean)=>void
}

export default function CheckBox({setCheck, children, ...props}:CheckboxInputProps)
{
    return <Checkbox.Root  {...props} onCheckedChange={e => setCheck(!props.checked)}>
                <Checkbox.HiddenInput />
                <Checkbox.Control>
                <Checkbox.Indicator />
                </Checkbox.Control>
                <Checkbox.Label>{children}</Checkbox.Label>
            </Checkbox.Root>
}