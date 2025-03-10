import { Box, HStack, Spacer, useDisclosure } from "@chakra-ui/react"
import { useEffect, useState } from "react";
import { ContractFeature } from "@/models/ContractFeature";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { useEntity } from "@/services/launchpad/entityService";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { BsStars } from "react-icons/bs";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { pages } from "@/constants/pages";
import EntityTable, { EntityColumnHeaderProps } from "@/components/reUIsables/EntityTable/entity-table";
import { FaPencilAlt, FaTrashAlt } from "react-icons/fa";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { ContractFeaturesDialog } from "@/components/launchpad/dialogs/contract-features-dialog";
import { TextModal } from "@/components/reUIsables/Modals/text-modal";

export default function () {
  const URL_SLUG = "ContractFeatures";
  const entityApi = useEntity<ContractFeature>(URL_SLUG);

  const { data = [], refetch } = entityApi.list();
  const [createContractFeature] = entityApi.create();
  const [updateContractFeature] = entityApi.update();
  const [removeContractFeature] = entityApi.remove();

  const [selectedItem, setSelectedItem] = useState<ContractFeature | null>(null);

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

  useEffect(() => {
    refetch();
  }, []);

  const { onOpen: onOpenCreate, onClose: onCloseCreate, open: openCreate } = useDisclosure();
  const { onOpen: onOpenEdit, onClose: onCloseEdit, open: openEdit } = useDisclosure();
  const { onOpen: onOpenRemove, onClose: onCloseRemove, open: openRemove } = useDisclosure();


  const breadcrumbs = getBreadcrumbs(pages, location.pathname);
  const formatDescription = (s?: ContractFeature[keyof ContractFeature]) => s ? <TextModal text={s as string} maxCharacters={20} /> : <></>

  const columns: EntityColumnHeaderProps<ContractFeature>[] = [{
    dataKey: "name",
    label: "Name",
    orderable: true,
    searchable: true,
    link: (t: ContractFeature) => t.uuid,
    displayable: true
  },
  {
    dataKey: "description",
    label: "Description",
    searchable: true,
    formatCell: formatDescription,
    displayable: true,
  },
  {
    dataKey: "dataType",
    label: "Data Type",
    searchable: true,
    displayable: true,
    orderable: true,
  },
  {
    dataKey: "defaultValue",
    label: "Default Value",
    searchable: false,
    displayable: true,
  },
  {
    dataKey: "normalizedName",
    label: "Normalized Name",
    searchable: false,
    displayable: true,
  },
  ];


  const sideMenu = (t: ContractFeature) => <HStack>
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



  return <PageWrapper title={"Contract Features"} icon={BsStars} breadcrumbsProps={{ items: breadcrumbs }}>
    <Box w="96%" mt="3em" mx="auto">
      <EntityTable topLeftElement={<LaunchpadNewButton onClick={onOpenCreate} />} itemsPerPage={6} searchable columnDescriptions={columns} rowLastColumn={sideMenu} items={data as ContractFeature[]} />
    </Box>
    <ContractFeaturesDialog open={openCreate} onClose={onCloseCreate} onSubmit={onSubmitCreate} title="New Contract Feature" />
    <ContractFeaturesDialog open={openEdit} onClose={onCloseEdit} onSubmit={onSubmitEdit} defaultValues={selectedItem || undefined} title="Edit Contract Feature" />
    <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Contract Feature (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
  </PageWrapper>
}