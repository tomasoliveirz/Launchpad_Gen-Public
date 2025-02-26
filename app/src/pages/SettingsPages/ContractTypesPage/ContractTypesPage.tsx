import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, useDisclosure } from "@chakra-ui/react"
import { LaunchpadNameTable } from "@/components/launchpad/tables/name-table";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { Toaster} from "@/components/ui/toaster"
import { useEffect, useState } from "react";
import { ContractType } from "@/models/ContractType";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { useEntity } from "@/services/launchpad/entityService";
import { TableWrapper } from "@/components/launchpad/wrappers/table-wrapper";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { RiFilePaper2Fill } from "react-icons/ri";


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
  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Type (Settings)" description="Manage your contract types" icon={RiFilePaper2Fill}>
      <TableWrapper newButtonOnClick={onOpenCreate}>
        <LaunchpadNameTable 
          items={paginatedItems} 
          pageCount={pageCount} 
          page={page} 
          setPage={setPage} 
          editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })}
          detailsLink={(item) => `/settings/contract/types/${item.uuid}`}
          removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })} />
      </TableWrapper>
    </PageWrapper>
    <EntityWithNameAndDescriptionDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Type" />
    <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Type" />
    <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Type (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
    <Toaster />
  </Box>
}
