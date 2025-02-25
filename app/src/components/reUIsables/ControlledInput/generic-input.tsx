import { Input, InputProps } from "@chakra-ui/react";

export interface GenericInputProps<T> extends Omit<InputProps, "onChange"|"value">
{
    setError?:(v:boolean|string)=>void
    onChange:(s:T|undefined)=>void
    format?:(s:T)=>string
    value:T
}

export function GenericInput<T>({setError,value,format, onChange, ...props}:GenericInputProps<T>)
{
    return <Input value={formatData(value, format)??""} onChange={e=>onChange(e.target.value as any)} {...props}/>
}

function formatData<T>(val:T, defaultFormat:((t:T)=>string)|undefined)
{
    if(!val) return "";
    if(defaultFormat) return defaultFormat(val);
    return val+"";
}