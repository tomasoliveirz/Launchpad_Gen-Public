import { HStack, RadioGroup, RadioGroupRootProps, Stack } from "@chakra-ui/react"
import { CiCircleQuestion } from "react-icons/ci"


export interface Option {
    label: string
    value: string
    description?: string
}


export interface RadioInputWithDescriptionProps extends Omit<Omit<RadioGroupRootProps, "value">, "onValueChange"> {
    options: Option[]
    value: string
    setValue: (s: string) => void
    direction?: "row" | "column" | "row-reverse" | "column-reverse"
}

export function RadioInputWithDescription({ options, value, direction, setValue, ...props }: RadioInputWithDescriptionProps) {
    return <RadioGroup.Root value={value} onValueChange={(e) => setValue(e.value)} {...props}>
        <Stack direction={direction}>
            {options.map((item) => (
                <RadioGroup.Item key={item.value} value={item.value}>
                    <RadioGroup.ItemHiddenInput />
                    <RadioGroup.ItemIndicator />
                    <RadioGroup.ItemText>{item.label}</RadioGroup.ItemText>
                    {item.description && (
                        <CiCircleQuestion title={item.description} />
                    )}
                </RadioGroup.Item>
            ))}
        </Stack>
    </RadioGroup.Root>
}