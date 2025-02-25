import { Checkbox } from "@/components/ui/checkbox"
import { FieldErrorText, FieldLabel, FieldRoot, HStack, Input, InputProps, Spacer } from "@chakra-ui/react"
import { FaInfoCircle } from "react-icons/fa"

export interface TypeFieldProps extends Omit<InputProps, "onChange">
{
    type:"text"|"email"|"url"|"integer"|"decimal"
    label:string
    description?:string
}

export function TypeField({type, label, description, ...props}:TypeFieldProps)
{
    return <FieldRoot>
                <FieldLabel>
                    <HStack>
                        {label}
                        <Spacer/>
                        {description && <FaInfoCircle title={description}/>}
                    </HStack>
                </FieldLabel>
                <FieldErrorText />
                {
                    type === "text" ? <Input {...props}/> :
                    type === "integer" ? <Input type="number" />: 
                    type === "url" ? <Input type="url" />: 
                    type === "email" ? <Input type="email" />:
                    type === "decimal" ? <Input type="" ></Input>:
                    type === "boolean" ? <Checkbox/>:
                                        <></>
                }
            </FieldRoot>
    
    
    
    
}

