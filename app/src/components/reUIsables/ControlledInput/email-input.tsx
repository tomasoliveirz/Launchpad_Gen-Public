import { GenericInput, GenericInputProps } from "./generic-input";


export function EmailInput({setError, onChange, ...props} : GenericInputProps<string>)
{
    return <GenericInput type="email" {...props} onChange={(s)=>updateValue(s, onChange, setError)}/>
}

function updateValue(s:any, onChange:(s:string)=>void, setError:((v:boolean|string)=>void)|undefined)
{
    onChange(s as string)
    if(setError) setError(s !== "" && !isEmail(s)); 
}

export function isEmail(email:string):boolean
{
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}

