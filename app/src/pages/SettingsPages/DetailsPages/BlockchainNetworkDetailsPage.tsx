import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { Button, Spinner, useDisclosure, VStack } from "@chakra-ui/react";
import { RiFilePaper2Fill } from "react-icons/ri";
import { Text } from "@chakra-ui/react";
import { BlockchainNetwork } from "@/models/BlockchainNetwork";
import { useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { EntityDetails } from "@/components/launchpad/details-component/entity-details";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect, useState } from "react";
import { Toaster } from "@/components/ui/toaster"
import { BlockchainNetworksDialog } from "@/components/launchpad/dialogs/blockchain-network-dialog";
import { DetailWrapper } from "@/components/reUIsables/DetailWrapper/detail-wrapper";
import { FaNetworkWired } from "react-icons/fa";
import { DeleteButton, EditButton } from "@/components/launchpad/buttons/button";

export default function () {
    const URL_SLUG = "BlockchainNetworks";
    const entityApi = useEntity<BlockchainNetwork>(URL_SLUG);
    const { uuid } = useParams();
    const navigate = useNavigate();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    const [selectedItem, setSelectedItem] = useState<BlockchainNetwork | null>(null);
    
    

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

    return <DetailWrapper title={BlockchainNetworkData.name??""} icon={BlockchainNetworkData.image??FaNetworkWired} rightSideElement={<EditDeleteStack/>}>
        <EntityDetails
            columns={[
                ["Name", BlockchainNetworkData.name as string],
                ["Description", BlockchainNetworkData.description as string],
                ["Image", BlockchainNetworkData.image as string, undefined, true],
            ]}
            item={BlockchainNetworkData}
            editButtonOnClick={(BlockchainNetworkData => { setSelectedItem(BlockchainNetworkData); onOpenEdit(); })}
            removeButtonOnClick={(BlockchainNetworkData => { setSelectedItem(BlockchainNetworkData); onOpenRemove(); })}
        />
        <BlockchainNetworksDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={BlockchainNetworkData} title="Edit Blockchain Network" />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Blockchain Network (${BlockchainNetworkData?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </DetailWrapper>
}

function EditDeleteStack()
{
    return <VStack>
                <EditButton/>
                <DeleteButton/>
    </VStack>
}