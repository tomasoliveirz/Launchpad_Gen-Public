import { DialogActionTrigger, DialogBackdrop, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog";
import { Field } from "@/components/ui/field";
import { ContractType } from "@/models/ContractType";
import { ContractVariant } from "@/models/ContractVariant";
import { Input, ListCollection, Textarea } from "@chakra-ui/react";
import { RefObject, useEffect, useRef } from "react";
import { useForm } from "react-hook-form";
import { ContractTypeSelect } from "../select/contract-type-select";
import { LaunchpadButton } from "../buttons/button";
import { FaSave } from "react-icons/fa";

export interface ContractVariantDialogProps {
    open: boolean,
    onClose: () => void,
    title: string,
    onSubmit: (data: ContractVariant) => void
    defaultValues?: ContractVariant;
    collection: ListCollection<ContractType>
    selectValue: string | undefined
    selectOnValueChange: (a: string) => void
}

export function ContractVariantDialog({ open, onClose, title, onSubmit, defaultValues, collection, selectValue, selectOnValueChange }: ContractVariantDialogProps) {
    const {
        register,
        handleSubmit,
        setValue,
        formState: { errors },
    } = useForm<ContractVariant>({
        defaultValues: defaultValues,
    });

    useEffect(() => {
        if (defaultValues) {
            setValue("name", defaultValues.name);
            setValue("description", defaultValues.description);
            setValue("contractType.uuid", defaultValues.contractType.uuid);
        }
    }, [defaultValues, setValue]);

    const contentRef = useRef<HTMLDivElement>(null)

    return <DialogRoot lazyMount open={open} placement="center">
        <DialogBackdrop />
        <DialogContent ref={contentRef}>
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
                        label="Contract Type"
                        invalid={!!errors.contractType?.uuid}
                        errorText={errors.contractType?.uuid?.message}
                    >
                        <ContractTypeSelect {...register("contractType.uuid", { required: "Contract Type is required" })} collection={collection} ref={contentRef as RefObject<HTMLDivElement>} value={selectValue} onValueChange={(value) => {
                            selectOnValueChange(value);
                            setValue("contractType.uuid", value);
                        }} />
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