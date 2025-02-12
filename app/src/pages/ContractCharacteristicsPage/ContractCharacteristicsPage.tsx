import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, HStack, VStack } from "@chakra-ui/react"
import { useEffect, useState } from "react";
import { FaPalette, FaPlus } from "react-icons/fa"
import { LaunchpadButton } from "@/components/launchpad/buttons/button";
import axios from "axios";
import { LaunchpadNameTable } from "@/components/launchpad/tables/name-and-description-table";
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

  const [open, setOpen] = useState<boolean>(false);
  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Characteristics (Settings)" description="Manage your contract characteristics" icon={FaPalette}>
      <VStack w="100%" h="100%" py="3em">
        <HStack w="100%">
          <LaunchpadButton onClick={() => setOpen(!open)} icon={FaPlus} text="New" color="white" bg="#5CB338" />
        </HStack>
      </VStack>
      <LaunchpadNameTable items={ContractCharacteristics} />
    </PageWrapper>
    <EntityWithNameAndDescriptionDialog open={open} setOpen={setOpen} entityUrl="ContractCharacteristics/new" title="New Contract Characteristic" />
  </Box>
}


