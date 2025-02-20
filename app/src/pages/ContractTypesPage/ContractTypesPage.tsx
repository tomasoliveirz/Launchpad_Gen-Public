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
  const [createContractType] = entityApi.create();
  const [updateContractType] = entityApi.update();
  const [removeContractType] = entityApi.remove();

  const contractTypeData = data as ContractType[];
  
  const [selectedItem, setSelectedItem] = useState<ContractType | null>(null);
  
  const [page, setPage] = useState(1);
  const pageSize = 6;
  const paginatedItems = contractTypeData.slice((page - 1) * pageSize, page * pageSize);
  const pageCount = Math.ceil(data.length / pageSize);

  const onSubmitCreate = async (data: ContractType) => {

    try {
      await createContractType(data).unwrap();
      toaster.create({
        title: "Success",
        description: "Contract Type Created Successfully",
        type: "success",
      })
      refetch();
    } catch {
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

    try {
      await updateContractType({ uuid: selectedItem.uuid, data })
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
