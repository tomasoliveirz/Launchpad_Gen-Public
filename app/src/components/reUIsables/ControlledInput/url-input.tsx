import { GenericInput, GenericInputProps } from "./generic-input";


export function UrlInput({setError, onChange, ...props} : GenericInputProps<string>)
{
    return <GenericInput type="url" {...props} onChange={(s)=>updateValue(s, onChange, setError)}/>
}

function updateValue(s:any, onChange:(s:string)=>void, setError:((v:boolean|string)=>void)|undefined)
{
    onChange(s as string)
    if(setError) setError(s !== "" && !isUrl(s)); 
}

export function isUrl(url:string):boolean
{
    try {
        new URL(url);
        return true;
      } catch (e) {
        return false;
      }
}

