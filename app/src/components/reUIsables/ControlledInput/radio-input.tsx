import { HStack, RadioGroup, RadioGroupRootProps, Stack } from "@chakra-ui/react"


export interface Option
{
    label:string
    value:string
}


export interface RadioInputProps extends Omit<Omit<RadioGroupRootProps, "value">,"onValueChange">
{
    options:Option[]
    value:string
    setValue:(s:string)=>void
    direction?:"row"|"column"|"row-reverse"|"column-reverse"
}

export function RadioInput({options,value, direction, setValue, ...props} : RadioInputProps)
{
    return <RadioGroup.Root value={value} onValueChange={(e) => setValue(e.value)} {...props}>
    <Stack direction={direction}>
      {options.map((item) => (
        <RadioGroup.Item key={item.value} value={item.value}>
          <RadioGroup.ItemHiddenInput />
          <RadioGroup.ItemIndicator />
          <RadioGroup.ItemText>{item.label}</RadioGroup.ItemText>
        </RadioGroup.Item>
      ))}
    </Stack>
  </RadioGroup.Root>
}