import { InputGroup } from "@/components/ui/input-group";
import { GenericInput, GenericInputProps } from "./generic-input";
import { FaSearch } from "react-icons/fa";


export function SearchInput({onChange, ...props} : GenericInputProps<string|undefined>)
{
    return <InputGroup startElement={<FaSearch />}>
                <GenericInput {...props} onChange={(s)=>updateValue(s, onChange)}/>
            </InputGroup>
}

function updateValue(s:any, onChange:(s:string)=>void)
{
    onChange(s as string)
}

