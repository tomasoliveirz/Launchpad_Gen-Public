import { DialogActionTrigger, DialogBackdrop, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog";
import { Field } from "@/components/ui/field";
import { Input, ListCollection } from "@chakra-ui/react";
import { RefObject, useEffect, useRef } from "react";
import { useForm } from "react-hook-form";
import { DialogSelect } from "../select/dialog-select";
import { LaunchpadButton } from "../buttons/button";
import { FaSave } from "react-icons/fa";
import { FeatureInContractType } from "@/models/FeatureInContractType";
import { ContractType } from "@/models/ContractType";
import { ContractFeature } from "@/models/ContractFeature";

export interface FeatureInContractTypeDialogProps {
    open: boolean,
    onClose: () => void,
    title: string,
    onSubmit: (data: FeatureInContractType) => void
    defaultValues?: ContractType;
    collection: ListCollection<ContractFeature>
    selectValue: string | undefined
    selectOnValueChange: (a: string) => void
}

export function FeatureInContractTypeDialog({ open, onClose, title, onSubmit, defaultValues, collection, selectValue, selectOnValueChange }: FeatureInContractTypeDialogProps) {
    const {
        register,
        handleSubmit,
        setValue,
        formState: { errors },
    } = useForm<FeatureInContractType>({
        defaultValues: defaultValues,
    });

    useEffect(() => {
        if (defaultValues) {
            setValue("contractType", defaultValues);
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
                        label="Contract Type"
                    >
                        <Input readOnly
                            {...register("contractType.name")}
                        />
                    </Field>
                    <Field
                        mt="2em"
                        label="Contract Feature"
                        invalid={!!errors.contractFeature?.uuid}
                        errorText={errors.contractFeature?.uuid?.message}
                    >
                        <DialogSelect label="Choose Contract Feature" {...register("contractFeature.uuid", { required: "Contract Feature is required" })} collection={collection} ref={contentRef as RefObject<HTMLDivElement>} value={selectValue} onValueChange={(value) => {
                            selectOnValueChange(value);
                            setValue("contractFeature.uuid", value);
                        }} />
                    </Field>
                    <Field
                        mt="2em"
                        label="Default Value"
                        invalid={!!errors.DefaultValue}
                        errorText={errors.DefaultValue?.message}
                    >
                        <Input
                            {...register("DefaultValue")}
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