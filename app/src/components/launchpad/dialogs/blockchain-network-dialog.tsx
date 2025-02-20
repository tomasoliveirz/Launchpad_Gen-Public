import { DialogActionTrigger, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog";
import { Field } from "@/components/ui/field";
import { BlockchainNetwork } from "@/models/BlockchainNetwork";
import { Button, Input, Textarea } from "@chakra-ui/react";
import { useCallback, useEffect } from "react";
import { useForm, UseFormSetValue } from "react-hook-form";
import { LaunchpadButton } from "../buttons/button";
import { FaSave } from "react-icons/fa";
import { FileUploadList, FileUploadRoot, FileUploadTrigger } from "@/components/ui/file-upload";
import { HiUpload } from "react-icons/hi";

export interface BlockchainNetworksDialogProps {
    open: boolean,
    onClose: () => void,
    title: string,
    onSubmit: (data: BlockchainNetwork) => void
    defaultValues?: BlockchainNetwork;
}

export function BlockchainNetworksDialog({ open, onClose, title, onSubmit, defaultValues }: BlockchainNetworksDialogProps) {
    const {
        register,
        handleSubmit,
        setValue,
        formState: { errors },
    } = useForm<BlockchainNetwork>({
        defaultValues: defaultValues,
    });

    useEffect(() => {
        if (open) {
            if (defaultValues) {
                setValue("name", defaultValues.name);
                setValue("description", defaultValues.description);
                setValue("image", defaultValues.image);
            } else {
                setValue("name", "");
                setValue("description", "");
                setValue("image", "");
            }
        }
    }, [open, defaultValues, setValue]);

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
                    <Field
                        mt="2em"
                        label="Image"
                        invalid={!!errors.image}
                        errorText={errors.image?.message}
                    >
                        <ImageUpload setValue={setValue} />
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

function ImageUpload({ setValue }: { setValue: UseFormSetValue<BlockchainNetwork> }) {
    const handleFileChange = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
      const file = event.target.files?.[0];
      if (file) {
        const reader = new FileReader();
        reader.onload = () => {
          const dataURL = reader.result as string;
          setValue('image', dataURL);
        };
        reader.readAsDataURL(file); 
      }
    }, [setValue]);

    return (
        <FileUploadRoot onChange={handleFileChange}>
            <FileUploadTrigger asChild>
                <Button variant="outline" size="sm">
                    <HiUpload /> Upload Image
                </Button>
            </FileUploadTrigger>
            <FileUploadList />
        </FileUploadRoot>
    );
}