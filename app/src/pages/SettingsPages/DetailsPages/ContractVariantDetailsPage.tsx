import { Box, BoxProps, createListCollection, Flex, Spinner, Theme, useDisclosure, VStack } from "@chakra-ui/react";
import { Text } from "@chakra-ui/react";
import { ContractVariant } from "@/models/ContractVariant";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect, useState } from "react";
import { DeleteButton, EditButton, LaunchpadButton } from "@/components/launchpad/buttons/button";
import { ContractVariantDetailNavigationItem, pages } from "@/constants/pages";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { DataList } from "@/components/reUIsables/DataList/data-list";
import { ContractVariantDialog } from "@/components/launchpad/dialogs/contract-variants-dialog";
import { ContractType } from "@/models/ContractType";
import { IoGitBranchOutline } from "react-icons/io5";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { FaPlus } from "react-icons/fa";
import { CharacteristicInContractVariantDialog } from "@/components/launchpad/dialogs/add-characteristic-in-contract-variant-dialog";
import { CharacteristicInContractVariant } from "@/models/CharacteristicInContractVariant";
import { ContractCharacteristic } from "@/models/ContractCharacteristic";
import { characteristicInVariantApi } from "@/services/launchpad/characteristicsInContractVariantService";
import { FaXmark } from "react-icons/fa6";

