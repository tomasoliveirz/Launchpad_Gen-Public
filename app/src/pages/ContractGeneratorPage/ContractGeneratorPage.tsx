import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { ContractType } from "@/models/ContractType";
import { ContractVariant } from "@/models/ContractVariant";
import { useEntity } from "@/services/launchpad/testService";
import { namedEntityToListCollection, previousGenerationToListCollection } from "@/support/adapters";
import { contractTypesData } from "@/test-data/contract-types";
import { contractVariantsData } from "@/test-data/contract-variants";
import { previousGenerationsData } from "@/test-data/previous-generations";
import { Flex, HStack, ListCollection, Show, Spacer, VStack } from "@chakra-ui/react";
import axios from "axios";
import { useEffect, useState } from "react";
import { FaCode } from "react-icons/fa";

export default function () {
    const url = import.meta.env.VITE_API_URL

    //DATA
    const [contractsGenerated, setContractsGenerated] = useState([]);
    useEffect(() => {
        axios.get(`${url}/ContractGenerationResults`)
            .then((response) => {
                console.log(response.data)
                setContractsGenerated(response.data)
            })
            .catch((error) => console.error(error));
    }, []);

    //const contractTypesApi = useEntity<ContractType>("ContractTypes");
    //const contractTypesData = contractTypesApi.list().data as ContractType[];


    //CONTROL
    const [contractType, setContractType] = useState<string>();
    const [contractVariant, setContractVariant] = useState<string>();
    const [previousGeneration, setPreviousGeneration] = useState<string>();

    const [contractVariants, setContractVariants] = useState<ContractVariant[]>(contractVariantsData);

    const contractTypesList: ListCollection = namedEntityToListCollection(contractTypesData)
    const contracVariantsList: ListCollection = namedEntityToListCollection(contractVariants);
    const previousGeneratedList: ListCollection = previousGenerationToListCollection(contractsGenerated);

    const [contractFeatureGroup, setContractFeatureGroup] = useState<{ label: string; value: string | undefined }[]>([]);

    useEffect(() => {
        if (contractType) {
            const contractTypeIdx = contractTypesData.findIndex(x => x.uuid == contractType);
            const contractTypeRecord = contractTypesData[contractTypeIdx];
            setContractFeatureGroup(prev => [
                ...prev.filter(item => item.label !== "Contract type"),
                { label: "Contract type", value: contractTypeRecord.name }
            ]);
        }
    }, [contractType]);

    useEffect(() => {
        if (contractVariant) {
            const contractVariantIdx = contractVariantsData.findIndex(x => x.uuid == contractVariant);
            const contractVariantRecord = contractVariantsData[contractVariantIdx];
            setContractFeatureGroup(prev => [
                ...prev.filter(item => item.label !== "Contract Variant"),
                { label: "Contract Variant", value: contractVariantRecord.name }
            ]);
        }
    }, [contractVariant]);

    useEffect(() => {
        if (previousGeneration) {
            const generationIdx = previousGenerationsData.findIndex(x => x.uuid == previousGeneration);
            if (generationIdx != -1) {
                const previousGenerationRecord = previousGenerationsData[generationIdx];
                setContractVariant(previousGenerationRecord.contractVariantUuid);
                const variantIdx = contractVariantsData.findIndex(x => x.uuid == previousGenerationRecord.contractVariantUuid);
                if (variantIdx != -1) {
                    const variantRecord = contractVariantsData[variantIdx];
                    setContractType(variantRecord.contractType.uuid);
                }
            }
        }
    }, [previousGeneration])

    useEffect(() => {
        if (contractVariant) {
            const idx = contractVariants.findIndex(x => x.uuid == contractVariant);
            if (idx != -1) {
                const contractVariantsRecord = contractVariants[idx];
                setContractType(contractVariantsRecord.contractType.uuid);
            }
        }
    }, [contractVariant])

    useEffect(() => {
        let variants: ContractVariant[] = [];
        contractVariantsData.forEach((x: ContractVariant) => {
            if (x.contractType.uuid == contractType) variants.push(x);
        });
        setContractVariants(variants);
    }, [contractType])

    //VIEW
    return <PageWrapper title="Contract Generator" icon={FaCode}>
        <HStack mt="2em" w="100%">
            <Show when={previousGeneratedList.size > 0}>
                <LaunchpadSelect w="20%" collection={previousGeneratedList} title="Previous contracts" value={previousGeneration} onValueChange={setPreviousGeneration} />
            </Show>
            <Spacer maxW="100%" />
        </HStack>
        <VStack w="100%" mt="3em">
            <Flex w="100%" gap="10em">
                <LaunchpadSelect size="sm" w="20%" collection={contractTypesList} title="Contract Types" value={contractType} onValueChange={setContractType} />
                <Show when={contractType}>
                    <LaunchpadSelect size="sm" w="20%" collection={contracVariantsList} title="Contract variants" value={contractVariant} onValueChange={setContractVariant} />
                </Show>
            </Flex>
        </VStack>
        <Show when={contractVariant}>
            <Flex w="100%" gap="10em">
                <ContractSettings />
                <ContractResult contractFeatureGroup={contractFeatureGroup} />
            </Flex>
        </Show>
    </PageWrapper>
}


