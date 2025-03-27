import { DialogBody, DialogContent,DialogRoot, DialogTrigger } from "@/components/ui/dialog";
import { DialogHeader, Text, TextProps } from "@chakra-ui/react";

export interface TextModalProps extends TextProps
{
    text:string
    maxCharacters:number
    fullWord?:boolean
}

export function TextModal({text, maxCharacters, fullWord, ...props}:TextModalProps)
{
    return text?.length < maxCharacters ?  <Text {...props}>{text}</Text> :
    <DialogRoot key={props.key} placement="center" motionPreset="slide-in-bottom">
    <DialogTrigger asChild>
      <Text cursor="pointer" {...props}>
        {
          fullWord ? 
          text.substring(0, maxCharacters) : 
          text.substring(0, maxCharacters-3)+"..."
        }
      </Text>
    </DialogTrigger>
    <DialogContent>
    <DialogHeader></DialogHeader>
      <DialogBody  w="95%" pb="5%" mx="auto" overflowY="auto">
        <Text textAlign="justify">
          {text}
        </Text>
      </DialogBody>
    </DialogContent>
  </DialogRoot>
}