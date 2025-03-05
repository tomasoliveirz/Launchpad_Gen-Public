import { Spinner, useDisclosure, VStack } from "@chakra-ui/react";
import { Text } from "@chakra-ui/react";
import { ContractCharacteristic } from "@/models/ContractCharacteristic";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect } from "react";
import { Toaster } from "@/components/ui/toaster"
import { FaPalette } from "react-icons/fa";
import { DeleteButton, EditButton } from "@/components/launchpad/buttons/button";
import { ContractCharacteristicDetailNavigationItem, pages } from "@/constants/pages";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { DataList } from "@/components/reUIsables/DataList/data-list";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";

export default function () {
    const URL_SLUG = "ContractCharacteristics";
    const entityApi = useEntity<ContractCharacteristic>(URL_SLUG);
    const { uuid } = useParams();
    const navigate = useNavigate();
    const location = useLocation();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();

    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const [updateContractCharacteristic] = entityApi.update();
    const [removeContractCharacteristic] = entityApi.remove();

    const ContractCharacteristicData = data as ContractCharacteristic;

    useEffect(() => {
        refetch();
    }, []);

    const onSubmitEdit = async (ContractCharacteristicData: ContractCharacteristic) => {
        if (!ContractCharacteristicData) return;

        try {
            await updateContractCharacteristic({ uuid: ContractCharacteristicData.uuid, data: ContractCharacteristicData })
            LaunchpadSuccessToaster("Contract Characteristic Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Characteristic Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!ContractCharacteristicData) return;

        try {
            await removeContractCharacteristic(ContractCharacteristicData.uuid);
            navigate(-1);
            LaunchpadSuccessToaster("Contract Characteristic Removed Successfully");

        } catch {
            LaunchpadErrorToaster("Contract Characteristic Removal Failed");
        }
        onCloseRemove();
    };

    if (isLoading) return <Spinner />;
    if (isError || !ContractCharacteristicData) return <Text>Error loading Contract Characteristic</Text>;

    const rightElement = <VStack>
        <EditButton w="100%" onClick={onOpenEdit} />
        <DeleteButton w="100%" onClick={onOpenRemove} />
    </VStack>

    const breadcrumbs = getBreadcrumbs(pages, location.pathname, [{
        ...ContractCharacteristicDetailNavigationItem,
        label: ContractCharacteristicData.name ?? "",
        icon: FaPalette
    }]);

    return <PageWrapper title={ContractCharacteristicData.name ?? ""} breadcrumbsProps={{ items: breadcrumbs }} icon={FaPalette} rightSideElement={rightElement}>
        <DataList columns={[["Description", ContractCharacteristicData.description as string]]} item={ContractCharacteristicData} />
        <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={ContractCharacteristicData} title="Edit Contract Characteristic" />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Characteristic (${ContractCharacteristicData?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </PageWrapper>
}