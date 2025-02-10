import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper"
import {Box, Button, ButtonProps, DialogActionTrigger, DialogBody, DialogCloseTrigger, DialogContent, DialogFooter, DialogHeader, DialogRoot, DialogTitle, HStack, Text, VStack} from "@chakra-ui/react"
import { FaPalette, FaPlus } from "react-icons/fa"

export default function()
{
    return <Box minW="100%" minH="100%">
                <PageWrapper w="100%" h="100%" title="Contract Characteristics (Settings)" icon={FaPalette}>
                    <VStack w="100%" h="100%" py="3em">
                        <HStack w="100%">
                            <NewButton bg="red"/>
                        </HStack>
                        <ContractCharacteristicsTable/>
                    </VStack>
                </PageWrapper>
                <ContractCharateristicModal/>
                <DeleteContractCharacteristicModal/>
            </Box>
}


export function NewButton(props : ButtonProps)
{
    return <Button color="white" bg="forestgreen" {...props}>
                <HStack>
                    <FaPlus/>
                    <Text>New</Text>
                </HStack>
        </Button>
}


export function ContractCharacteristicsTable(){
    return <></>
}


export function ContractCharateristicModal(){
    return <></>
    
}


export function DeleteContractCharacteristicModal(){
    return <DialogContent>
    <DialogHeader>
      <DialogTitle>Dialog Title</DialogTitle>
    </DialogHeader>
    <DialogBody>
      <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do
        eiusmod tempor incididunt ut labore et dolore magna aliqua.
      </p>
    </DialogBody>
    <DialogFooter>
      <DialogActionTrigger asChild>
        <Button variant="outline">Cancel</Button>
      </DialogActionTrigger>
      <Button>Save</Button>
    </DialogFooter>
    <DialogCloseTrigger />
  </DialogContent>
}