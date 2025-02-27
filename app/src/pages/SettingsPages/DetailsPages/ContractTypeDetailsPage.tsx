import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { Spinner, useDisclosure } from "@chakra-ui/react";
import { RiFilePaper2Fill } from "react-icons/ri";
import { Text } from "@chakra-ui/react";
import { ContractType } from "@/models/ContractType";
import { useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { EntityDetails } from "@/components/launchpad/details-component/entity-details";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect, useState } from "react";
import { Toaster} from "@/components/ui/toaster"

export default function () {
    const URL_SLUG = "ContractTypes";
    const entityApi = useEntity<ContractType>(URL_SLUG);
    const { uuid } = useParams();

    const navigate = useNavigate();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    const [selectedItem, setSelectedItem] = useState<ContractType | null>(null);

    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const [updateContractType] = entityApi.update();
    const [removeContractType] = entityApi.remove();

    useEffect(() => {
            refetch();
        }, []);

    const contractTypeData = data as ContractType;

    const onSubmitEdit = async (contractTypeData: ContractType) => {
        if (!contractTypeData) return;

        try {
            await updateContractType({ uuid: contractTypeData.uuid, data: contractTypeData })
            LaunchpadSuccessToaster("Contract Type Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Type Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!contractTypeData) return;

        try {
            await removeContractType(contractTypeData.uuid);
            navigate(-1);
            LaunchpadSuccessToaster("Contract Type Removed Successfully");
            
        } catch {
            LaunchpadErrorToaster("Contract Type Removal Failed");
        }
        onCloseRemove();
    };

    if (isLoading) return <Spinner />;
    if (isError || !contractTypeData) return <Text>Error loading contract type</Text>;

    const contractTypeColumns: [string, string][] = [
        ["Name", contractTypeData.name as string],
        ["Description", contractTypeData.description as string],
        ["Description", contractTypeData.description as string]
    ]

    return <PageWrapper title="Contract Type (Details)" icon={RiFilePaper2Fill}>
        <EntityDetails
            columns={contractTypeColumns}
            item={contractTypeData}
            editButtonOnClick={(contractTypeData => { setSelectedItem(contractTypeData); onOpenEdit(); })}
            removeButtonOnClick={(contractTypeData => { setSelectedItem(contractTypeData); onOpenRemove(); })} 
            />
        <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={contractTypeData} title="Edit Contract Type" />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Type (${contractTypeData?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </PageWrapper>
}


