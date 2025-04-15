import { Box, InputProps, Stack } from "@chakra-ui/react"
import CheckBox from "./checkbox"
import { IntegerInput } from "./int-input"
import { GenericInput } from "./generic-input"
import { EmailInput } from "./email-input"
import { UrlInput } from "./url-input"
import { RadioInput, Option } from "./radio-input"
import { MultiSelectOption } from "./multi-option-input"


export interface MultitypeInputProps extends Omit<Omit<InputProps, "type">,"value">
{
    type:"integer"|"option"|"string"|"boolean"|"email"|"url"
    options?: Option[]
    optional?:boolean
    multiSelect?:boolean
    value?:string
    setValue:(s:string)=>void
    label?:string
}

export function MultitypeInput({type,value,setValue,options,multiSelect, ...props}:MultitypeInputProps)
{
    const numberValue:(s:string|undefined)=>number = (s:string|undefined)=> s && parseInt(s) || 0;

    return <>
        
        {type === "boolean" ? <CheckBox checked={value=="true"} setCheck={()=>setValue(value === "true" ? "false" : "true")} /> :
         type === "integer" ? <IntegerInput value={numberValue(value)} onChange={(n:number|undefined)=>setValue((n??0).toString())}></IntegerInput>:
         type === "email" ? <EmailInput value={value} onChange={(e:string|undefined)=>setValue(e??"")}/>:
         type === "url" ? <UrlInput value={value||""} onChange={(e:string|undefined)=>setValue(e??"")}/>:
         type === "option" ? (multiSelect ? <MultiSelectOption options={options??[]} value={value??""} setValue={setValue}/> : <RadioInput options={options??[]} value={value??""} setValue={setValue} />):
         <GenericInput value={value??""} onChange={(e)=>setValue(e??"")}/>
        }
    </>
}
