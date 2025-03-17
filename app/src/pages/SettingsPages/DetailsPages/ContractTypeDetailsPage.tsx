import { Box, BoxProps, createListCollection, Flex, Spinner, useDisclosure, VStack } from "@chakra-ui/react";
import { Text } from "@chakra-ui/react";
import { ContractType } from "@/models/ContractType";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { useEntity } from "@/services/launchpad/entityService";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { useEffect, useState } from "react";
import { DeleteButton, EditButton, LaunchpadButton } from "@/components/launchpad/buttons/button";
import { ContractTypeDetailNavigationItem, pages } from "@/constants/pages";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { DataList, DataListItemProps } from "@/components/reUIsables/DataList/data-list";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { FaPlus, FaScroll } from "react-icons/fa";
import { EntityDialog, EntityDialogItemProps } from "@/components/launchpad/dialogs/entity-dialog";
import { FeatureInContractType } from "@/models/FeatureInContractType";
import { TextModal } from "@/components/reUIsables/Modals/text-modal";
import { FaXmark } from "react-icons/fa6";
import { ContractFeature } from "@/models/ContractFeature";
import { featureInTypeApi } from "@/services/launchpad/featuresInContractTyprService";
import { FeatureInContractTypeDialog } from "@/components/launchpad/dialogs/add-feature-in-contract-type-dialog";

export default function () {
    const URL_SLUG = "ContractTypes";
    const entityApi = useEntity<ContractType>(URL_SLUG);

    const entityApiContractFeature = useEntity<ContractFeature>("ContractFeatures");
    const entityApiFeatureInContractType = useEntity<FeatureInContractType>("FeatureInContractType");
    const { uuid } = useParams();
    const navigate = useNavigate();
    const location = useLocation();

    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    const { onOpen: onOpenAddFeature, onClose: onCloseAddFeature, open: openAddFeature } = useDisclosure();
    const { onOpen: onOpenRemoveFeatureInType, onClose: onCloseRemoveFeatureInType, open: openRemoveFeatureInType } = useDisclosure();


    const { data, isLoading, isError, refetch } = entityApi.get(uuid!);
    const [updateContractType] = entityApi.update();
    const [removeContractType] = entityApi.remove();

    const [createFeatureInContractType] = entityApiFeatureInContractType.create();
    const [removeFeatureInContractType] = entityApiFeatureInContractType.remove();

    const { data: FeatureInTypeData = [], refetch: refetchFeatures } = featureInTypeApi.useGetFeaturesByTypeUuidQuery({ contractTypeUuid: uuid! });

    const { data: contractFeaturesData = [] } = entityApiContractFeature.list();
    const contractFeaturesCollection = createListCollection({
        items: contractFeaturesData as ContractFeature[],
    })

    const [featureUuid, setFeatureUuid] = useState<string>("");
    const [selectedFeatureInType, setSelectedFeatureInType] = useState<FeatureInContractType | null>(null);

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

    const onSubmitAddFeature = async (data: FeatureInContractType) => {
        try {
            await createFeatureInContractType(data).unwrap();
            LaunchpadSuccessToaster("Contract Feature Added Successfully");
            refetchFeatures();
        } catch (error) {
            LaunchpadErrorToaster("Contract Feature Added Failed");
        }
        onCloseAddFeature();
    }
    const onSubmitRemoveFeatureInType = async () => {
        if (!selectedFeatureInType) return;
        try {
            await removeFeatureInContractType(selectedFeatureInType.uuid);
            LaunchpadSuccessToaster("Feature Removed Successfully");
            refetchFeatures();
        } catch {
            LaunchpadErrorToaster("Feature Removal Failed");
        }
        onCloseRemoveFeatureInType();
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

    const dialogColumns: EntityDialogItemProps<ContractType>[] = [
        {
            dataKey: "name",
            label: "Name",
            dataType: "text"
        },
        {
            dataKey: "description",
            label: "Description",
            dataType: "longText"
        }
    ];

    return <PageWrapper title={ContractTypeData.name ?? ""} breadcrumbsProps={{ items: breadcrumbs }} icon={FaScroll} rightSideElement={rightElement}>
        <DataList columns={columns} item={ContractTypeData} />
        <FeaturesInContractType contractType={ContractTypeData} data={FeatureInTypeData} selectedItem={selectedFeatureInType} addFeature={() => onOpenAddFeature()} open={openRemoveFeatureInType} onClose={onCloseRemoveFeatureInType} onSubmit={onSubmitRemoveFeatureInType} removeButtonOnClick={(feature) => {
            setSelectedFeatureInType(feature);
            onOpenRemoveFeatureInType();
        }} />
        <EntityDialog columns={dialogColumns} open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} title="Edit Contract Type" defaultValues={ContractTypeData} />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Type (${ContractTypeData?.name})`} onSubmit={onSubmitRemove} />
        <FeatureInContractTypeDialog open={openAddFeature} onClose={onCloseAddFeature} defaultValues={ContractTypeData} onSubmit={onSubmitAddFeature} title="Add Feature" collection={contractFeaturesCollection} selectValue={featureUuid} selectOnValueChange={setFeatureUuid} />
    </PageWrapper>
}

export interface FeaturesInContractTypeProps extends BoxProps {
    contractType: ContractType;
    data: FeatureInContractType[];
    selectedItem: FeatureInContractType | null;
    addFeature?: (item: ContractType) => void;
    removeButtonOnClick?: (item: FeatureInContractType) => void;
    open: boolean;
    onClose: () => void;
    onSubmit: () => void;
}

export function FeaturesInContractType({ contractType, selectedItem, addFeature, removeButtonOnClick, open, onClose, onSubmit, data, ...props }: FeaturesInContractTypeProps) {
    return (
        <Box p="1em" {...props}>
            <Text fontSize="md" mr="0.5em" fontWeight="bold">Features</Text>
            <LaunchpadButton
                onClick={() => addFeature?.(contractType)}
                icon={FaPlus}
                text="Add Feature"
                color="white"
                bg="none"
                p="0"
                fontSize="sm"
                border="none"
                _hover={{ border: "none" }}
                _active={{ border: "none" }}
                _focus={{ boxShadow: "none", outline: "none" }}
            />
            <Box {...props}>
                <Flex wrap="wrap" gap="0.5em">
                    {data.map((feature) => (
                        <Box
                            key={feature.uuid}
                            bg="bg.secondary"
                            display="inline-flex"
                            alignItems="center"
                            minWidth="fit-content"
                            maxWidth="100%"
                            p="0.2em"
                        >
                            <TextModal
                                text={`${feature.contractFeature.name} - ${feature.contractFeature.description}`}
                                maxCharacters={(feature.contractFeature.name!.length + 3)}
                                fontSize="sm"
                                ps="0.5em"
                                whiteSpace="nowrap"
                            />
                            <LaunchpadButton
                                onClick={() => removeButtonOnClick?.(feature)}
                                icon={FaXmark}
                                color="white"
                                bg="none"
                                p="0"
                                h="2em"
                                fontSize="xs"
                                border="none"
                                _hover={{ border: "none" }}
                                _active={{ border: "none" }}
                                _focus={{ boxShadow: "none", outline: "none" }}
                            />
                        </Box>
                    ))}
                </Flex>
                <DeleteConfirmationDialog
                    open={open}
                    onClose={onClose}
                    title={`Delete Feature In Type (${selectedItem?.contractFeature.name})`}
                    onSubmit={onSubmit}
                />
            </Box>
        </Box>
    );
}