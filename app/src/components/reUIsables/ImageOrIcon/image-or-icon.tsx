import { Box, BoxProps, Image, ImageProps } from "@chakra-ui/react";
import { IconBaseProps, IconType } from "react-icons";

export interface ImageOrIconProps extends BoxProps
{
    value?:string|IconType
    imageProps?:ImageProps
    iconTypeProps?:IconBaseProps
}

export function IconFromValue(value:IconType, props?:IconBaseProps){
    const Value = value;
    return <Value size="100%" {...props}/>
}

export function ImageOrIcon({value, imageProps, iconTypeProps, ...props}:ImageOrIconProps)
{
    return <Box {...props}>
        {value === undefined ? <></> : 
         typeof(value) === "string" ? <Image w="100%" h="auto" {...imageProps} src={value}/> : 
                                        IconFromValue(value as IconType, iconTypeProps)
        }
    </Box>
    
}
