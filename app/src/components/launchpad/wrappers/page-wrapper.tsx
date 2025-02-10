import { Box, BoxProps, Heading, HStack } from "@chakra-ui/react";
import { IconType } from "react-icons";

export interface PageWrapperProps extends BoxProps
{
    title:string
    description?:string
    icon?:IconType
}

export function PageWrapper({title, icon, children, ...props}:PageWrapperProps)
{
    const Icon = icon;
    return <Box {...props} pt="8em" pl="4em" pr="0em" maxW="100%" overflow={"hidden"}>
        <HStack w="100%" gap="0.75em" fontSize="2.5rem" color="white">
            {Icon && <Icon/>}
            <Heading as="h1" fontSize="1.1em">{title}</Heading>
        </HStack>
        {children}
    </Box>
}