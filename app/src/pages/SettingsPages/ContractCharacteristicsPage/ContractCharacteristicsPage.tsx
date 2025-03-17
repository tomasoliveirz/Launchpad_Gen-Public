import { Box, HStack, Spacer, useDisclosure } from "@chakra-ui/react"
import { FaPalette, FaPencilAlt, FaTrashAlt } from "react-icons/fa"
import { useEffect, useState } from "react";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { ContractCharacteristic } from "@/models/ContractCharacteristic";
import { useEntity } from "@/services/launchpad/entityService";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import EntityTable, { EntityColumnHeaderProps } from "@/components/reUIsables/EntityTable/entity-table";
import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { pages } from "@/constants/pages";
import { TextModal } from "@/components/reUIsables/Modals/text-modal";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { EntityDialog, EntityDialogItemProps } from "@/components/launchpad/dialogs/entity-dialog";

export default function () {
  const URL_SLUG = "ContractCharacteristics";
  const entityApi = useEntity<ContractCharacteristic>(URL_SLUG);

  const { data = [], refetch } = entityApi.list();
  const [createContractCharacteristic] = entityApi.create();
  const [updateContractCharacteristic] = entityApi.update();
  const [removeContractCharacteristic] = entityApi.remove();

  const [selectedItem, setSelectedItem] = useState<ContractCharacteristic | null>(null);

  const onSubmitCreate = async (data: ContractCharacteristic) => {

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

  useEffect(() => {
    refetch();
  }, []);

  const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
  const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
  const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();


  const breadcrumbs = getBreadcrumbs(pages, location.pathname);
  const formatDescription = (s?: ContractCharacteristic[keyof ContractCharacteristic]) => s ? <TextModal text={s as string} maxCharacters={20} /> : <></>

  const columns: EntityColumnHeaderProps<ContractCharacteristic>[] = [{
    dataKey: "name",
    label: "Name",
    orderable: true,
    searchable: true,
    link: (t: ContractCharacteristic) => t.uuid,
    displayable: true
  },
  {
    dataKey: "description",
    label: "Description",
    searchable: true,
    formatCell: formatDescription,
    displayable: true,
  },
  ];

  const dialogColumns: EntityDialogItemProps<ContractCharacteristic>[] = [
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


  const sideMenu = (t: ContractCharacteristic) => <HStack>
    <Spacer />
    <FaPencilAlt title="Edit" cursor="pointer" onClick={() => {
      setSelectedItem(t);
      onOpenEdit();
    }} />
    <FaTrashAlt title="Delete" cursor="pointer" onClick={() => {
      setSelectedItem(t);
      onOpenRemove();
    }} />
  </HStack>



  return <PageWrapper title={"Contract Characteristics"} icon={FaPalette} breadcrumbsProps={{ items: breadcrumbs }}>
    <Box w="96%" mt="3em" mx="auto">
      <EntityTable topLeftElement={<LaunchpadNewButton onClick={onOpenCreate} />} itemsPerPage={6} searchable columnDescriptions={columns} rowLastColumn={sideMenu} items={data as ContractCharacteristic[]} />
    </Box>
    <EntityDialog
      columns={dialogColumns}
      open={openCreate}
      onClose={onCloseCreate}
      onSubmit={onSubmitCreate}
      title="New Contract Characteristic"
    />
    <EntityDialog
      columns={dialogColumns}
      open={openEdit}
      onClose={onCloseEdit}
      onSubmit={onSubmitEdit}
      defaultValues={selectedItem || undefined}
      title="Edit Contract Characteristic"
    />
    {/* <EntityWithNameAndDescriptionDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Characteristic" />
    <EntityWithNameAndDescriptionDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Characteristic" /> */}
    <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Characteristic (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
  </PageWrapper>
}