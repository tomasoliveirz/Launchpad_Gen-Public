import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, HStack, useDisclosure, VStack } from "@chakra-ui/react"
import { useEffect, useState } from "react";
import { FaPalette, FaPlus } from "react-icons/fa"
import { LaunchpadButton, LaunchpadNewButton } from "@/components/launchpad/buttons/button";
import axios from "axios";
import { LaunchpadNameTable } from "@/components/launchpad/tables/name-table";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";

export default function () {
  const url = import.meta.env.VITE_APP_API_URL

  const [ContractCharacteristics, setContractCharacteristics] = useState([]);
  useEffect(() => {
    axios.get(`${url}/ContractCharacteristics`)
      .then((response) => {
        setContractCharacteristics(response.data)
      })
      .catch((error) => console.error(error));
  }, []);

  const {onOpen, onClose, open } = useDisclosure();
  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Characteristics (Settings)" description="Manage your contract characteristics" icon={FaPalette}>
      <VStack w="100%" h="100%" py="3em">
        <HStack w="100%">
          <LaunchpadNewButton onClick={onOpen} />
        </HStack>
      </VStack>
      <LaunchpadNameTable items={ContractCharacteristics} />
    </PageWrapper>
    <EntityWithNameAndDescriptionDialog open={open} onClose={onClose} title="New Contract Characteristic" />
  </Box>
}


