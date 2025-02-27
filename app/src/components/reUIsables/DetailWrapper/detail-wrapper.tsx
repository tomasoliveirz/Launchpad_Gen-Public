import { Box, Heading, HStack, StackProps, VStack } from "@chakra-ui/react";
import { IconType } from "react-icons";
import { BreadcrumbProps } from "../Breadcrumbs/breadcrumbs";
import { ImageOrIcon } from "../ImageOrIcon/image-or-icon";
import { JSX } from "react";


export interface DetailWrapperProps extends StackProps
{
    icon?: IconType | string
    title: string
    breadcrumbProps?:BreadcrumbProps
    rightSideElement:JSX.Element
}

export function DetailWrapper({icon, title, breadcrumbProps, rightSideElement, children, ...props}:DetailWrapperProps)
{
    return <VStack {...props} pt="2em" px="3em" maxW="100%" w="100%" maxH="100%" h="100%" overflowY="hidden">
                <HStack w="100%">
                    <VStack w="100%">
                        <HStack w="100%">
                            {icon && <ImageOrIcon w="2em" value={icon}/>}
                            <Heading as="h2" fontSize="2em">{title}</Heading>
                        </HStack>
                    </VStack>
                    {rightSideElement}
                </HStack>
                <Box w="100%">
                    {children}
                </Box>
        </VStack>
}