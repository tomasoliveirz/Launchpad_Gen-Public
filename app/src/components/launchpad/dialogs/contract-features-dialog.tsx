import { Field } from "@/components/ui/field"
import { DialogActionTrigger, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog"
import { useForm } from "react-hook-form"
import { LaunchpadButton } from "../buttons/button"
import { FaSave } from "react-icons/fa"
import { Input, Textarea } from "@chakra-ui/react"
import { useEffect } from "react"
import { ContractFeature } from "@/models/ContractFeature"


export interface ContractFeaturesDialogProps {
    open: boolean,
    onClose: () => void,
    title: string,
    onSubmit: (data: ContractFeature) => void
    defaultValues?: ContractFeature;
}

export function ContractFeaturesDialog({ open, onClose, title, onSubmit, defaultValues }: ContractFeaturesDialogProps) {
    const {
        register,
        handleSubmit,
        setValue,
        formState: { errors },
    } = useForm<ContractFeature>({
        defaultValues: defaultValues,
    });

    useEffect(() => {
        if (open) {
            if (defaultValues) {
                setValue("name", defaultValues.name);
                setValue("description", defaultValues.description);
                setValue("dataType", defaultValues.dataType);
                setValue("defaultValue", defaultValues.defaultValue);
                setValue("normalizedName", defaultValues.normalizedName);
            } else {
                setValue("name", "");
                setValue("description", "");
                setValue("dataType", "");
                setValue("defaultValue", "");
                setValue("normalizedName", "");
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
                        label="Data Type"
                        invalid={!!errors.dataType}
                        errorText={errors.dataType?.message}
                    >
                        <Input
                            {...register("dataType", { required: "DataType is required" })}
                        />
                    </Field>
                    <Field
                        mt="2em"
                        label="Normalized Name"
                        invalid={!!errors.normalizedName}
                        errorText={errors.normalizedName?.message}
                    >
                        <Input
                            {...register("normalizedName")}
                        />
                    </Field>
                    <Field
                        mt="2em"
                        label="Default Value"
                        invalid={!!errors.defaultValue}
                        errorText={errors.defaultValue?.message}
                    >
                        <Input
                            {...register("defaultValue")}
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

