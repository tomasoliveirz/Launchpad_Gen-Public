import { Field } from "@/components/ui/field"
import { DialogActionTrigger, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle, Input, Textarea } from "@chakra-ui/react"
import axios from "axios"
import { useForm } from "react-hook-form"
import { LauchpadButton } from "../buttons/button"
import { FaSave } from "react-icons/fa"

export interface EntityWithNameAndDescriptionDialogProps {
  open: boolean,
  setOpen: (b: boolean) => void,
  entityUrl: string,
  title: string
}

export function EntityWithNameAndDescriptionDialog({ open, setOpen, entityUrl, title }: EntityWithNameAndDescriptionDialogProps) {

    interface FormValues {
      name: string
      description: string
    }
    const {
      register,
      handleSubmit,
      formState: { errors },
    } = useForm<FormValues>()
  
    const onSubmit = handleSubmit(async (data) => {
      const url = import.meta.env.VITE_APP_API_URL
      try {
        await axios.post(`${url}/${entityUrl}`, data)
  
      } catch {
  
      }
      setOpen(false)
    })
  
    return <DialogRoot lazyMount open={open} onOpenChange={(e) => setOpen(e.open)}>
      <DialogContent position="fixed" right="35%" bottom="20%">
        <DialogHeader>
          <DialogTitle>{title}</DialogTitle>
        </DialogHeader>
          <DialogBody>
        <form onSubmit={onSubmit}>
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
            <LauchpadButton type="submit" icon={FaSave} text="Save" color="white" bg="#5CB338" />
        </form>
          </DialogBody>
          <DialogFooter>
            <DialogActionTrigger asChild>
              <LauchpadButton text="Cancel" color="white" bg="#FF7518" />
            </DialogActionTrigger>
          </DialogFooter>
      </DialogContent>
    </DialogRoot>
  }