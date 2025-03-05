import { DialogBody, DialogCloseTrigger, DialogContent,DialogRoot, DialogTrigger } from "@/components/ui/dialog";
import { DialogHeader, Text, TextProps } from "@chakra-ui/react";

export interface TextModalProps extends TextProps
{
    text:string
    maxCharacters:number
}

export function TextModal({text, maxCharacters, ...props}:TextModalProps)
{
    return text?.length < maxCharacters ?  <Text {...props}>{text}</Text> :
    <DialogRoot key={props.key} size="cover" placement="center" motionPreset="slide-in-bottom">
    <DialogTrigger asChild>
      <Text cursor="pointer" {...props}>
        {text.substring(0, maxCharacters-3)+"..."}
      </Text>
    </DialogTrigger>
    <DialogContent h="90%" >
    <DialogHeader></DialogHeader>
      <DialogCloseTrigger/>
      <DialogBody  h="90%" w="95%" pb="5%" mx="auto" overflowY="auto">
        <Text textAlign="justify">
          {text}
        </Text>
      </DialogBody>
    </DialogContent>
  </DialogRoot>
}