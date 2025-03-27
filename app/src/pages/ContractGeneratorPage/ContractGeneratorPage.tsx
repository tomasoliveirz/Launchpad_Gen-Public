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
import { Flex, HStack, ListCollection, Show, Spacer, Text, Stack, VStack, Button, Field, Input, NumberInput, Icon, Box, FieldRoot, FieldLabel, FieldErrorText, BoxProps, RadioGroup, Checkbox, CheckboxGroup, For, Fieldset } from "@chakra-ui/react";
import axios from "axios";
import { useEffect, useState } from "react";
import { FaCode } from "react-icons/fa";
import { Controller, useForm } from "react-hook-form";
import { CiCircleQuestion } from "react-icons/ci";
import { BigIntegerInput } from "@/components/reUIsables/ControlledInput/big-int-input";
import { IntegerInput } from "@/components/reUIsables/ControlledInput/int-input";

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



    //VIEW
    return <PageWrapper title="Contract Generator" icon={FaCode}>
        <HStack mt="2em" w="100%">
            <VStack>
                <FungibleTokenContract />
            </VStack>
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
    label?: string;
    value: string;
    description?: string;
    type: "text" | "number" | "option" | "boolean";
    options?: { label: string, value: string }[];
}

export interface ContractGroupProps {
    groupLabel: string;
    inputs: ContractInputItemProps[];
}

export interface ContractGroupsProps {
    groups: ContractGroupProps[]
}
export function ContractGroups({ groups }: ContractGroupsProps) {
    const {
        register,
        handleSubmit,
        control,
        formState: { errors },
    } = useForm<Record<string, any>>()

    const onSubmit = handleSubmit((data) => console.log(data))

    return (
        groups.map((group) => (
            <Box>
                <Text>{group.groupLabel}</Text>
                <Stack gap="4" align="flex-start" maxW="100%">
                    {group.inputs.map((input) => {
                        if (input.type === "text") {
                            return (
                                <InputFieldWrapper label={input.label!} description={input.description} defaultError={errors[input.value]?.message?.toString() || ""} error={errors[input.value]?.message?.toString() || false}>
                                    <Input {...register(input.value)} variant="subtle" />
                                </InputFieldWrapper>
                            );
                        }

                        if (input.type === "number") {
                            return (
                                <InputFieldWrapper label={input.label!} description={input.description} defaultError={errors[input.value]?.message?.toString() || ""} error={errors[input.value]?.message?.toString() || false}>
                                    <Controller
                                        name={input.value}
                                        control={control}
                                        render={({ field }) => (
                                            <IntegerInput
                                                variant="subtle"
                                                value={field.value}
                                                onChange={field.onChange}
                                            />
                                        )} />
                                </InputFieldWrapper>
                            );
                        }

                        if (input.type === "option") {
                            return (
                                <InputFieldWrapper label={input.label!} description={input.description} defaultError={errors[input.value]?.message?.toString() || ""} error={errors[input.value]?.message?.toString() || false}>
                                    <Controller
                                        name={input.value}
                                        control={control}
                                        render={({ field }) => (
                                            <RadioGroup.Root
                                                variant="subtle"
                                                value={field.value}
                                                onValueChange={({ value }) => field.onChange(value)}
                                            >
                                                <VStack gap="6">
                                                    {input.options?.map((option) => (
                                                        <RadioGroup.Item key={option.value} value={option.value}>
                                                            <RadioGroup.ItemHiddenInput />
                                                            <RadioGroup.ItemIndicator />
                                                            <RadioGroup.ItemText>{option.label}</RadioGroup.ItemText>
                                                        </RadioGroup.Item>
                                                    ))}
                                                </VStack>
                                            </RadioGroup.Root>
                                        )}
                                    />
                                </InputFieldWrapper>
                            );
                        }

                        if (input.type === "boolean") {
                            return (
                                <InputFieldWrapper
                                    label={input.label!}
                                    description={input.description}
                                    defaultError={errors[input.value]?.message?.toString() || ""}
                                    error={errors[input.value]?.message?.toString() || false}
                                >
                                    <Fieldset.Root>
                                        <Fieldset.Content>
                                            <For each={input.options}>
                                                {(value) => (
                                                    <Checkbox.Root key={value.value} value={value.value}>
                                                        <Checkbox.HiddenInput />
                                                        <Checkbox.Control />
                                                        <Checkbox.Label>{value.label}</Checkbox.Label>
                                                    </Checkbox.Root>
                                                )}
                                            </For>
                                        </Fieldset.Content>
                                    </Fieldset.Root>
                                </InputFieldWrapper>
                            );
                        }
                    })}
                </Stack>
            </Box>
        ))
    );
}

export interface InputFieldWrapperProps extends BoxProps {
    label: string
    description?: string
    defaultError: string
    error: string | boolean
}

export function InputFieldWrapper({ label, description, defaultError, error, ...props }: InputFieldWrapperProps) {
    return (
        <FieldRoot invalid={(typeof (error) === "boolean" && error === true) || typeof (error) === "string" && error !== ""}>
            <Flex justifyContent="space-between" w="100%">
                <Field.Label>{label}</Field.Label>
                <Box title={description}><Icon size="md"><CiCircleQuestion /></Icon></Box>
            </Flex>
            {props.children}
            <FieldErrorText>
                {typeof (error) === "boolean" ? defaultError : error}
            </FieldErrorText>
        </FieldRoot>

    );
}


const fungibleTokenContractInputGroups: ContractGroupProps[] = [
    {
        groupLabel: "Settings",
        inputs: [
            {
                label: "Name",
                value: "MyToken",
                type: "text"
            },
            {
                label: "Symbol",
                value: "MTK",
                type: "text"
            },
            {
                label: "Premint",
                value: "number",
                description: "Allows gasless approvals using off-chain signatures.",
                type: "number"
            }
        ]
    },
    {
        groupLabel: "Features",
        inputs: [
            {
                value: "features",
                type: "boolean",
                options: [
                    { label: "Mintable", value: "MINTABLE" },
                    { label: "Burnable", value: "BURNABLE" },
                    { label: "Pausable", value: "PAUSABLE" },
                    { label: "Permit", value: "PERMIT" },
                    { label: "Flash Minting", value: "FLASH_MINTING" },
                ]
            },
        ]
    },
];

export function FungibleTokenContract() {
    return (
        <ContractGroups groups={fungibleTokenContractInputGroups} />
    );
}


