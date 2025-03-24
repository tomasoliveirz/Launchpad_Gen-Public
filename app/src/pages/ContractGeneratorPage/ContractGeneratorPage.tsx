import { CodePreview } from "@/components/launchpad/code-preview/code-preview";
import { ContractSettings } from "@/components/launchpad/contract-generator-components/contract-settings";
import { LaunchpadSelect } from "@/components/launchpad/select/select";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { ContractType } from "@/models/ContractType";
import { ContractVariant } from "@/models/ContractVariant";
import { useEntity } from "@/services/launchpad/entityService";
import { namedEntityToListCollection, previousGenerationToListCollection } from "@/support/adapters";
import { contractVariantsData } from "@/test-data/contract-variants";
import { previousGenerationsData } from "@/test-data/previous-generations";
import { Flex, HStack, ListCollection, Show, Spacer, Stack, VStack, Button, Field, Input, NumberInput, Icon, Box } from "@chakra-ui/react";
import axios from "axios";
import { useEffect, useState } from "react";
import { FaCode } from "react-icons/fa";
import { Controller, useForm } from "react-hook-form";
import { CiCircleQuestion } from "react-icons/ci";

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

    const contractTypesApi = useEntity<ContractType>("ContractTypes");
    const contractTypesData = contractTypesApi.list().data as ContractType[];


    //CONTROL
    const [contractType, setContractType] = useState<string>();
    const [contractVariant, setContractVariant] = useState<string>();
    const [previousGeneration, setPreviousGeneration] = useState<string>();

    const [contractVariants, setContractVariants] = useState<ContractVariant[]>(contractVariantsData);

    /* const contractTypesList: ListCollection = namedEntityToListCollection(contractTypesData)
    const contracVariantsList: ListCollection = namedEntityToListCollection(contractVariants);
    const previousGeneratedList: ListCollection = previousGenerationToListCollection(contractsGenerated); */

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

    const [val, setVal] = useState<string | undefined>();
    const [error, setError] = useState<boolean | string>(false);
    const defaultError = "invalid number"



    const testInputs: ContractInputItemProps[] = [
        {
            label: "Name",
            value: "name",
            description: "name",
            type: "text"
        },
        {
            label: "Number",
            value: "number",
            description: "number",
            type: "number"
        }
    ]



    //VIEW
    return <PageWrapper title="Contract Generator" icon={FaCode}>
        <HStack mt="2em" w="100%">
            <ContractInputs inputs={testInputs} />
            {/* <Show when={previousGeneratedList.size > 0}>
                <LaunchpadSelect w="20%" collection={previousGeneratedList} title="Previous contracts" value={previousGeneration} onValueChange={setPreviousGeneration} />
            </Show> */}
            <Spacer maxW="100%" />
        </HStack>
        <VStack w="100%" mt="3em">

            {/* <Flex w="100%" gap="10em">
                <LaunchpadSelect size="sm" w="20%" collection={contractTypesList} title="Contract Types" value={contractType} onValueChange={setContractType} />
                <Show when={contractType}>
                    <LaunchpadSelect size="sm" w="20%" collection={contracVariantsList} title="Contract variants" value={contractVariant} onValueChange={setContractVariant} />
                </Show>
            </Flex> */}
        </VStack>
        <Show when={contractVariant}>
            <Flex w="100%" gap="10em">
                <ContractSettings />
                <CodePreview mt="5em" />
            </Flex>
        </Show>
    </PageWrapper>
}

export interface ContractInputItemProps {
    label: string;
    value: string;
    description?: string;
    type: "text" | "number" | "option" | "boolean";
}

export interface ContractInputsProps {
    inputs: ContractInputItemProps[];
}
export function ContractInputs({ inputs }: ContractInputsProps) {
    const {
        register,
        handleSubmit,
        control,
        formState: { errors },
    } = useForm<Record<string, any>>()

    const onSubmit = handleSubmit((data) => console.log(data))

    return (
        <Stack gap="4" align="flex-start" maxW="100%">
            {inputs.map((input) => {
                if (input.type === "text") {
                    return (
                        <Field.Root key={input.value} invalid={!!errors[input.value]}>
                            <Flex justifyContent="space-between" w="100%">
                                <Field.Label>{input.label}</Field.Label>
                                <Box title={input.description}><Icon size="md"><CiCircleQuestion/></Icon></Box>
                            </Flex>
                            <Input {...register(input.value)} variant="subtle" />
                            <Field.ErrorText>
                                {typeof errors[input.value]?.message === "string"
                                    ? errors[input.value]?.message?.toString()
                                    : ""}
                            </Field.ErrorText>
                        </Field.Root>
                    );
                }
                if (input.type === "number") {
                    return (
                        <Field.Root invalid={!!errors.number}>
                            <Flex justifyContent="space-between" w="100%">
                                <Field.Label>{input.label}</Field.Label>
                                <Box title={input.description}><Icon size="md"><CiCircleQuestion/></Icon></Box>
                            </Flex>
                            <Controller
                                name={input.label}
                                control={control}
                                render={({ field }) => (
                                    <NumberInput.Root
                                        variant={"subtle"}
                                        name={input.label}
                                        value={field.value}
                                        onValueChange={({ value }) => {
                                            field.onChange(value)
                                        }}
                                    >
                                        <NumberInput.Input/>
                                    </NumberInput.Root>
                                )}
                            />
                            <Field.ErrorText>{errors[input.value]?.message?.toString()}</Field.ErrorText>
                        </Field.Root>
                    )
                }
            })}
        </Stack>
    );


}


