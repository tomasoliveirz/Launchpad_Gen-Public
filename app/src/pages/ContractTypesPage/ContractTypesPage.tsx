import { LauchpadButton } from "@/components/launchpad/buttons/button"
import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, HStack, VStack } from "@chakra-ui/react"
import { useEffect, useState } from "react";
import { FaPalette, FaPlus} from "react-icons/fa"
import axios from "axios";
import { LauchpadNameTable } from "@/components/launchpad/tables/name-and-description-table";
import { EntityWithNameAndDescriptionDialog } from "@/components/launchpad/dialogs/entity-with-name-and-description-dialog";

export default function () {
  const url = import.meta.env.VITE_APP_API_URL

  const [ContractTypes, setContractTypes] = useState([]);
  useEffect(() => {
    axios.get(`${url}/ContractTypes`)
      .then((response) => {
        setContractTypes(response.data)
      })
      .catch((error) => console.error(error));
  }, []);

  const [open, setOpen] = useState<boolean>(false);
  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Type (Settings)" description="Manage your contract types" icon={FaPalette}>
      <VStack w="100%" h="100%" py="3em">
        <HStack w="100%">
          <LauchpadButton onClick={() => setOpen(!open)} icon={FaPlus} text="New" color="white" bg="#5CB338" />
        </HStack>
      </VStack>
      <LauchpadNameTable items={ContractTypes} />
    </PageWrapper>
    <EntityWithNameAndDescriptionDialog open={open} setOpen={setOpen} entityUrl="ContractTypes/new" title="New Contract Type" />
  </Box>
}