import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, HStack, Table, useDisclosure, VStack } from "@chakra-ui/react"
import { FaPalette } from "react-icons/fa"
import { LaunchpadNameTable } from "@/components/launchpad/tables/name-table";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { Toaster, toaster } from "@/components/ui/toaster"
import { launchpadApi } from "@/services/launchpad/launchpadService";
import { useState } from "react";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-diaolg";
import { ContractCharacteristic } from "@/models/ContractCharacteristic";
import { useEntity } from "@/services/launchpad/testService";
import { TableWrapper } from "@/components/launchpad/wrappers/table-wrapper";


export default function () {
    const URL_SLUG = "ContractCharacteristics";
    const entityApi = useEntity<ContractCharacteristic>(URL_SLUG);
  
    const { data = [], error, isLoading, refetch } = entityApi.list();
    const [createContractCharacteristic] = entityApi.create();
    const [updateContractCharacteristic] = entityApi.update();
    const [removeContractCharacteristic] = entityApi.remove();
  
    const contractCharacteristicData = data as ContractCharacteristic[];
    
    const [selectedItem, setSelectedItem] = useState<ContractCharacteristic | null>(null);
    
    const [page, setPage] = useState(1);
    const pageSize = 6;
    const paginatedItems = contractCharacteristicData.slice((page - 1) * pageSize, page * pageSize);
    const pageCount = Math.ceil(data.length / pageSize);

  const onSubmitCreate = async (data: ContractCharacteristic) => {
    console.log("data", data);
    try {
      await createContractCharacteristic(data).unwrap();
      toaster.create({
        title: "Success",
        description: "Contract Characteristic Created Successfully",
        type: "success",
      })
      refetch();
    } catch {
      toaster.create({
        title: "Failed",
        description: "Contract Characteristic Created Failed",
        type: "error",
      })
    }
    onCloseCreate();
  }

  const onSubmitEdit = async (data: ContractCharacteristic) => {
    if (!selectedItem) return;

    try {
      await updateContractCharacteristic({ uuid: selectedItem.uuid, data })
      toaster.create({
        title: "Success",
        description: "Contract Characteristic Updated Successfully",
        type: "success",
      })
      refetch();
    } catch {
      toaster.create({
        title: "Failed",
        description: "Contract Characteristic Updated Failed",
        type: "error",
      })
    }
    onCloseEdit();
  }

  const onSubmitRemove = async () => {
    if (!selectedItem) return;

    console.log("selectedItem", selectedItem);
    try {
      await removeContractCharacteristic(selectedItem.uuid);
      toaster.create({
        title: "Success",
        description: "Contract Characteristic Removed Successfully",
        type: "success",
      });
      refetch();
    } catch {
      toaster.create({
        title: "Failed",
        description: "Contract Characteristic Removal Failed",
        type: "error",
      });
    }
    onCloseRemove();
  };

  const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
  const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
  const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Characteristic (Settings)" description="Manage your contract characteristics" icon={FaPalette}>
      <TableWrapper newButtonOnClick={onOpenCreate}>
        <LaunchpadNameTable items={paginatedItems} pageCount={pageCount} page={page} setPage={setPage} editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })} removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })} />
      </TableWrapper>
    </PageWrapper>
    <EntityWithNameAndDescriptionDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Characteristic" />
    <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Characteristic" />
    <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Characteristic (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
    <Toaster />
  </Box>
}
