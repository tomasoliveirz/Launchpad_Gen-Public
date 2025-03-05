import { createListCollection, Spinner, useDisclosure, VStack } from "@chakra-ui/react";
import { Text } from "@chakra-ui/react";
import { ContractVariant } from "@/models/ContractVariant";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect, useState } from "react";
import { Toaster } from "@/components/ui/toaster"
import { DetailWrapper } from "@/components/reUIsables/DetailWrapper/detail-wrapper";
import { DeleteButton, EditButton } from "@/components/launchpad/buttons/button";
import { ContractVariantDetailNavigationItem, pages } from "@/constants/pages";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { DataList } from "@/components/reUIsables/DataList/data-list";
import { ContractVariantDialog } from "@/components/launchpad/dialogs/contract-variants-dialog";
import { ContractType } from "@/models/ContractType";
import { IoGitBranchOutline } from "react-icons/io5";

export default function () {
    const URL_SLUG = "ContractVariants";
    const entityApi = useEntity<ContractVariant>(URL_SLUG);
    const entityApiContractType = useEntity<ContractType>("ContractTypes");
    const { data: ContractTypesData = [] } = entityApiContractType.list();
    const ContractTypesCollection = createListCollection({
        items: ContractTypesData as ContractType[],
    })

    const { uuid } = useParams();
    const navigate = useNavigate();
    const location = useLocation();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();

    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const [updateContractVariant] = entityApi.update();
    const [removeContractVariant] = entityApi.remove();

    const ContractVariantData = data as ContractVariant;
    const [ContractTypeUuid, setContractTypeUuid] = useState<string>();

    useEffect(() => {
        refetch();
    }, []);

    const onSubmitEdit = async (ContractVariantData: ContractVariant) => {
        if (!ContractVariantData) return;

        try {
            await updateContractVariant({ uuid: ContractVariantData.uuid, data: ContractVariantData })
            LaunchpadSuccessToaster("Contract Variant Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Variant Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!ContractVariantData) return;

        try {
            await removeContractVariant(ContractVariantData.uuid);
            navigate(-1);
            LaunchpadSuccessToaster("Contract Variant Removed Successfully");

        } catch {
            LaunchpadErrorToaster("Contract Variant Removal Failed");
        }
        onCloseRemove();
    };

    if (isLoading) return <Spinner />;
    if (isError || !ContractVariantData) return <Text>Error loading Contract Variant</Text>;

    const rightElement = <VStack>
        <EditButton w="100%" onClick={onOpenEdit} />
        <DeleteButton w="100%" onClick={onOpenRemove} />
    </VStack>

    const breadcrumbs = getBreadcrumbs(pages, location.pathname, [{
        ...ContractVariantDetailNavigationItem,
        label: ContractVariantData.name ?? "",
        icon: IoGitBranchOutline
    }]);

    return <DetailWrapper title={ContractVariantData.name ?? ""} breadcrumbsProps={{ items: breadcrumbs }} icon={IoGitBranchOutline} rightSideElement={rightElement}>
        <DataList columns={[
            ["Contract Type", ContractVariantData.contractType.name as string, `/settings/contract/types/${ContractVariantData.contractType.uuid}`],
            ["Description", ContractVariantData.description as string]]} item={ContractVariantData} />
        <ContractVariantDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={ContractVariantData} title="Edit Contract Variant" collection={ContractTypesCollection} selectValue={ContractTypeUuid} selectOnValueChange={setContractTypeUuid} />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Variant (${ContractVariantData?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </DetailWrapper>
}