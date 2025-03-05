import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, useDisclosure } from "@chakra-ui/react"
import { useEffect, useState } from "react";
import { ContractFeature } from "@/models/ContractFeature";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { useEntity } from "@/services/launchpad/entityService";
import { TableWrapper } from "@/components/launchpad/wrappers/table-wrapper";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { ContractFeaturesTable } from "@/components/launchpad/tables/contract-features-table";
import { ContractFeaturesDialog } from "@/components/launchpad/dialogs/contract-features-dialog";
import { BsStars } from "react-icons/bs";


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
      LaunchpadSuccessToaster("Contract Feature Created Successfully");
      refetch();
    } catch {
      LaunchpadErrorToaster("Contract Feature Created Failed");
    }
    onCloseCreate();
  }

  const onSubmitEdit = async (data: ContractFeature) => {
    if (!selectedItem) return;

    try {
      await updateContractFeature({ uuid: selectedItem.uuid, data })
      LaunchpadSuccessToaster("Contract Feature Updated Successfully");
      refetch();
    } catch {
      LaunchpadErrorToaster("Contract Feature Updated Failed");
    }
    onCloseEdit();
  }

  const onSubmitRemove = async () => {
    if (!selectedItem) return;

    try {
      await removeContractFeature(selectedItem.uuid);
      LaunchpadSuccessToaster("Contract Feature Removed Successfully");
      refetch();
    } catch {
      LaunchpadErrorToaster("Contract Feature Removal Failed");
    }
    onCloseRemove();
  };

  const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
  const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
  const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();

  useEffect(() => {
    refetch();
  }, []);

  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Feature (Settings)" description="Manage your contract features" icon={BsStars}>
      <TableWrapper newButtonOnClick={onOpenCreate}>
        <ContractFeaturesTable
          items={paginatedItems}
          pageCount={pageCount}
          page={page}
          setPage={setPage}
          editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })}
          removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })}
          detailsLink={(item) => `/settings/contract/features/${item.uuid}`}
        />
      </TableWrapper>
    </PageWrapper>
    <ContractFeaturesDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Feature" />
    <ContractFeaturesDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Feature" />
    <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Feature (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
  </Box>
}
