import { BoxProps, Button, HStack, Input, Textarea, VStack } from "@chakra-ui/react"
import {
    DialogActionTrigger,
    DialogBody,
    DialogCloseTrigger,
    DialogContent,
    DialogFooter,
    DialogHeader,
    DialogRoot,
    DialogTitle,
    DialogTrigger,
  } from "@/components/ui/dialog"
import { FaPencilAlt } from "react-icons/fa"
import { Field } from "@/components/ui/field"


export interface Item {
    uuid: string
    name: string
}

export interface ContractTypesModalProps extends BoxProps
{
    item: Item[]
}



export default function({item, ...props}:ContractTypesModalProps)
{
    return <> 
          <HStack>
          <DialogRoot placement="center">
            <DialogTrigger asChild>
              <Button size="sm">
                <FaPencilAlt color="white"/>
              </Button>
            </DialogTrigger>
            <DialogContent>
              <DialogHeader>
                <DialogTitle>{item.name ?? "New Contract Type"}</DialogTitle>
              </DialogHeader>
              <DialogBody>
                <VStack gap="4em">
                    <Field label="Name" required>
                        <Input variant="subtle" value={item.name} />
                    </Field>
                    <Field label="Description">
                        <Textarea size="xl" resize="none" variant="subtle" value={item.description} />
                    </Field>
                </VStack>
              </DialogBody>
              <DialogFooter>
                <DialogActionTrigger asChild>
                  <Button variant="outline">Cancel</Button>
                </DialogActionTrigger>
                <Button>Save</Button>
              </DialogFooter>
              <DialogCloseTrigger />
            </DialogContent>
          </DialogRoot>
          </HStack>
            </>
}
