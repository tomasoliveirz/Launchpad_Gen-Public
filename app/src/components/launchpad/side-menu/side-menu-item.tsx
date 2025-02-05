import { Box, BoxProps } from "@chakra-ui/react";
import { IconType } from "react-icons";
import { Link } from "react-router-dom";

export interface SideMenuItemProps extends BoxProps
{
    to:string
    icon: IconType
    isMain?:boolean
    title?:string
}

export default function({icon, to, title, isMain, ...props}:SideMenuItemProps)
{
    const Icon = icon
    return <Box _hover={{fontSize:isMain ? "1.75em":"1.5em"}} {...props}>
                <Link to={to}>
                    <Box fontSize={isMain? "1.5em": "1.25em"}>
                        <Icon color="white" title={title}/>
                    </Box>
                </Link>
            </Box>
}