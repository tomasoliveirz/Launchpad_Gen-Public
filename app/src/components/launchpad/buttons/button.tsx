import { Button, ButtonProps, HStack, Text } from "@chakra-ui/react";
import { IconType } from "react-icons";
import { FaPlus } from "react-icons/fa";


export interface LaunchpadButtonProps extends ButtonProps {
    icon?: IconType,
    text?: string
}

export function LaunchpadButton({ icon, text, ...props }: LaunchpadButtonProps) {
    const Icon = icon
    return <Button {...props}>
        <HStack>
            {Icon && <Icon/>} 
            {text && <Text>{text}</Text>}
        </HStack>
    </Button>
}

export function LaunchpadNewButton({...props}:LaunchpadButtonProps)
{
    return <LaunchpadButton icon={FaPlus} text="New" color="white" bg="#5CB338" {...props}/>
}