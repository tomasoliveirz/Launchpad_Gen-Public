import { FieldRoot, FieldLabel, FieldErrorText, BoxProps } from "@chakra-ui/react"
import { error } from "console"
import { IntegerInput } from "./int-input"

export interface FieldWrapperProps extends BoxProps
{
    label:string
    defaultError:string
    error:string|boolean
    
}

export function FieldWrapper({label, defaultError, error, ...props}:FieldWrapperProps)
{
    return <FieldRoot invalid={(typeof(error) === "boolean" && error === true) || typeof(error) === "string" && error !== ""}>
                <FieldLabel>{label}</FieldLabel>
                {props.children}
                <FieldErrorText>
                    {typeof(error) === "boolean" ? defaultError : error}    
                </FieldErrorText>
            </FieldRoot>
}