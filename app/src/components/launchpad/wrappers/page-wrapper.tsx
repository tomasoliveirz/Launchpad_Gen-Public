import { Box, BoxProps, Heading, HStack } from "@chakra-ui/react";
import { IconType } from "react-icons";

export interface PageWrapperProps extends BoxProps
{
    title:string
    icon?:IconType
}

export function PageWrapper({title, icon, children, ...props}:PageWrapperProps)
{
    const Icon = icon;
    return <Box {...props} pt="10em" pl="7em" pr="5em">
        <HStack w="100%" gap="0.75em" fontSize="4rem" color="white">
            {Icon && <Icon/>}
            <Heading as="h2" fontSize="1em">{title}</Heading>
        </HStack>
        {children}
    </Box>
}