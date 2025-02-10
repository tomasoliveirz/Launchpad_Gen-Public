import { LaunchpadSelect } from "@/components/launchpad/select/select";
import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { ContractVariant } from "@/models/ContractVariant";
import { namedEntityToListCollection, previousGenerationToListCollection } from "@/support/adapters";
import { contractTypesData } from "@/test-data/contract-types";
import { contractVariantsData } from "@/test-data/contract-variants";
import { previousGenerationsData } from "@/test-data/previous-generations";
import { Flex, HStack, ListCollection, Spacer, VStack } from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { FaCode } from "react-icons/fa";

export default function()
{

    //DATA
    
    //CONTROL
    const [contractType, setContractType] = useState<string>();
    const [contractVariant,setContractVariant ] = useState<string>();
    const [previousGeneration,setPreviousGeneration ] = useState<string>();
    
    const [contractVariants, setContractVariants] = useState<ContractVariant[]>(contractVariantsData);

    const contractTypesList : ListCollection = namedEntityToListCollection(contractTypesData)
    const contracVariantsList : ListCollection = namedEntityToListCollection(contractVariants);
    const previousGeneratedList:ListCollection = previousGenerationToListCollection(previousGenerationsData);


    useEffect(()=>
    {
        if(previousGeneration)
        {
            const generationIdx = previousGenerationsData.findIndex(x => x.uuid == previousGeneration);
            if(generationIdx != -1)
            {
                const previousGenerationRecord = previousGenerationsData[generationIdx];
                setContractVariant(previousGenerationRecord.contractVariantUuid);
                const variantIdx = contractVariantsData.findIndex(x => x.uuid == previousGenerationRecord.contractVariantUuid);
                if(variantIdx != -1)
                {
                    const variantRecord = contractVariantsData[variantIdx];
                    setContractType(variantRecord.contractTypeUuid);
                }
            }
        }
    },[previousGeneration])

    useEffect(()=>
    {
        if(contractVariant)
        {
            const idx = contractVariants.findIndex(x => x.uuid == contractVariant);
            if(idx != -1)
            {
                const contractVariantsRecord = contractVariants[idx];
                setContractType(contractVariantsRecord.contractTypeUuid);
            } 
        }
    },[contractVariant])

    useEffect(()=>{
        let variants:ContractVariant[] = [];
        contractVariantsData.forEach((x:ContractVariant)=>{
                if(x.contractTypeUuid == contractType) variants.push(x);
        });
        setContractVariants(variants);
    }, [contractType])

    //VIEW
    return <PageWrapper title="Contract Generator" icon={FaCode}>
                <HStack mt="2em" w="100%">
                    <LaunchpadSelect w="30%" collection={previousGeneratedList} title="Previous contracts" value={previousGeneration} onValueChange={setPreviousGeneration} />
                    <Spacer maxW="100%"/>
                </HStack>
                <VStack w="100%" mt="3em">
                    <Flex w="100%">
                        <LaunchpadSelect w="30%" collection={contractTypesList} title="Contract Types" value={contractType} onValueChange={setContractType} />
                        <LaunchpadSelect w="30%" collection={contracVariantsList} title="Contract variants" value={contractVariant}onValueChange={setContractVariant} />
                    </Flex>                    
                    {/* <ContractSettingsAndResult/> */}
                </VStack>
            </PageWrapper>
}






