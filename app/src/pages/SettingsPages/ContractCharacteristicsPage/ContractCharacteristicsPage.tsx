import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, useDisclosure } from "@chakra-ui/react"
import { FaPalette } from "react-icons/fa"
import { LaunchpadNameTable } from "@/components/launchpad/tables/name-table";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { useEffect, useState } from "react";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { ContractCharacteristic } from "@/models/ContractCharacteristic";
import { useEntity } from "@/services/launchpad/entityService";
import { TableWrapper } from "@/components/launchpad/wrappers/table-wrapper";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";


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
      LaunchpadSuccessToaster("Contract Characteristic Created Successfully");
      refetch();
    } catch {
      LaunchpadErrorToaster("Contract Characteristic Created Failed");
    }
    onCloseCreate();
  }

  const onSubmitEdit = async (data: ContractCharacteristic) => {
    if (!selectedItem) return;

    try {
      await updateContractCharacteristic({ uuid: selectedItem.uuid, data })
      LaunchpadSuccessToaster("Contract Characteristic Updated Successfully");
      refetch();
    } catch {
      LaunchpadErrorToaster("Contract Characteristic Updated Failed");
    }
    onCloseEdit();
  }

  const onSubmitRemove = async () => {
    if (!selectedItem) return;

    try {
      await removeContractCharacteristic(selectedItem.uuid);
      LaunchpadSuccessToaster("Contract Characteristic Removed Successfully");
      refetch();
    } catch {
      LaunchpadErrorToaster("Contract Characteristic Removal Failed");
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
    <PageWrapper w="100%" h="100%" title="Contract Characteristic (Settings)" description="Manage your contract characteristics" icon={FaPalette}>
      <TableWrapper newButtonOnClick={onOpenCreate}>
        <LaunchpadNameTable
          items={paginatedItems}
          pageCount={pageCount}
          page={page}
          setPage={setPage}
          editButtonOnClick={(item => { setSelectedItem(item); onOpenEdit(); })}
          removeButtonOnClick={(item => { setSelectedItem(item); onOpenRemove(); })}
          detailsLink={(item) => `/settings/contract/characteristics/${item.uuid}`}
        />
      </TableWrapper>
    </PageWrapper>
    <EntityWithNameAndDescriptionDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Characteristic" />
    <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Characteristic" />
    <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Characteristic (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
  </Box>
}
