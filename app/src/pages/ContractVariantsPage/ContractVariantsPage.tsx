import { LaunchpadButton, LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-diaolg";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { LaunchpadNameTable } from "@/components/launchpad/tables/name-table";
import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { DialogActionTrigger, DialogBody, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle } from "@/components/ui/dialog";
import { Field } from "@/components/ui/field";
import { SelectContent, SelectItem, SelectLabel, SelectRoot, SelectTrigger, SelectValueText } from "@/components/ui/select";
import { Toaster, toaster } from "@/components/ui/toaster";
import { ContractVariant } from "@/models/ContractVariant";
import { launchpadApi, useGetContractTypesQuery } from "@/services/launchpad/launchpadService";
import { Box, createListCollection, HStack, Input, Textarea, useDisclosure, VStack } from "@chakra-ui/react";
import { use, useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { FaSave } from "react-icons/fa";
import { IoGitBranchOutline } from "react-icons/io5";

export default function () {
    const { data = [], error, isLoading, refetch } = launchpadApi.useGetContractVariantsQuery();
    const [page, setPage] = useState(1);
    const pageSize = 6;
    const paginatedItems = data.slice((page - 1) * pageSize, page * pageSize);
    const pageCount = Math.ceil(data.length / pageSize);

    const [selectedItem, setSelectedItem] = useState<ContractVariant | null>(null);

    const [createContractVariant] = launchpadApi.useCreateContractVariantMutation()
    const [updateContractVariant] = launchpadApi.useUpdateContractVariantMutation()
    const [removeContractVariant] = launchpadApi.useRemoveContractVariantMutation()

    const onSubmitCreate = async (data: ContractVariant) => {
        console.log("data", data);
        try {
            await createContractVariant(data).unwrap();
            toaster.create({
                title: "Success",
                description: "Contract Variant Created Successfully",
                type: "success",
            })
            refetch();
        } catch {
            toaster.create({
                title: "Failed",
                description: "Contract Variant Created Failed",
                type: "error",
            })
        }
        onCloseCreate();
    }

    const onSubmitEdit = async (data: ContractVariant) => {
        if (!selectedItem) return;
        console.log("data", data);
        console.log("selectedItem", selectedItem);
        try {
            await updateContractVariant({ uuid: selectedItem.uuid, contractVariant: data })
            toaster.create({
                title: "Success",
                description: "Contract Variant Updated Successfully",
                type: "success",
            })
            refetch();
        } catch {
            toaster.create({
                title: "Failed",
                description: "Contract Variant Updated Failed",
                type: "error",
            })
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!selectedItem) return;

        console.log("selectedItem", selectedItem);
        try {
            await removeContractVariant(selectedItem.uuid);
            toaster.create({
                title: "Success",
                description: "Contract Variant Removed Successfully",
                type: "success",
            });
            refetch();
        } catch {
            toaster.create({
                title: "Failed",
                description: "Contract Variant Removal Failed",
                type: "error",
            });
        }
        onCloseRemove();
    };

    const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    return <Box minW="100%" minH="100%">
        <PageWrapper w="100%" h="100%" title="Contract Characteristic (Settings)" description="Manage your contract types" icon={IoGitBranchOutline}>
            <VStack w="100%" h="100%" py="3em">
                <HStack w="100%">
                    <LaunchpadNewButton onClick={onOpenCreate} />
                </HStack>
            </VStack>
            <LaunchpadNameTable items={paginatedItems} pageCount={pageCount} page={page} setPage={setPage} editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })} removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })} />
        </PageWrapper>
        <ContractVariantDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Variant" />
        <ContractVariantDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Variant" />

        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Variant (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </Box>
}

export interface ContractVariantDialogProps {
    open: boolean,
    onClose: () => void,
    title: string,
    onSubmit: (data: ContractVariant) => void
    defaultValues?: ContractVariant;
}

export function ContractVariantDialog({ open, onClose, title, onSubmit, defaultValues }: ContractVariantDialogProps) {
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
            setValue("contractTypeUuid", defaultValues.contractTypeUuid);
        }
    }, [defaultValues, setValue]);

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
                        label="Contract Type"
                        invalid={!!errors.contractTypeUuid}
                        errorText={errors.contractTypeUuid?.message}
                    >
                        <ContractTypeSelect />
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

export function ContractTypeSelect() {
    const { data: contractTypes, error, isLoading } = useGetContractTypesQuery();

    const contractTypesCollection = createListCollection({
        items: contractTypes || [],
        itemToString: (item) => item.name || "",
        itemToValue: (item) => item.uuid,
    });

    return (
        <SelectRoot collection={contractTypesCollection} size="sm" width="320px">
            <SelectLabel>Select pokemon</SelectLabel>
            <SelectTrigger>
                <SelectValueText placeholder="Select Contract Type" />
            </SelectTrigger>
            <SelectContent>
                {contractTypesCollection.items.map((contractType) => (
                    <SelectItem item={contractType} key={contractType.uuid}>
                        {contractType.name}
                    </SelectItem>
                ))}
            </SelectContent>
        </SelectRoot>
    )
}

export function ContractVariantTable() {
    
}