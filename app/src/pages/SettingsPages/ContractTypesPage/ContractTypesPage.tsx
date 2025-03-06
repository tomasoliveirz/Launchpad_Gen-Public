import { HStack, Spacer, useDisclosure, Text, Box } from "@chakra-ui/react"
import { useEffect, useState } from "react";
import { ContractType } from "@/models/ContractType";
import { useEntity } from "@/services/launchpad/entityService";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { pages } from "@/constants/pages";
import EntityTable, { EntityColumnHeaderProps } from "@/components/reUIsables/EntityTable/entity-table";
import { FaPencilAlt, FaTrashAlt } from "react-icons/fa";
import { TextModal } from "@/components/reUIsables/Modals/text-modal";
import { RiFilePaper2Fill } from "react-icons/ri";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";


export default function () {
  const URL_SLUG = "ContractTypes";
  const entityApi = useEntity<ContractType>(URL_SLUG);

  const { data = [], error, isLoading, refetch } = entityApi.list();
  const [createContractType] = entityApi.create();
  const [updateContractType] = entityApi.update();
  const [removeContractType] = entityApi.remove();

  const contractTypeData = data as ContractType[];

  const [selectedItem, setSelectedItem] = useState<ContractType | null>(null);


  const onSubmitCreate = async (data: ContractType) => {

    try {
      await createContractType(data).unwrap();
      LaunchpadSuccessToaster("Contract Type Created Successfully");
      refetch();
    } catch {
      LaunchpadErrorToaster("Contract Type Created Failed");
    }
    onCloseCreate();
  }

  const onSubmitEdit = async (data: ContractType) => {
    if (!selectedItem) return;

    try {
      await updateContractType({ uuid: selectedItem.uuid, data })
      LaunchpadSuccessToaster("Contract Type Updated Successfully");
      refetch();
    } catch {
      LaunchpadErrorToaster("Contract Type Updated Failed");
    }
    onCloseEdit();
  }

  const onSubmitRemove = async () => {
    if (!selectedItem) return;

    try {
      await removeContractType(selectedItem.uuid);
      LaunchpadSuccessToaster("Contract Type Removed Successfully");
      refetch();
    } catch {
      LaunchpadErrorToaster("Contract Type Removal Failed");
    }
    onCloseRemove();
  };

  useEffect(() => {
    refetch();
  }, []);

  const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
  const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
  const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
 

  const breadcrumbs = getBreadcrumbs(pages, location.pathname);
  const formatDescription = (s?:string)=> s ? <TextModal text={s} maxCharacters={20}/>:<></>

  const columns:EntityColumnHeaderProps<ContractType>[] =[{
    dataKey: "name", 
    label:"Name",
    orderable:true,
    searchable:true,
    link:(t:ContractType) => t.uuid,
    displayable:true
  },
  {
    dataKey: "description", 
    label:"Description",
    searchable:true,
    formatCell:formatDescription,
    displayable:true,
  },
];


  const sideMenu= (t:ContractType)=><HStack>
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



  return <PageWrapper title={"Contract Types"} icon={RiFilePaper2Fill} breadcrumbsProps={{items:breadcrumbs}}>
            <Box w="96%" mt="3em" mx="auto">
              <EntityTable topLeftElement={<LaunchpadNewButton onClick={onOpenCreate}/>} itemsPerPage={6} searchable columnDescriptions={columns} rowLastColumn={sideMenu} items={data as ContractType[]}/>
            </Box>
            <EntityWithNameAndDescriptionDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Type" />
            <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Type" />
            <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Type (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
        </PageWrapper>
}



