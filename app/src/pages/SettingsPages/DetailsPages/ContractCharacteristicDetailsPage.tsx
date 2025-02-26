import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { Spinner, useDisclosure } from "@chakra-ui/react";
import { RiFilePaper2Fill } from "react-icons/ri";
import { Text } from "@chakra-ui/react";
import { ContractCharacteristic } from "@/models/ContractCharacteristic";
import { useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { EntityDetails } from "@/components/launchpad/details-component/entity-details";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect, useState } from "react";
import { Toaster} from "@/components/ui/toaster"

export default function () {
    const URL_SLUG = "ContractCharacteristics";
    const entityApi = useEntity<ContractCharacteristic>(URL_SLUG);
    const { uuid } = useParams();

    const navigate = useNavigate();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    const [selectedItem, setSelectedItem] = useState<ContractCharacteristic | null>(null);

    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const [updateContractCharacteristic] = entityApi.update();
    const [removeContractCharacteristic] = entityApi.remove();

    useEffect(() => {
            refetch();
        }, []);

    const contractCharacteristicData = data as ContractCharacteristic;
    const onSubmitEdit = async (contractCharacteristicData: ContractCharacteristic) => {
        if (!contractCharacteristicData) return;

        try {
            await updateContractCharacteristic({ uuid: contractCharacteristicData.uuid, data: contractCharacteristicData })
            LaunchpadSuccessToaster("Contract Characteristic Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Characteristic Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!contractCharacteristicData) return;

        try {
            await removeContractCharacteristic(contractCharacteristicData.uuid);
            navigate(-1);
            LaunchpadSuccessToaster("Contract Characteristic Removed Successfully");
            
        } catch {
            LaunchpadErrorToaster("Contract Characteristic Removal Failed");
        }
        onCloseRemove();
    };

    if (isLoading) return <Spinner />;
    if (isError || !contractCharacteristicData) return <Text>Error loading contract characteristic</Text>;

    const contractCharacteristicColumns: [string, string][] = [
        ["Name", contractCharacteristicData.name as string],
        ["Description", contractCharacteristicData.description as string]
    ]

    return <PageWrapper title="Contract Characteristic (Details)" icon={RiFilePaper2Fill}>
        <EntityDetails
            columns={contractCharacteristicColumns}
            item={contractCharacteristicData}
            editButtonOnClick={(contractCharacteristicData => { setSelectedItem(contractCharacteristicData); onOpenEdit(); })}
            removeButtonOnClick={(contractCharacteristicData => { setSelectedItem(contractCharacteristicData); onOpenRemove(); })} 
            />
        <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={contractCharacteristicData} title="Edit Contract Characteristic" />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Characteristic (${contractCharacteristicData?.name})`} onSubmit={onSubmitRemove} />
        <Toaster />
    </PageWrapper>
}


