import { Spinner, useDisclosure, VStack } from "@chakra-ui/react";
import { Text } from "@chakra-ui/react";
import { ContractType } from "@/models/ContractType";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect } from "react";
import { DeleteButton, EditButton } from "@/components/launchpad/buttons/button";
import { ContractTypeDetailNavigationItem, pages } from "@/constants/pages";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { DataList, DataListItemProps } from "@/components/reUIsables/DataList/data-list";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { FaScroll } from "react-icons/fa";

export default function () {
    const URL_SLUG = "ContractTypes";
    const entityApi = useEntity<ContractType>(URL_SLUG);
    const { uuid } = useParams();
    const navigate = useNavigate();
    const location = useLocation();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();

    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const [updateContractType] = entityApi.update();
    const [removeContractType] = entityApi.remove();

    const ContractTypeData = data as ContractType;

    useEffect(() => {
        refetch();
    }, []);

    const onSubmitEdit = async (ContractTypeData: ContractType) => {
        if (!ContractTypeData) return;

        try {
            await updateContractType({ uuid: ContractTypeData.uuid, data: ContractTypeData })
            LaunchpadSuccessToaster("Contract Type Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Type Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!ContractTypeData) return;

        try {
            await removeContractType(ContractTypeData.uuid);
            navigate(-1);
            LaunchpadSuccessToaster("Contract Type Removed Successfully");

        } catch {
            LaunchpadErrorToaster("Contract Type Removal Failed");
        }
        onCloseRemove();
    };

    if (isLoading) return <Spinner />;
    if (isError || !ContractTypeData) return <Text>Error loading Contract Type</Text>;

    const rightElement = <VStack>
        <EditButton w="100%" onClick={onOpenEdit} />
        <DeleteButton w="100%" onClick={onOpenRemove} />
    </VStack>


    const breadcrumbs = getBreadcrumbs(pages, location.pathname, [{
        ...ContractTypeDetailNavigationItem,
        label: ContractTypeData.name ?? "",
        icon: FaScroll
    }]);

    const columns: DataListItemProps<ContractType>[] = [{
        dataKey: "description",
        label: "Description",
    }];
    
    return <PageWrapper title={ContractTypeData.name ?? ""} breadcrumbsProps={{ items: breadcrumbs }} icon={FaScroll} rightSideElement={rightElement}>
        <DataList columns={columns} item={ContractTypeData} />
        <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={ContractTypeData} title="Edit Contract Type" />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Type (${ContractTypeData?.name})`} onSubmit={onSubmitRemove} />
    </PageWrapper>
}