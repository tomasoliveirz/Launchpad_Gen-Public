import { Button, ButtonProps, HStack, Text } from "@chakra-ui/react";
import { IconType } from "react-icons";


export interface LauchpadButtonProps extends ButtonProps {
    icon?: IconType,
    text?: string
}

export function LauchpadButton({ icon, text, ...props }: LauchpadButtonProps) {
    const Icon = icon
    return <Button {...props}>
        <HStack>
            {Icon && <Icon/>} 
            {text && <Text>{text}</Text>}
        </HStack>
    </Button>
}