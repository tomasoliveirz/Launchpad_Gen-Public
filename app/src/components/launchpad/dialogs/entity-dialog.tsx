import { Field } from "@/components/ui/field"
import { DialogActionTrigger, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog"
import { DefaultValues, FieldValues, FormProvider, Path, PathValue, useForm, useFormContext } from "react-hook-form";
import { LaunchpadButton } from "../buttons/button"
import { FaSave } from "react-icons/fa"
import { Input, ListCollection, Textarea, Image } from "@chakra-ui/react"
import { GenericSelect } from "../select/entity-select";
import { RefObject, useEffect, useState } from "react";
import { ImageUpload } from "@/components/reUIsables/ImageInput/image-input";

export interface EntityDialogItemProps<T> {
    dataKey: keyof (T);
    label: string;
    dataType?: "text" | "longText" | "select" | "image";
    selectCollection?: ListCollection<any>;
    getItemKey?: (item: any) => string;
    getItemLabel?: (item: any) => string;
    ref?: RefObject<HTMLDivElement>;
}

export interface EntityDialogProps<T> {
    columns?: EntityDialogItemProps<T>[]
    open: boolean,
    onClose: () => void,
    title: string,
    onSubmit: (data: T) => void
    defaultValues?: T;
    ref?: RefObject<HTMLDivElement>;
}

export function FieldType<T extends FieldValues>({ dataKey, label, dataType, selectCollection, getItemKey, getItemLabel, ref }: EntityDialogItemProps<T>) {
    const { register, formState: { errors }, setValue, watch } = useFormContext<T>();
    const fieldValue = watch(dataKey as Path<T>);

    const handleValueChange = (val: string) => {
        setValue(dataKey as Path<T>, val as PathValue<T, Path<T>>);
    };

    if (dataType === "text") {
        return (
            <Input
                {...register(dataKey as Path<T>, { required: `${label} is required` })}
            />
        )
    }

    if (dataType === "longText") {
        return (
            <Textarea
                resize="none"
                {...register(dataKey as Path<T>)}
            />
        )
    }

    if (dataType === "select" && selectCollection && getItemKey && getItemLabel) {
        return (
            <GenericSelect
                {...register(dataKey as Path<T>, { required: `${label} is required` })}
                collection={selectCollection}
                onValueChange={(val) => handleValueChange(val)}
                value={fieldValue}
                label={label}
                getItemKey={getItemKey}
                getItemLabel={getItemLabel}
                ref={ref}
            />
        );
    }

    if (dataType === "image") {
        return (
            <ImageUpload
                setValue={setValue}
                formField="image"
            />
        )
    }
}


export function EntityDialog<T extends FieldValues>({ columns, open, onClose, title, onSubmit, defaultValues, ref }: EntityDialogProps<T>) {
    const formMethods = useForm<T>({
        defaultValues: defaultValues as DefaultValues<T>,
    });

    const { setValue, reset, watch } = formMethods;

    useEffect(() => {
        if (defaultValues) {
            columns?.forEach(({ dataKey }) => {
                setValue(dataKey as Path<T>, defaultValues[dataKey as keyof T] as PathValue<T, Path<T>>);
            });
        }
        else {
            reset();
        }
    }, [defaultValues, setValue, reset]);

    return <FormProvider {...formMethods}>
        <DialogRoot lazyMount open={open} placement="center">
            <DialogContent ref={ref}>
                <form onSubmit={formMethods.handleSubmit(onSubmit)}>
                    <DialogHeader>
                        <DialogTitle>{title}</DialogTitle>
                    </DialogHeader>
                    <DialogBody>
                        {columns?.map(({ dataKey, label, dataType, selectCollection, getItemKey, getItemLabel }) => (
                            <Field key={String(dataKey)} mt="2em" label={label} invalid={!!formMethods.formState.errors[dataKey]}
                                errorText={formMethods.formState.errors[dataKey]?.message as string}>
                                <FieldType dataKey={dataKey} label={label} dataType={dataType} selectCollection={selectCollection}
                                    getItemKey={getItemKey} getItemLabel={getItemLabel} ref={ref} />
                                {dataType === 'image' && watch(dataKey as Path<T>) && (
                                    <Image
                                        src={watch(dataKey as Path<T>) as string}
                                        alt="Preview"
                                        boxSize="100px"
                                        borderRadius="md"
                                        mt="1em"
                                    />
                                )}
                            </Field>
                        ))}
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
    </FormProvider>
}