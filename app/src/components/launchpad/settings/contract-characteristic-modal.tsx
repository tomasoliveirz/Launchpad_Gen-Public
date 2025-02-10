import { Field } from "@/components/ui/field"
import {Button, DialogActionTrigger, DialogBody, DialogCloseTrigger, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle, Input, Text, Textarea, VStack} from "@chakra-ui/react"

export interface ContractCharateristicModalProps {
  open: boolean,
  setOpen: (b: boolean) => void

  uuid: string,
  name: string,
  description: string 
}

export function ContractCharateristicModal({uuid, name, description, open, setOpen }: ContractCharateristicModalProps) {
  return <DialogRoot lazyMount open={open} onOpenChange={(e) => setOpen(e.open)}>
    <DialogContent>
      <DialogHeader>
        <DialogTitle>{name == "" ? name :  "New Contract Characteristic" } </DialogTitle>
      </DialogHeader>
      <DialogBody>
        <VStack gap="4em">
          <Field label="Name" required>
            <Input variant="subtle" value={name} />
          </Field>
          <Field label="Description">
            <Textarea size="xl" resize="none" variant="subtle" value={description} />
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
}