export default function () {
    const URL_SLUG = "ContractVariants";
    const entityApi = useEntity<ContractVariant>(URL_SLUG);
    const entityApiContractType = useEntity<ContractType>("ContractTypes");
    const entityApiContractCharacteristic = useEntity<ContractCharacteristic>("ContractCharacteristics");
    const entityApiCharacteristicInContractVariant = useEntity<CharacteristicInContractVariant>("CharacteristicInContractVariants");
    const { data: ContractTypesData = [] } = entityApiContractType.list();
    const ContractTypesCollection = createListCollection({
        items: ContractTypesData as ContractType[],
    })

    const { uuid } = useParams();
    const navigate = useNavigate();
    const location = useLocation();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    const { onOpen: onOpenAddCharacteristic, onClose: onCloseAddCharacteristic, open: openAddCharacteristic } = useDisclosure();
    const { onOpen: onOpenRemoveCharacteristicInVariant, onClose: onCloseRemoveCharacteristicInVariant, open: openRemoveCharacteristicInVariant } = useDisclosure();



    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const ContractVariantData = data as ContractVariant;
    const [updateContractVariant] = entityApi.update();
    const [removeContractVariant] = entityApi.remove();

    const [createCharacteristicInContractVariant] = entityApiCharacteristicInContractVariant.create();
    const [removeCharacteristicInContractVariant] = entityApiCharacteristicInContractVariant.remove();

    const { data: CharacteristicsInVariantData = [], refetch: refetchCharacteristics } = characteristicInVariantApi.useGetCharacteristicsByVariantUuidQuery({ contractVariantUuid: ContractVariantData?.uuid });

    const { data: contractCharacteristicsData = [] } = entityApiContractCharacteristic.list();
    const contractCharacteristicsCollection = createListCollection({
        items: contractCharacteristicsData as ContractCharacteristic[],
    })

    const [characteristicUuid, setCharacteristicUuid] = useState<string>("");
    const [selectedCharacteristicInVariant, setSelectedCharacteristicInVariant] = useState<CharacteristicInContractVariant | null>(null);

    const [ContractTypeUuid, setContractTypeUuid] = useState<string>();

    useEffect(() => {
        refetch();
        refetchCharacteristics();
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

    const onSubmitAddCharacteristic = async (data: CharacteristicInContractVariant) => {
        try {
            await createCharacteristicInContractVariant(data).unwrap();
            LaunchpadSuccessToaster("Contract Characteristic Added Successfully");
            refetchCharacteristics();
        } catch (error) {
            LaunchpadErrorToaster("Contract Characteristic Added Failed");
        }
        onCloseAddCharacteristic();
    }
    const onSubmitRemoveCharacteristicInVariant = async () => {
        if (!selectedCharacteristicInVariant) return;
        try {
            await removeCharacteristicInContractVariant(selectedCharacteristicInVariant.uuid);
            LaunchpadSuccessToaster("Characteristic Removed Successfully");
            refetchCharacteristics();
        } catch {
            LaunchpadErrorToaster("Characteristic Removal Failed");
        }
        onCloseRemoveCharacteristicInVariant();
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

    return <PageWrapper title={ContractVariantData.name ?? ""} breadcrumbsProps={{ items: breadcrumbs }} icon={IoGitBranchOutline} rightSideElement={rightElement}>
        <DataList columns={[
            ["Description", ContractVariantData.description as string],
            ["Contract Type", ContractVariantData.contractType.name as string, `/settings/contract/types/${ContractVariantData.contractType.uuid}`]]}
            item={ContractVariantData}
        />
        <CharacteristicsInContractVariant contractVariant={ContractVariantData} data={CharacteristicsInVariantData} addCharacteristic={() => onOpenAddCharacteristic()} open={openRemoveCharacteristicInVariant} onClose={onCloseRemoveCharacteristicInVariant} onSubmit={onSubmitRemoveCharacteristicInVariant} removeButtonOnClick={(characteristic) => {
            setSelectedCharacteristicInVariant(characteristic);
            onOpenRemoveCharacteristicInVariant();
        }} />
        <ContractVariantDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={ContractVariantData} title="Edit Contract Variant" collection={ContractTypesCollection} selectValue={ContractTypeUuid} selectOnValueChange={setContractTypeUuid} />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Variant (${ContractVariantData?.name})`} onSubmit={onSubmitRemove} />
        <CharacteristicInContractVariantDialog open={openAddCharacteristic} onClose={onCloseAddCharacteristic} defaultValues={ContractVariantData} onSubmit={onSubmitAddCharacteristic} title="Add Characteristic" collection={contractCharacteristicsCollection} selectValue={characteristicUuid} selectOnValueChange={setCharacteristicUuid} />
    </PageWrapper>
}


export interface CharacteristicsInContractVariantProps extends BoxProps {
    contractVariant: ContractVariant;
    data: CharacteristicInContractVariant[];
    addCharacteristic?: (item: ContractVariant) => void;
    removeButtonOnClick?: (item: CharacteristicInContractVariant) => void;
    open: boolean;
    onClose: () => void;
    onSubmit: () => void;
}
export function CharacteristicsInContractVariant({ contractVariant, addCharacteristic, removeButtonOnClick, open, onClose, onSubmit, data, ...props }: CharacteristicsInContractVariantProps) {
    return <Box p="1em" {...props}>
        <Text fontSize="xl" mr="0.5em" fontWeight="bold">Characteristics</Text>
        <LaunchpadButton onClick={() => addCharacteristic?.(contractVariant)} icon={FaPlus} text="Add Characteristic" color="white" bg="none" p="0" fontSize="sm"
            border="none"
            _hover={{ border: "none" }}
            _active={{ border: "none" }}
            _focus={{ boxShadow: "none", outline: "none" }}
        />
        <Box {...props}>
            <Flex>
                {data.map((characteristic) => (
                    <Box me="1em" w="6em" key={characteristic.uuid} bg="bg.secondary">
                        <Flex justifyContent="space-between">
                            <Text title={characteristic.contractCharacteristic.description} fontSize="md" ps="0.5em">{characteristic.contractCharacteristic.name}</Text>
                            <LaunchpadButton onClick={() => removeButtonOnClick?.(characteristic)} icon={FaXmark} color="white" bg="none" p="0" h="2em" fontSize="xs" border="none"
                                _hover={{ border: "none" }}
                                _active={{ border: "none" }}
                                _focus={{ boxShadow: "none", outline: "none" }} />
                        </Flex>
                        <DeleteConfirmationDialog open={open} onClose={onClose} title={`Delete Contract Variant (${characteristic.contractCharacteristic.name})`} onSubmit={onSubmit} />
                    </Box>
                ))}
            </Flex>
        </Box>
    </Box>
}
