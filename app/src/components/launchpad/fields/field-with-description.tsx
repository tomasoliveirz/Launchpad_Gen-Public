import { BoxProps, FieldLabel, FieldRoot, HStack } from "@chakra-ui/react"
import { CiCircleQuestion } from "react-icons/ci"

export interface FieldWithDescriptionProps extends BoxProps {
    children: React.ReactNode
    label: React.ReactNode
    description?: string
}
export function FieldWithDescription({ children, description, label, ...props }: FieldWithDescriptionProps) {
    return <FieldRoot {...props}>
        <FieldLabel>
            <HStack>
                {label}
                {description && <CiCircleQuestion title={description} />}
            </HStack>
        </FieldLabel>
        {children}
    </FieldRoot>
}