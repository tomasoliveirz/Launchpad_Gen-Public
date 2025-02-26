import { DialogActionTrigger, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog"
import { useForm } from "react-hook-form"
import { LaunchpadButton } from "../buttons/button"
import { FaSave, FaTrashAlt } from "react-icons/fa"
import { Text } from "@chakra-ui/react"
import { EntityWithNameAndDescription } from "@/models/EntityWithNameAndDescription"

export interface DeleteConfirmationDialogProps {
  open: boolean,
  onClose: () => void,
  title: string,
  onSubmit: (data: EntityWithNameAndDescription) => void
  defaultValues?: EntityWithNameAndDescription;
}

export function DeleteConfirmationDialog({ open, onClose, title, onSubmit }: DeleteConfirmationDialogProps) {
  const {
    handleSubmit
  } = useForm<EntityWithNameAndDescription>();

  return <DialogRoot lazyMount open={open} placement="center">
    <DialogContent>
      <form onSubmit={handleSubmit(onSubmit)}>
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
        </DialogHeader>
        <DialogBody>
          <Text>Are you sure you want to delete ?</Text>
        </DialogBody>
        <DialogFooter>
          <LaunchpadButton type="submit" icon={FaTrashAlt} text="Delete" color="white" bg="#dd1717" />
          <DialogActionTrigger asChild>
            <LaunchpadButton text="Cancel" onClick={onClose} color="white" bg="#FF7518" />
          </DialogActionTrigger>
        </DialogFooter>
      </form>
    </DialogContent>
  </DialogRoot>
}

