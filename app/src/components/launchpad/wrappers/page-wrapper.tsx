import { Box, BoxProps, Heading, HStack, VStack } from "@chakra-ui/react";
import { IconType } from "react-icons";

export interface PageWrapperProps extends BoxProps
{
    title:string
    description?:string
    icon?:IconType
}

export function PageWrapper({title, icon, description, children, ...props}:PageWrapperProps)
{
    const Icon = icon;
    return <Box {...props} pt="8em" pl="4em" pr="0em" maxW="100%">
        <VStack>
            <HStack w="100%" gap="0.75em" fontSize="2.5rem" color="white">
                {Icon && <Icon />}
                <Heading as="h1" fontSize="1.1em">{title}</Heading>
            </HStack>
            <HStack w="100%" gap="0.75em" color="white">
                {description && <Heading as="h6" fontSize="1em">{description}</Heading>}
            </HStack>
        </VStack>
        {children}
    </Box>
}