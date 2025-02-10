import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import { Box, Button, ButtonProps, DialogActionTrigger, DialogBody, DialogCloseTrigger, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle, HStack, Input, Text, Textarea, VStack } from "@chakra-ui/react"
import { useState } from "react";
import { FaPalette, FaPlus } from "react-icons/fa"
import { Field } from "@/components/ui/field"
import { ContractCharateristicModal } from "@/components/launchpad/settings/contract-characteristic-modal";

export default function () {
  const [open, setOpen] = useState<boolean>(false);

  return <Box minW="100%" minH="100%">
    <PageWrapper w="100%" h="100%" title="Contract Characteristics (Settings)" icon={FaPalette}>
      <VStack w="100%" h="100%" py="3em">
        <HStack w="100%">
          <NewButton onClick={() => setOpen(!open)} />
          <ContractCharateristicModal uuid="" name="" description="" open={open} setOpen={setOpen} />
        </HStack>
        <ContractCharacteristicsTable />
      </VStack>
    </PageWrapper>
  </Box>
}


export function NewButton(props: ButtonProps) {
  return <Button color="white" bg="forestgreen" {...props}>
    <HStack>
      <FaPlus />
      <Text>New</Text>
    </HStack>
  </Button>
}


export function ContractCharacteristicsTable() {
  return <Box>

  </Box>
}


