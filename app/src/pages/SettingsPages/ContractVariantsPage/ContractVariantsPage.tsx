import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { CharacteristicInContractVariantDialog } from "@/components/launchpad/dialogs/add-characteristic-in-contract-variant-dialog";
import { ContractVariantDialog } from "@/components/launchpad/dialogs/contract-variants-dialog";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { LaunchpadContractVariantTable } from "@/components/launchpad/tables/contract-variants-table";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import EntityTable, { EntityColumnHeaderProps } from "@/components/reUIsables/EntityTable/entity-table";
import { TextModal } from "@/components/reUIsables/Modals/text-modal";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { pages } from "@/constants/pages";
import { CharacteristicInContractVariant } from "@/models/CharacteristicInContractVariant";
import { ContractCharacteristic } from "@/models/ContractCharacteristic";
import { ContractType } from "@/models/ContractType";
import { ContractVariant } from "@/models/ContractVariant";
import { useEntity } from "@/services/launchpad/entityService";
import { Box, createListCollection, HStack, Spacer, useDisclosure , Text} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { FaPencilAlt, FaTrashAlt } from "react-icons/fa";
import { IoGitBranchOutline } from "react-icons/io5";

export default function () {
    const URL_SLUG = "ContractVariants";
    const entityApi = useEntity<ContractVariant>(URL_SLUG);
    const entityApiContractType = useEntity<ContractType>("ContractTypes");
    const entityApiContractCharacteristic = useEntity<ContractCharacteristic>("ContractCharacteristics");
    const entityApiCharacteristicInContractVariant = useEntity<CharacteristicInContractVariant>("CharacteristicInContractVariants");

    const { data = [] as ContractVariant[], refetch } = entityApi.list();
    const { data: contractTypesData = [], refetch: refetchContractTypes } = entityApiContractType.list();
    const contractTypesCollection = createListCollection({
        items: contractTypesData as ContractType[],
    })
    
    const { data: contractCharacteristicsData = [] } = entityApiContractCharacteristic.list();
    const contractCharacteristicsCollection = createListCollection({
        items: contractCharacteristicsData as ContractCharacteristic[],
    })

    const [createContractVariant] = entityApi.create();
    const [updateContractVariant] = entityApi.update();
    const [removeContractVariant] = entityApi.remove();

    const [createCharacteristicInContractVariant] = entityApiCharacteristicInContractVariant.create();

    const contractVariantData = data as ContractVariant[];

    const [selectedItem, setSelectedItem] = useState<ContractVariant | null>(null);
    const [contractTypeUuid, setContractTypeUuid] = useState<string>("");
    const [characteristicUuid, setCharacteristicUuid] = useState<string>("");

    const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
    const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
    const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
    const { onOpen: onOpenAddCharacteristic, onClose: onCloseAddCharacteristic, open: openAddCharacteristic } = useDisclosure();

    useEffect(() => {
        refetch();
        refetchContractTypes();
    }, []);

    const [page, setPage] = useState(1);
    const pageSize = 6;
    const paginatedItems = contractVariantData.slice((page - 1) * pageSize, page * pageSize);
    const pageCount = Math.ceil(data.length / pageSize);

    const onSubmitCreate = async (data: ContractVariant) => {
        try {
            await createContractVariant(data).unwrap();
            LaunchpadSuccessToaster("Contract Variant Created Successfully");
            refetch();
        } catch (error) {
            LaunchpadErrorToaster("Contract Variant Created Failed");
        }
        onCloseCreate();
    }

    const onSubmitEdit = async (data: ContractVariant) => {
        if (!selectedItem) return;

        try {
            await updateContractVariant({ uuid: selectedItem.uuid, data })
            LaunchpadSuccessToaster("Contract Variant Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Variant Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!selectedItem) return;

        try {
            await removeContractVariant(selectedItem.uuid);
            LaunchpadSuccessToaster("Contract Variant Removed Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Contract Variant Removal Failed");
        }
        onCloseRemove();
    };

    const onSubmitAddCharacteristic = async (data: CharacteristicInContractVariant) => {
        try {
            await createCharacteristicInContractVariant(data).unwrap();
            LaunchpadSuccessToaster("Contract Characteristic Added Successfully");
            refetch();
        } catch (error) {
            LaunchpadErrorToaster("Contract Characteristic Added Failed");
        }
        onCloseAddCharacteristic();
    }

    // return <Box minW="100%" minH="100%">
    //     <PageWrapper w="100%" h="100%" title="Contract Variants (Settings)" description="Manage your contract variants" icon={IoGitBranchOutline}>
    //         <TableWrapper newButtonOnClick={onOpenCreate}>
    //             <LaunchpadContractVariantTable
    //                 items={paginatedItems}
    //                 pageCount={pageCount}
    //                 page={page}
    //                 setPage={setPage}
    //                 addCharacteristic={(item => { setSelectedItem(item); onOpenAddCharacteristic(); })}
    //                 editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })}
    //                 removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })}
    //                 variantDetailsLink={(item) => `/settings/contract/variants/${item.uuid}`}
    //                 typeDetailsLink={(item) => `/settings/contract/types/${item.contractType.uuid}`}
    //             />
    //         </TableWrapper>
    //     </PageWrapper>
    //     <ContractVariantDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Variant" collection={contractTypesCollection} selectValue={contractTypeUuid} selectOnValueChange={setContractTypeUuid} />
    //     <ContractVariantDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Variant" collection={contractTypesCollection} selectValue={contractTypeUuid} selectOnValueChange={setContractTypeUuid} />
    //     <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Variant (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
    //     <CharacteristicInContractVariantDialog open={openAddCharacteristic} onClose={onCloseAddCharacteristic} defaultValues={selectedItem || undefined} onSubmit={onSubmitAddCharacteristic} title="Add Characteristic" collection={contractCharacteristicsCollection} selectValue={characteristicUuid} selectOnValueChange={setCharacteristicUuid} />
    // </Box>

      const sideMenu= (t:ContractVariant)=><HStack>
                        <Spacer/>
                        <FaPencilAlt title="Edit" cursor="pointer" onClick={()=>{
                          setSelectedItem(t);
                          onOpenEdit();
                        }}/>
                        <FaTrashAlt title="Delete" cursor="pointer" onClick={()=>{
                          setSelectedItem(t);
                          onOpenRemove();
                        }}/>
                      </HStack>

    const formatDescription = (s?:string|ContractVariant[keyof ContractVariant])=> s ? <TextModal text={s as string} maxCharacters={20}/>:<></>
    
    const columns:EntityColumnHeaderProps<ContractVariant>[] =[{
        dataKey: "name", 
        label:"Name",
        orderable:true,
        searchable:true,
        link:(t:ContractVariant) => t.uuid,
        displayable:true
    },
    {
        dataKey: "description", 
        label:"Description",
        searchable:true,
        formatCell:formatDescription,
        displayable:true,
    },
    {
        dataKey : "contractType",
        label:"Type",
        searchable:true,
        link:(t:ContractVariant)=> "/settings/contract/types/"+t.contractType.uuid,
        formatCell:(t:string|ContractType|undefined)=> typeof(t) === "object" ? <Text>{(t as ContractType).name}</Text>:<></>,
        displayable:true,
    }
    ];
                      
                      
    const breadcrumbs = getBreadcrumbs(pages, location.pathname);

    return <PageWrapper title={"Contract Variants"} icon={IoGitBranchOutline} breadcrumbsProps={{items:breadcrumbs}}>
                <Box w="96%" mt="3em" mx="auto">
                  <EntityTable topLeftElement={<LaunchpadNewButton onClick={onOpenCreate}/>} itemsPerPage={6} searchable columnDescriptions={columns} rowLastColumn={sideMenu} items={data as ContractVariant[]}/>
                </Box>
                <ContractVariantDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Variant" collection={contractTypesCollection} selectValue={contractTypeUuid} selectOnValueChange={setContractTypeUuid} />
                <ContractVariantDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Variant" collection={contractTypesCollection} selectValue={contractTypeUuid} selectOnValueChange={setContractTypeUuid} />
                <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Variant (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
            </PageWrapper>
}