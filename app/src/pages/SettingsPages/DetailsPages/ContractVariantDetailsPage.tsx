import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { createListCollection, Spinner, useDisclosure } from "@chakra-ui/react";
import { RiFilePaper2Fill } from "react-icons/ri";
import { Text } from "@chakra-ui/react";
import { ContractVariant } from "@/models/ContractVariant";
import { useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { EntityDetails } from "@/components/launchpad/details-component/entity-details";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect, useState } from "react";
import { Toaster } from "@/components/ui/toaster"
import { ContractVariantDialog } from "@/components/launchpad/dialogs/contract-variants-dialog";
import { ContractType } from "@/models/ContractType";

export default function () {

    const URL_SLUG = "ContractVariants";
    const entityApi = useEntity<ContractVariant>(URL_SLUG);
    const entityApiContractType = useEntity<ContractType>("ContractTypes");
    const { data: contractTypesData = [] } = entityApiContractType.list();
    const contractTypesCollection = createListCollection({
        items: contractTypesData as ContractType[],
    })
    const { uuid } = useParams();

    const navigate = useNavigate();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    const [selectedItem, setSelectedItem] = useState<ContractVariant | null>(null);
    const [contractTypeUuid, setContractTypeUuid] = useState<string>();

    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const [updateContractVariant] = entityApi.update();
    const [removeContractVariant] = entityApi.remove();

    useEffect(() => {
            refetch();
        }, []);

    const contractVariantData = data as ContractVariant;

    const onSubmitEdit = async (contractVariantData: ContractVariant) => {
        if (!contractVariantData) return;

        try {
            await updateContractVariant({ uuid: contractVariantData.uuid, data: contractVariantData })
            LaunchpadSuccessToaster("Contract Variant Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Variant Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!contractVariantData) return;

        try {
            await removeContractVariant(contractVariantData.uuid);
            navigate(-1);
            LaunchpadSuccessToaster("Contract Variant Removed Successfully");

        } catch {
            LaunchpadErrorToaster("Contract Variant Removal Failed");
        }
        onCloseRemove();
    };

    if (isLoading) return <Spinner />;
    if (isError || !contractVariantData) return <Text>Error loading contract variant</Text>;

    return <PageWrapper title="Contract Variant (Details)" icon={RiFilePaper2Fill}>
        <EntityDetails
            columns={[
                ["Name", contractVariantData.name as string],
                ["Description", contractVariantData.description as string],
                ["Contract Type", contractVariantData.contractType.name as string, `/settings/contract/types/${contractVariantData.contractType.uuid}`]
            ]}
            item={contractVariantData}
            editButtonOnClick={(contractVariantData => { setSelectedItem(contractVariantData); onOpenEdit(); })}
            removeButtonOnClick={(contractVariantData => { setSelectedItem(contractVariantData); onOpenRemove(); })}
        />
        <ContractVariantDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={contractVariantData} title="Edit Contract Variant" collection={contractTypesCollection} selectValue={contractTypeUuid} selectOnValueChange={setContractTypeUuid} />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Variant (${contractVariantData?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </PageWrapper>
}


