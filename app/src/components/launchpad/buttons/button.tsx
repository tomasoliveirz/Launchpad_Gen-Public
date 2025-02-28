import { Button, ButtonProps, HStack, Text } from "@chakra-ui/react";
import { IconType } from "react-icons";
import { FaPencilAlt, FaPlus, FaTrashAlt } from "react-icons/fa";


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
    return <LaunchpadButton size="sm" icon={FaPlus} text="New" color="white" bg="#5CB338" {...props}/>
}

export function EditButton(props:LaunchpadButtonProps)
{
    return <LaunchpadButton size="sm" icon={FaPencilAlt} text="Edit" {...props} bg="warning" color="text.primary"/>
}

export function DeleteButton(props:LaunchpadButtonProps)
{
    return <LaunchpadButton size="sm" icon={FaTrashAlt} text="Delete" {...props} bg="error" color="text.primary"/>
}