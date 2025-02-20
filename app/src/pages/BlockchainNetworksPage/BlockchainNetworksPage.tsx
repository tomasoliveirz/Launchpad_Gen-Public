import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, HStack, useDisclosure, VStack } from "@chakra-ui/react"
import { FaNetworkWired} from "react-icons/fa"
import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { Toaster, toaster } from "@/components/ui/toaster"
import { useState } from "react";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-diaolg";
import { useEntity } from "@/services/launchpad/testService";
import { BlockchainNetwork } from "@/models/BlockchainNetwork";
import { BlockchainNetworksTable } from "@/components/launchpad/tables/blockchain-networks-table";
import { BlockchainNetworksDialog } from "@/components/launchpad/dialogs/blockchain-network-dialog";

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
            toaster.create({
                title: "Success",
                description: "Blockchain Network Created Successfully",
                type: "success",
            })
            refetch();
        } catch {
            toaster.create({
                title: "Failed",
                description: "Blockchain Network Created Failed",
                type: "error",
            })
        }
        onCloseCreate();
    }

    const onSubmitEdit = async (data: BlockchainNetwork) => {
        if (!selectedItem) return;

        try {
            await updateBlockchainNetwork({ uuid: selectedItem.uuid, data })
            toaster.create({
                title: "Success",
                description: "Blockchain Network Updated Successfully",
                type: "success",
            })
            refetch();
        } catch {
            toaster.create({
                title: "Failed",
                description: "Blockchain Network Updated Failed",
                type: "error",
            })
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!selectedItem) return;

        try {
            await removeBlockchainNetwork(selectedItem.uuid);
            toaster.create({
                title: "Success",
                description: "Blockchain Network Removed Successfully",
                type: "success",
            });
            refetch();
        } catch {
            toaster.create({
                title: "Failed",
                description: "Blockchain Network Removal Failed",
                type: "error",
            });
        }
        onCloseRemove();
    };

    const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    return <Box minW="100%" minH="100%">
        <PageWrapper w="100%" h="100%" title="Blockchain Network (Settings)" description="Manage your blockchain networks" icon={FaNetworkWired}>
            <VStack w="100%" h="100%" py="3em">
                <HStack w="100%">
                    <LaunchpadNewButton onClick={onOpenCreate} />
                </HStack>
            </VStack>
            <BlockchainNetworksTable items={paginatedItems} pageCount={pageCount} page={page} setPage={setPage} editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })} removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })} />
        </PageWrapper>
        <BlockchainNetworksDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Blockchain Network" />
        <BlockchainNetworksDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} title="New Blockchain Network" defaultValues={selectedItem || undefined} />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Blockchain Network (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </Box>
}

