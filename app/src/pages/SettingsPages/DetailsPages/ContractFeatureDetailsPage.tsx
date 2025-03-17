import { Spinner, useDisclosure, VStack } from "@chakra-ui/react";
import { Text } from "@chakra-ui/react";
import { ContractFeature } from "@/models/ContractFeature";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect } from "react";
import { DeleteButton, EditButton } from "@/components/launchpad/buttons/button";
import { ContractFeatureDetailNavigationItem, pages } from "@/constants/pages";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { DataList, DataListItemProps } from "@/components/reUIsables/DataList/data-list";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { BsStars } from "react-icons/bs";
import { EntityDialog, EntityDialogItemProps } from "@/components/launchpad/dialogs/entity-dialog";

export default function () {
    const URL_SLUG = "ContractFeatures";
    const entityApi = useEntity<ContractFeature>(URL_SLUG);
    const { uuid } = useParams();
    const navigate = useNavigate();
    const location = useLocation();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();

    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const [updateContractFeature] = entityApi.update();
    const [removeContractFeature] = entityApi.remove();

    const ContractFeatureData = data as ContractFeature;

    useEffect(() => {
        refetch();
    }, []);

    const onSubmitEdit = async (ContractFeatureData: ContractFeature) => {
        if (!ContractFeatureData) return;

        try {
            await updateContractFeature({ uuid: ContractFeatureData.uuid, data: ContractFeatureData })
            LaunchpadSuccessToaster("Contract Feature Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Feature Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!ContractFeatureData) return;

        try {
            await removeContractFeature(ContractFeatureData.uuid);
            navigate(-1);
            LaunchpadSuccessToaster("Contract Feature Removed Successfully");

        } catch {
            LaunchpadErrorToaster("Contract Feature Removal Failed");
        }
        onCloseRemove();
    };

    if (isLoading) return <Spinner />;
    if (isError || !ContractFeatureData) return <Text>Error loading Contract Feature</Text>;

    const rightElement = <VStack>
        <EditButton w="100%" onClick={onOpenEdit} />
        <DeleteButton w="100%" onClick={onOpenRemove} />
    </VStack>

    const breadcrumbs = getBreadcrumbs(pages, location.pathname, [{
        ...ContractFeatureDetailNavigationItem,
        label: ContractFeatureData.name ?? "",
        icon: BsStars
    }]);

    const columns: DataListItemProps<ContractFeature>[] = [
        {
            dataKey: "description",
            label: "Description"
        },
        {
            dataKey: "dataType",
            label: "DataType"
        },
        {
            dataKey: "defaultValue",
            label: "Default Value"
        },
        {
            dataKey: "options",
            label: "Options"
        }
    ];

    const dialogColumns: EntityDialogItemProps<ContractFeature>[] = [
        {
            dataKey: "name",
            label: "Name",
            dataType: "text"
        },
        {
            dataKey: "description",
            label: "Description",
            dataType: "longText"
        },
        {
            dataKey: "dataType",
            label: "Data Type",
            dataType: "text",
        },
        {
            dataKey: "defaultValue",
            label: "Default Value",
            dataType: "text",
        },
        {
            dataKey: "options",
            label: "Options",
            dataType: "text",
        }
    ];

    return <PageWrapper title={ContractFeatureData.name ?? ""} breadcrumbsProps={{ items: breadcrumbs }} icon={BsStars} rightSideElement={rightElement}>
        <DataList columns={columns} 
            item={ContractFeatureData} />
        <EntityDialog columns={dialogColumns} open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} title="Edit Contract Feature" defaultValues={ContractFeatureData} />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Feature (${ContractFeatureData?.name})`} onSubmit={onSubmitRemove} />
    </PageWrapper>
}


