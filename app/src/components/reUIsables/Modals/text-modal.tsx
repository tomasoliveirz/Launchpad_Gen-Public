import { DialogBody, DialogCloseTrigger, DialogContent,DialogRoot, DialogTrigger } from "@/components/ui/dialog";
import { DialogHeader, Text, TextProps } from "@chakra-ui/react";

export interface TextModalProps extends TextProps
{
    text:string
    header?:string
    maxCharacters:number
}

export function TextModal({text, header, maxCharacters, ...props}:TextModalProps)
{
    return text?.length < maxCharacters ?  <Text {...props}>{text}</Text> :
    <DialogRoot key={props.key} size="sm" placement="center" motionPreset="slide-in-bottom">
    <DialogTrigger asChild>
      <Text cursor="pointer" {...props}>
        {text.substring(0, maxCharacters-3)+"..."}
      </Text>
    </DialogTrigger>
    <DialogContent  >
      <DialogBody   >
        <Text mt="1em" textAlign="justify">
          {text}
        </Text>
      </DialogBody>
    </DialogContent>
  </DialogRoot>
}