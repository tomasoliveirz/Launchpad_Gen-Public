import ContractTypesTable from "@/components/launchpad/settings/contract-types-table";
import { Box, Center, Flex, Heading, VStack } from "@chakra-ui/react";
import axios from "axios";
import { useEffect, useState } from "react";
import { IoIosSettings } from "react-icons/io";

export default function(){
    const [ContractTypes, setContractTypes] = useState([]);
    useEffect(() => {
        axios.get(`https://localhost:7127/api/ContractTypes`)
        .then( (response) => {
            console.log(response.data)
            setContractTypes(response.data)})
        .catch((error) => console.error(error));
    }, []);

    return <>
    <Center height="100%">
        <VStack gap="4em">
            <Flex>
                <Box fontSize="5rem">
                    <IoIosSettings/>
                </Box>
                <Heading as="h1" textStyle="6xl">Contract Types</Heading>
            </Flex>
            <ContractTypesTable items={ContractTypes}/>
        </VStack>
    
    </Center>
    </>
}