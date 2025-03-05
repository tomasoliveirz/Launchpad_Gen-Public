import { DialogActionTrigger, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog";
import { Field } from "@/components/ui/field";
import { BlockchainNetwork } from "@/models/BlockchainNetwork";
import { Input, Textarea } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { LaunchpadButton } from "../buttons/button";
import { FaSave } from "react-icons/fa";
import { ImageUpload } from "@/components/reUIsables/ImageInput/image-input";
import { Image } from "@chakra-ui/react";

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
        watch,
        formState: { errors },
    } = useForm<BlockchainNetwork>({
        defaultValues: defaultValues,
    });

    const [previewImage, setPreviewImage] = useState<string | null>(defaultValues?.image || null);

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

    const imageValue = watch("image");

    useEffect(() => {
        if (typeof imageValue === "string") {
            setPreviewImage(imageValue);
        }
    }, [imageValue]);

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
                    <ImageUpload
                        setValue={setValue}
                        formField="image"
                    />
                    </Field>
                    {previewImage && (
                            <Image
                                src={previewImage}
                                alt="Preview"
                                boxSize="100px"
                                borderRadius="md"
                                mt="1em"
                            />
                        )}
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

