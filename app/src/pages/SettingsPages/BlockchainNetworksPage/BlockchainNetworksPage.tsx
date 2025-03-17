import { Box, useDisclosure, Image, HStack, Spacer } from "@chakra-ui/react";
import { FaNetworkWired, FaPencilAlt, FaQuestion, FaTrashAlt } from "react-icons/fa";
import { useEffect, useState } from "react";
import { DeleteConfirmationDialog } from "@/components/launchpad/dialogs/delete-confirmation-dialog";
import { useEntity } from "@/services/launchpad/entityService";
import { BlockchainNetwork } from "@/models/BlockchainNetwork";
import { LaunchpadErrorToaster, LaunchpadSuccessToaster } from "@/components/reUIsables/Toaster/toaster";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { pages } from "@/constants/pages";
import EntityTable, { EntityColumnHeaderProps } from "@/components/reUIsables/EntityTable/entity-table";
import { TextModal } from "@/components/reUIsables/Modals/text-modal";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { EntityDialog, EntityDialogItemProps } from "@/components/launchpad/dialogs/entity-dialog";

export default function () {
    const URL_SLUG = "BlockchainNetworks";
    const entityApi = useEntity<BlockchainNetwork>(URL_SLUG);

    const { data = [], refetch } = entityApi.list();
    const [createBlockchainNetwork] = entityApi.create();
    const [updateBlockchainNetwork] = entityApi.update();
    const [removeBlockchainNetwork] = entityApi.remove();

    const [selectedItem, setSelectedItem] = useState<BlockchainNetwork | null>(null);

    const onSubmitCreate = async (data: BlockchainNetwork) => {

        try {
            await createBlockchainNetwork(data).unwrap();
            LaunchpadSuccessToaster("Blockchain Network Created Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Blockchain Network Created Failed");
        }
        onCloseCreate();
    }

    const onSubmitEdit = async (data: BlockchainNetwork) => {
        if (!selectedItem) return;

        try {
            await updateBlockchainNetwork({ uuid: selectedItem.uuid, data })
            LaunchpadSuccessToaster("Blockchain Network Updated Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Blockchain Network Updated Failed");
        }
        onCloseEdit();
    }

    const onSubmitRemove = async () => {
        if (!selectedItem) return;

        try {
            await removeBlockchainNetwork(selectedItem.uuid);
            LaunchpadSuccessToaster("Blockchain Network Removed Successfully");
            refetch();
        } catch {
            LaunchpadErrorToaster("Blockchain Network Removal Failed");
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
    const formatDescription = (s?: BlockchainNetwork[keyof BlockchainNetwork]) => s ? <TextModal text={s as string} maxCharacters={20} /> : <></>
    const formatImage = (s?: BlockchainNetwork[keyof BlockchainNetwork]) => s ? <Image w="2em" h="auto" src={s as string} alt={s as string} /> : <Box fontSize="2em"><FaQuestion /></Box>

    const columns: EntityColumnHeaderProps<BlockchainNetwork>[] = [{
        dataKey: "image",
        label: "Image",
        searchable: false,
        formatCell: formatImage,
        displayable: true,
    },
    {
        dataKey: "name",
        label: "Name",
        orderable: true,
        searchable: true,
        link: (t: BlockchainNetwork) => t.uuid,
        displayable: true
    },
    {
        dataKey: "description",
        label: "Description",
        searchable: true,
        formatCell: formatDescription,
        displayable: true,
    }
    ];

    const dialogColumns: EntityDialogItemProps<BlockchainNetwork>[] = [
        {
            dataKey: "name",
            label: "Name",
            dataType: "text"
        },
        {
            dataKey: "description",
            label: "Description",
            dataType: "longText"
        },
        {
            dataKey: "image",
            label: "Image",
            dataType: "image"
        }
    ]

    const sideMenu = (t: BlockchainNetwork) => <HStack>
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

    return <PageWrapper title={"Blockchain Networks"} icon={FaNetworkWired} breadcrumbsProps={{ items: breadcrumbs }}>
        <Box w="96%" mt="3em" mx="auto">
            <EntityTable topLeftElement={<LaunchpadNewButton onClick={onOpenCreate} />} itemsPerPage={6} searchable columnDescriptions={columns} rowLastColumn={sideMenu} items={data as BlockchainNetwork[]} />
        </Box>
        <EntityDialog
            columns={dialogColumns}
            open={openCreate}
            onClose={onCloseCreate}
            onSubmit={onSubmitCreate}
            title="New Blockchain Network"
        />
        <EntityDialog
            columns={dialogColumns}
            open={openEdit}
            onClose={onCloseEdit}
            onSubmit={onSubmitEdit}
            title="Edit Blockchain Network"
            defaultValues={selectedItem || undefined}
        />
        <DeleteConfirmationDialog open={openRemove} onClose={onCloseRemove} title={`Delete Blockchain Network (${selectedItem?.name})`} onSubmit={onSubmitRemove} />
    </PageWrapper>
}