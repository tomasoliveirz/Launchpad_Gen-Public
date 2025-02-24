import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { ContractVariantDialog } from "@/components/launchpad/dialogs/contract-variants-dialog";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-diaolg";
import { LaunchpadContractVariantTable } from "@/components/launchpad/tables/contract-variants-table";
import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { TableWrapper } from "@/components/launchpad/wrappers/table-wrapper";
import { Toaster, toaster } from "@/components/ui/toaster";
import { ContractType } from "@/models/ContractType";
import { ContractVariant } from "@/models/ContractVariant";
import { useEntity } from "@/services/launchpad/testService";
import { Box, createListCollection, HStack, useDisclosure, VStack } from "@chakra-ui/react";
import { useState } from "react";
import { IoGitBranchOutline } from "react-icons/io5";

export default function () {
    const URL_SLUG = "ContractVariants";
    const entityApi = useEntity<ContractVariant>(URL_SLUG);

    const entityApiContractType = useEntity<ContractType>("ContractTypes");
    const { data: contractTypesData = [] } = entityApiContractType.list();
    const contractTypesCollection = createListCollection({
        items: contractTypesData as ContractType[],
    })

    const { data = [], refetch } = entityApi.list();
    const [createContractVariant] = entityApi.create();
    const [updateContractVariant] = entityApi.update();
    const [removeContractVariant] = entityApi.remove();

    const contractVariantData = data as ContractVariant[];

    const [selectedItem, setSelectedItem] = useState<ContractVariant | null>(null);

    const [page, setPage] = useState(1);
    const pageSize = 6;
    const paginatedItems = contractVariantData.slice((page - 1) * pageSize, page * pageSize);
    const pageCount = Math.ceil(data.length / pageSize);
    const [contractTypeUuid, setContractTypeUuid] = useState<string>("");

    const onSubmitCreate = async (data: ContractVariant) => {
        try {
            await createContractVariant(data).unwrap();
            toaster.create({
                title: "Success",
                description: "Contract Variant Created Successfully",
                type: "success",
            })
            refetch();
        } catch (error) {
            console.log("error", error);
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

        try {
            await updateContractVariant({ uuid: selectedItem.uuid, data })
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
        <PageWrapper w="100%" h="100%" title="Contract Variants (Settings)" description="Manage your contract variants" icon={IoGitBranchOutline}>
            <TableWrapper newButtonOnClick={onOpenCreate}>
                <LaunchpadContractVariantTable items={paginatedItems} pageCount={pageCount} page={page} setPage={setPage} editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })} removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })} />
            </TableWrapper>
        </PageWrapper>
        <ContractVariantDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Variant" collection={contractTypesCollection} selectValue={contractTypeUuid} selectOnValueChange={setContractTypeUuid} />
        <ContractVariantDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Variant" collection={contractTypesCollection} selectValue={contractTypeUuid} selectOnValueChange={setContractTypeUuid} />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Variant (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </Box>
}