import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { Box, useDisclosure } from "@chakra-ui/react";
import { FaNetworkWired } from "react-icons/fa";
import { Toaster, toaster } from "@/components/ui/toaster";
import { useEffect, useState } from "react";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { useEntity } from "@/services/launchpad/entityService";
import { BlockchainNetwork } from "@/models/BlockchainNetwork";
import { BlockchainNetworksTable } from "@/components/launchpad/tables/blockchain-networks-table";
import { BlockchainNetworksDialog } from "@/components/launchpad/dialogs/blockchain-network-dialog";
import { TableWrapper } from "@/components/launchpad/wrappers/table-wrapper";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";

export default function () {
    const URL_SLUG = "BlockchainNetworks";
    const entityApi = useEntity<BlockchainNetwork>(URL_SLUG);

    const { data = [], error, isLoading, refetch } = entityApi.list();

    const [createBlockchainNetwork] = entityApi.create();
    const [updateBlockchainNetwork] = entityApi.update();
    const [removeBlockchainNetwork] = entityApi.remove();

    const BlockchainNetworkData = data as BlockchainNetwork[];

    const [selectedItem, setSelectedItem] = useState<BlockchainNetwork | null>(null);

    const [page, setPage] = useState(1);
    const pageSize = 6;
    const paginatedItems = BlockchainNetworkData.slice((page - 1) * pageSize, page * pageSize);
    const pageCount = Math.ceil(data.length / pageSize);

    const onSubmitCreate = async (data: BlockchainNetwork) => {
        try {
            await createBlockchainNetwork(data).unwrap();
            LaunchpadSuccessToaster("Blockchain Network Created Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Blockchain Network Created Failed");
        }
        onCloseCreate();
    }

    const onSubmitEdit = async (data: BlockchainNetwork) => {
        if (!selectedItem) return;

        try {
            await updateBlockchainNetwork({ uuid: selectedItem.uuid, data })
            LaunchpadSuccessToaster("Blockchain Network Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Blockchain Network Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!selectedItem) return;

        try {
            await removeBlockchainNetwork(selectedItem.uuid);
            LaunchpadSuccessToaster("Blockchain Network Removed Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Blockchain Network Removal Failed");
        }
        onCloseRemove();
    };

    useEffect(() => {
        refetch();
    }, []);

    const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    return <Box minW="100%" minH="100%">
        <PageWrapper w="100%" h="100%" title="Blockchain Network (Settings)" description="Manage your blockchain networks" icon={FaNetworkWired}>
            <TableWrapper newButtonOnClick={onOpenCreate}>
                <BlockchainNetworksTable
                    items={paginatedItems}
                    pageCount={pageCount}
                    page={page}
                    setPage={setPage}
                    editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })}
                    removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })}
                    detailsLink={(item) => `/settings/blockchain/networks/${item.uuid}`}
                />
            </TableWrapper>
        </PageWrapper>
        <BlockchainNetworksDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Blockchain Network" />
        <BlockchainNetworksDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} title="New Blockchain Network" defaultValues={selectedItem || undefined} />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Blockchain Network (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </Box>
}

