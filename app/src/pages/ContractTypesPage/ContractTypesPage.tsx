import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, HStack, useDisclosure, VStack } from "@chakra-ui/react"
import { FaPalette } from "react-icons/fa"
import { LaunchpadNameTable } from "@/components/launchpad/tables/name-table";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { Toaster, toaster } from "@/components/ui/toaster"
import { launchpadApi } from "@/services/launchpad/launchpadService";
import { useState } from "react";
import { ContractType } from "@/models/ContractType";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-diaolg";
import { useEntity } from "@/services/launchpad/testService";


export default function () {
  const URL_SLUG = "ContractTypes";
  
  const entityApi = useEntity<ContractType>(URL_SLUG);
  const { data = [], error, isLoading, refetch } = entityApi.list();
  const [createContractType, status] = entityApi.create();

  //const { data = [], error, isLoading, refetch } = launchpadApi.useGetContractTypesQuery()
  //const [createContractType] = launchpadApi.useCreateContractTypeMutation()
  const [updateContractType] = launchpadApi.useUpdateContractTypeMutation()
  const [removeContractType] = launchpadApi.useRemoveContractTypeMutation()
  
  const [selectedItem, setSelectedItem] = useState<ContractType | null>(null);
  
  const [page, setPage] = useState(1);
  const pageSize = 6;
  const paginatedItems = data.slice((page - 1) * pageSize, page * pageSize);
  const pageCount = Math.ceil(data.length / pageSize);
  console.log(createContractType);
  const onSubmitCreate = async (data: ContractType) => {
    console.log("data", data);
    try {
      await createContractType(data).unwrap();
      toaster.create({
        title: "Success",
        description: "Contract Type Created Successfully",
        type: "success",
      })
      refetch();
    } catch (e) {
      console.log(status);
      toaster.create({
        title: "Failed",
        description: "Contract Type Created Failed",
        type: "error",
      })
    }
    onCloseCreate();
  }

  const onSubmitEdit = async (data: ContractType) => {
    if (!selectedItem) return;
    console.log("data", data);
    console.log("selectedItem", selectedItem);
    try {
      await updateContractType({ uuid: selectedItem.uuid, contractType: data })
      toaster.create({
        title: "Success",
        description: "Contract Type Updated Successfully",
        type: "success",
      })
      refetch();
    } catch {
      toaster.create({
        title: "Failed",
        description: "Contract Type Updated Failed",
        type: "error",
      })
    }
    onCloseEdit();
  }

  const onSubmitRemove = async () => {
    if (!selectedItem) return;

    console.log("selectedItem", selectedItem);
    try {
      await removeContractType(selectedItem.uuid);
      toaster.create({
        title: "Success",
        description: "Contract Type Removed Successfully",
        type: "success",
      });
      refetch();
    } catch {
      toaster.create({
        title: "Failed",
        description: "Contract Type Removal Failed",
        type: "error",
      });
    }
    onCloseRemove();
  };

  const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
  const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
  const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Type (Settings)" description="Manage your contract types" icon={FaPalette}>
      <VStack w="100%" h="100%" py="3em">
        <HStack w="100%">
          <LaunchpadNewButton onClick={onOpenCreate} />
        </HStack>
      </VStack>
      <LaunchpadNameTable items={paginatedItems} pageCount={pageCount} page={page} setPage={setPage} editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })} removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })} />
    </PageWrapper>
    <EntityWithNameAndDescriptionDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Type" />
    <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Type" />
    <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Type (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
    <Toaster />
  </Box>
}
