import { Field } from "@/components/ui/field"
import { DialogActionTrigger, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog"
import { useForm } from "react-hook-form"
import { LaunchpadButton } from "../buttons/button"
import { FaSave } from "react-icons/fa"
import { Input, Textarea } from "@chakra-ui/react"


export interface EntityWithNameAndDescriptionDialogProps {
  open: boolean,
  onClose: () => void,
  title: string,
  onSubmit: (data: FormValues) => void
}

export interface FormValues {
  name: string
  description: string
}
export function EntityWithNameAndDescriptionDialog({ open, onClose, title, onSubmit }: EntityWithNameAndDescriptionDialogProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>()

  return <DialogRoot lazyMount open={open} placement="center">
    <DialogContent>
      <form onSubmit={handleSubmit(onSubmit)}>
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
        </DialogHeader>
        <DialogBody>
          <Field
            label="Name"
            invalid={!!errors.name}
            errorText={errors.name?.message}
          >
            <Input
              {...register("name", { required: "Name is required" })}
            />
          </Field>
          <Field
            mt="2em"
            label="Description"
            invalid={!!errors.description}
            errorText={errors.description?.message}
          >
            <Textarea
              resize="none"
              {...register("description")}
            />
          </Field>
        </DialogBody>
        <DialogFooter>
          <LaunchpadButton type="submit" icon={FaSave} text="Save" color="white" bg="#5CB338" />
          <DialogActionTrigger asChild>
            <LaunchpadButton text="Cancel" onClick={onClose} color="white" bg="#FF7518" />
          </DialogActionTrigger>
        </DialogFooter>
      </form>
    </DialogContent>
  </DialogRoot>
}

