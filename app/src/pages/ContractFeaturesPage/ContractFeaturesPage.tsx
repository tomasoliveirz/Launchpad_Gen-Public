import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, HStack, useDisclosure, VStack } from "@chakra-ui/react"
import { FaPalette } from "react-icons/fa"
import { LaunchpadNameTable } from "@/components/launchpad/tables/name-table";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { Toaster, toaster } from "@/components/ui/toaster"
import { launchpadApi } from "@/services/launchpad/launchpadService";
import { useState } from "react";
import { ContractFeature } from "@/models/ContractFeature";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-diaolg";
import { useEntity } from "@/services/launchpad/testService";
import { TableWrapper } from "@/components/launchpad/wrappers/table-wrapper";


export default function () {
  const URL_SLUG = "ContractFeatures";
  const entityApi = useEntity<ContractFeature>(URL_SLUG);

  const { data = [], error, isLoading, refetch } = entityApi.list();
  const [createContractFeature] = entityApi.create();
  const [updateContractFeature] = entityApi.update();
  const [removeContractFeature] = entityApi.remove();

  const ContractFeatureData = data as ContractFeature[];

  const [selectedItem, setSelectedItem] = useState<ContractFeature | null>(null);

  const [page, setPage] = useState(1);
  const pageSize = 6;
  const paginatedItems = ContractFeatureData.slice((page - 1) * pageSize, page * pageSize);
  const pageCount = Math.ceil(data.length / pageSize);

  const onSubmitCreate = async (data: ContractFeature) => {

    try {
      await createContractFeature(data).unwrap();
      toaster.create({
        title: "Success",
        description: "Contract Feature Created Successfully",
        type: "success",
      })
      refetch();
    } catch {
      toaster.create({
        title: "Failed",
        description: "Contract Feature Created Failed",
        type: "error",
      })
    }
    onCloseCreate();
  }

  const onSubmitEdit = async (data: ContractFeature) => {
    if (!selectedItem) return;

    try {
      await updateContractFeature({ uuid: selectedItem.uuid, data })
      toaster.create({
        title: "Success",
        description: "Contract Feature Updated Successfully",
        type: "success",
      })
      refetch();
    } catch {
      toaster.create({
        title: "Failed",
        description: "Contract Feature Updated Failed",
        type: "error",
      })
    }
    onCloseEdit();
  }

  const onSubmitRemove = async () => {
    if (!selectedItem) return;

    try {
      await removeContractFeature(selectedItem.uuid);
      toaster.create({
        title: "Success",
        description: "Contract Feature Removed Successfully",
        type: "success",
      });
      refetch();
    } catch {
      toaster.create({
        title: "Failed",
        description: "Contract Feature Removal Failed",
        type: "error",
      });
    }
    onCloseRemove();
  };

  const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
  const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
  const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();
  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Feature (Settings)" description="Manage your contract features" icon={FaPalette}>
      <TableWrapper newButtonOnClick={onOpenCreate}>
        <LaunchpadNameTable items={paginatedItems} pageCount={pageCount} page={page} setPage={setPage} editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })} removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })} />
      </TableWrapper>
    </PageWrapper>
    <EntityWithNameAndDescriptionDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Feature" />
    <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Feature" />
    <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Feature (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
    <Toaster />
  </Box>
}
