import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, HStack, useDisclosure, VStack } from "@chakra-ui/react"
import { FaPalette } from "react-icons/fa"
import { LaunchpadNameTable } from "@/components/launchpad/tables/name-table";
import { EntityWithNameAndDescriptionDialog, FormValues } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";
import { LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import { Toaster, toaster } from "@/components/ui/toaster"
import { launchpadApi } from "@/services/launchpad/launchpadService";
import { useState } from "react";


export default function () {
  const { data = [], error, isLoading, refetch } = launchpadApi.useGetContractTypesQuery()
  const [page, setPage] = useState(1);
  const pageSize = 6;
  const paginatedItems = data.slice(
    (page - 1) * pageSize,
    page * pageSize
  );
  const pageCount = Math.ceil(data.length / pageSize);

  const [createContractType] = launchpadApi.useCreateContractTypeMutation()

  const onSubmit = async (data: FormValues) => {
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
    onClose();
  }

  const { onOpen, onClose, open } = useDisclosure();
  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Type (Settings)" description="Manage your contract types" icon={FaPalette}>
      <VStack w="100%" h="100%" py="3em">
        <HStack w="100%">
          <LaunchpadNewButton onClick={onOpen} />

        </HStack>
      </VStack>
      <LaunchpadNameTable items={paginatedItems} pageCount={pageCount} page={page} setPage={setPage} />
    </PageWrapper>
    <EntityWithNameAndDescriptionDialog open={open} onClose={onClose} onSubmit={onSubmit} title="New Contract Type" />
    <Toaster />
  </Box>
}





