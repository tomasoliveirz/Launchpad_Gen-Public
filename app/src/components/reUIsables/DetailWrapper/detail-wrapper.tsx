import { Box, Heading, HStack, StackProps, VStack } from "@chakra-ui/react";
import { IconType } from "react-icons";
import { BreadcrumbsProps, Breadcrumbs } from "../Breadcrumbs/breadcrumbs";
import { ImageOrIcon } from "../ImageOrIcon/image-or-icon";
import { JSX } from "react";


export interface DetailWrapperProps extends StackProps
{
    icon?: IconType | string
    title: string
    breadcrumbsProps?:BreadcrumbsProps
    rightSideElement:JSX.Element
}

export function DetailWrapper({icon, title, breadcrumbsProps, rightSideElement, children, ...props}:DetailWrapperProps)
{
    return <VStack {...props} pt="2em" px="3em" maxW="100%" w="100%" maxH="100%" h="100%" overflowY="hidden">
                <HStack w="100%">
                    <VStack w="100%">
                        <HStack w="100%">
                            {icon && <ImageOrIcon w="2.4em" mr="1em" value={icon}/>}
                            <Heading as="h2" fontSize="2.4em">{title}</Heading>
                        </HStack>
                        <Box w="100%" pt="1em">
                            <Breadcrumbs {...breadcrumbsProps}/>
                        </Box>
                    </VStack>
                    <Box>
                        {rightSideElement}
                    </Box>
                </HStack>
                <Box w="100%">
                    {children}
                </Box>
        </VStack>
}