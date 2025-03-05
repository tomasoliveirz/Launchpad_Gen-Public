import { Spinner, useDisclosure, VStack } from "@chakra-ui/react";
import { Text } from "@chakra-ui/react";
import { BlockchainNetwork } from "@/models/BlockchainNetwork";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect } from "react";
import { Toaster } from "@/components/ui/toaster"
import { BlockchainNetworksDialog } from "@/components/launchpad/dialogs/blockchain-network-dialog";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { FaNetworkWired } from "react-icons/fa";
import { DeleteButton, EditButton } from "@/components/launchpad/buttons/button";
import { BlockchainNetworkDetailNavigationItem, pages } from "@/constants/pages";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { DataList } from "@/components/reUIsables/DataList/data-list";

export default function () {
    const URL_SLUG = "BlockchainNetworks";
    const entityApi = useEntity<BlockchainNetwork>(URL_SLUG);
    const { uuid } = useParams();
    const navigate = useNavigate();
    const location = useLocation();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();

    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const [updateBlockchainNetwork] = entityApi.update();
    const [removeBlockchainNetwork] = entityApi.remove();

    const BlockchainNetworkData = data as BlockchainNetwork;

    useEffect(() => {
        refetch();
    }, []);

    const onSubmitEdit = async (BlockchainNetworkData: BlockchainNetwork) => {
        if (!BlockchainNetworkData) return;

        try {
            await updateBlockchainNetwork({ uuid: BlockchainNetworkData.uuid, data: BlockchainNetworkData })
            LaunchpadSuccessToaster("Blockchain Network Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Blockchain Network Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!BlockchainNetworkData) return;

        try {
            await removeBlockchainNetwork(BlockchainNetworkData.uuid);
            navigate(-1);
            LaunchpadSuccessToaster("Blockchain Network Removed Successfully");

        } catch {
            LaunchpadErrorToaster("Blockchain Network Removal Failed");
        }
        onCloseRemove();
    };

    if (isLoading) return <Spinner />;
    if (isError || !BlockchainNetworkData) return <Text>Error loading Blockchain Network</Text>;

    const rightElement = <VStack>
        <EditButton w="100%" onClick={onOpenEdit} />
        <DeleteButton w="100%" onClick={onOpenRemove} />
    </VStack>
 
    const breadcrumbs = getBreadcrumbs(pages, location.pathname, [{
        ...BlockchainNetworkDetailNavigationItem,
        label: BlockchainNetworkData.name ?? "",
        icon: BlockchainNetworkData.image ?? FaNetworkWired
    }]);

    return <PageWrapper title={BlockchainNetworkData.name ?? ""} breadcrumbsProps={{ items: breadcrumbs }} icon={BlockchainNetworkData.image ?? FaNetworkWired} rightSideElement={rightElement}>
        <DataList columns={[["Description", BlockchainNetworkData.description as string]]} item={BlockchainNetworkData} />
        <BlockchainNetworksDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={BlockchainNetworkData} title="Edit Blockchain Network" />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Blockchain Network (${BlockchainNetworkData?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </PageWrapper>
}

