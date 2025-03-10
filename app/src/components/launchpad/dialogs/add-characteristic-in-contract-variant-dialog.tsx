import { DialogActionTrigger, DialogBackdrop, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog";
import { Field } from "@/components/ui/field";
import { CharacteristicInContractVariant } from "@/models/CharacteristicInContractVariant";
import { ContractCharacteristic } from "@/models/ContractCharacteristic";
import { Input, ListCollection } from "@chakra-ui/react";
import { RefObject, useEffect, useRef } from "react";
import { useForm } from "react-hook-form";
import { DialogSelect } from "../select/dialog-select";
import { LaunchpadButton } from "../buttons/button";
import { FaSave } from "react-icons/fa";
import { ContractVariant } from "@/models/ContractVariant";

export interface CharacteristicInContractVariantDialogProps {
    open: boolean,
    onClose: () => void,
    title: string,
    onSubmit: (data: CharacteristicInContractVariant) => void
    defaultValues?: ContractVariant;
    collection: ListCollection<ContractCharacteristic>
    selectValue: string | undefined
    selectOnValueChange: (a: string) => void
}

export function CharacteristicInContractVariantDialog({ open, onClose, title, onSubmit, defaultValues, collection, selectValue, selectOnValueChange }: CharacteristicInContractVariantDialogProps) {
    const {
        register,
        handleSubmit,
        setValue,
        formState: { errors },
    } = useForm<CharacteristicInContractVariant>({
        defaultValues: defaultValues,
    });

    useEffect(() => {
        if (defaultValues) {
            setValue("contractVariant", defaultValues);
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
                        label="Contract Variant"
                    >
                        <Input readOnly
                            {...register("contractVariant.name")}
                        />
                    </Field>
                    <Field
                        mt="2em"
                        label="Contract Characteristic"
                        invalid={!!errors.contractCharacteristic?.uuid}
                        errorText={errors.contractCharacteristic?.uuid?.message}
                    >
                        <DialogSelect label="Choose Contract Characteristic" {...register("contractCharacteristic.uuid", { required: "Contract Characteristic is required" })} collection={collection} ref={contentRef as RefObject<HTMLDivElement>} value={selectValue} onValueChange={(value) => {
                            selectOnValueChange(value);
                            setValue("contractCharacteristic.uuid", value);
                        }} />
                    </Field>
                    <Field
                        mt="2em"
                        label="Value"
                        invalid={!!errors.Value}
                        errorText={errors.Value?.message}
                    >
                        <Input
                            {...register("Value")}
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