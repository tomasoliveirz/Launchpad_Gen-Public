import { Box, BoxProps, Text, Wrap } from "@chakra-ui/react";
import { IconType } from "react-icons";
import { Link } from "react-router-dom";

export interface LauchpadHomeCardProps extends BoxProps
{
    to:string
    icon: IconType
    title?:string
    description: string
}

export function LauchpadHomeCard({icon, to, title, description, ...props}:LauchpadHomeCardProps){
    const Icon = icon
    return <Box w="15em" h="15em" bgColor="gray.900" shadow="sm" {...props}>
        <Link to={to} style={{ width: "100%", height: "100%", display: "flex", justifyContent: "center" }}>
        <Wrap 
            textAlign="center" 
            display="flex"
            flexDirection="column"
            alignItems="center"
            justifyContent="space-evenly"
            px="1em"
        >
            <Box fontSize="4em">
                <Icon color="white"/>
            </Box>
            <Box>
                <Text textStyle="l" color="white">{title}</Text>
                <Text textStyle="xs" color="gray.400">{description}</Text>
            </Box>
        </Wrap>
        </Link>
    </Box>
}