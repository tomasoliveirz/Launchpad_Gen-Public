import { Field } from "@/components/ui/field";
import { Box, BoxProps, For, Text } from "@chakra-ui/react";

export interface ContractResultProps extends BoxProps {
    contractFeatureGroup: { label: string; value: string | undefined }[];
}

export default function ContractResult({contractFeatureGroup, ...props}: ContractResultProps)  {
    return (
        <Box mt="3em" {...props}>
            <Field label="Result" />
            <Box minW="30rem" minH="25rem" bg="#26282B" p="1em">
            {contractFeatureGroup.map((item) => (
                    <Box py="1rem">
                        <Text color="white">{item.label}: {item.value}</Text>
                    </Box>
                ))}
            </Box>
        </Box>
    )
}