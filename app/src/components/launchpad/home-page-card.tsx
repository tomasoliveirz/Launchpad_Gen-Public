import { Box, BoxProps, Text, Wrap } from "@chakra-ui/react";
import { IconType } from "react-icons";
import { Link } from "react-router-dom";


export interface HomePageCardProps extends BoxProps
{
    to:string
    icon: IconType
    title?:string
    description: string
}

export default function({icon, to, title, description, ...props}:HomePageCardProps)
{
    const Icon = icon
    return <Box w="280px" h="260px" bgColor="gray.900" shadow="sm" {...props}>
        <Link to={to} style={{ width: "100%", height: "100%", display: "flex" }}>
        <Wrap 
            textAlign="center" 
            width="100%"
            display="flex"
            flexDirection="column"
            alignItems="center"
            justifyContent="space-around"
            p="2em"
        >
            <Box fontSize="5em">
                <Icon color="white"/>
            </Box>
            <Text textStyle="2xl" color="white">{title}</Text>
            <Text textStyle="sm" color="gray.400">{description}</Text>
        </Wrap>
        </Link>
    </Box>
}
  