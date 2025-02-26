import { ContractVariantDialog } from "@/components/launchpad/dialogs/contract-variants-dialog";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadContractVariantTable } from "@/components/launchpad/tables/contract-variants-table";
import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { TableWrapper } from "@/components/launchpad/wrappers/table-wrapper";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { Toaster, } from "@/components/ui/toaster";
import { ContractType } from "@/models/ContractType";
import { ContractVariant } from "@/models/ContractVariant";
import { useEntity } from "@/services/launchpad/entityService";
import { Box, createListCollection, useDisclosure } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { IoGitBranchOutline } from "react-icons/io5";

export default function () {
    const URL_SLUG = "ContractVariants";
    const entityApi = useEntity<ContractVariant>(URL_SLUG);
    const entityApiContractType = useEntity<ContractType>("ContractTypes");

    const { data = [] as ContractVariant[], refetch } = entityApi.list();
    const { data: contractTypesData = [], refetch: refetchContractTypes } = entityApiContractType.list();
    const contractTypesCollection = createListCollection({
        items: contractTypesData as ContractType[],
    })

    const [createContractVariant] = entityApi.create();
    const [updateContractVariant] = entityApi.update();
    const [removeContractVariant] = entityApi.remove();

    const contractVariantData = data as ContractVariant[];

    const [selectedItem, setSelectedItem] = useState<ContractVariant | null>(null);
    const [contractTypeUuid, setContractTypeUuid] = useState<string>("");

    const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();

    useEffect(() => {
        refetch();
        refetchContractTypes();
    }, []);

    const [page, setPage] = useState(1);
    const pageSize = 6;
    const paginatedItems = contractVariantData.slice((page - 1) * pageSize, page * pageSize);
    const pageCount = Math.ceil(data.length / pageSize);

    const onSubmitCreate = async (data: ContractVariant) => {
        console.log
        try {
            await createContractVariant(data).unwrap();
            LaunchpadSuccessToaster("Contract Variant Created Successfully");
            refetch();
        } catch (error) {
            LaunchpadErrorToaster("Contract Variant Created Failed");
        }
        onCloseCreate();
    }

    const onSubmitEdit = async (data: ContractVariant) => {
        if (!selectedItem) return;

        try {
            await updateContractVariant({ uuid: selectedItem.uuid, data })
            LaunchpadSuccessToaster("Contract Variant Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Variant Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!selectedItem) return;

        try {
            await removeContractVariant(selectedItem.uuid);
            LaunchpadSuccessToaster("Contract Variant Removed Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Variant Removal Failed");
        }
        onCloseRemove();
    };

    return <Box minW="100%" minH="100%">
        <PageWrapper w="100%" h="100%" title="Contract Variants (Settings)" description="Manage your contract variants" icon={IoGitBranchOutline}>
            <TableWrapper newButtonOnClick={onOpenCreate}>
                <LaunchpadContractVariantTable
                    items={paginatedItems}
                    pageCount={pageCount}
                    page={page}
                    setPage={setPage}
                    editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })}
                    removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })}
                    variantDetailsLink={(item) => `/settings/contract/variants/${item.uuid}`}
                    typeDetailsLink={(item) => `/settings/contract/types/${item.contractType.uuid}`}
                />
            </TableWrapper>
        </PageWrapper>
        <ContractVariantDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Variant" collection={contractTypesCollection} selectValue={contractTypeUuid} selectOnValueChange={setContractTypeUuid} />
        <ContractVariantDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Variant" collection={contractTypesCollection} selectValue={contractTypeUuid} selectOnValueChange={setContractTypeUuid} />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Variant (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </Box>
}