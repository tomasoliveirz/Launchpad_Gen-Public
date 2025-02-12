import { Field } from "@/components/ui/field";
import { Box, Flex, Input } from "@chakra-ui/react";
import { LaunchpadButton } from "../buttons/button";

export function ContractSettings() {
    return (
        <Box mt="3em" w="20%">
            <Field label="Contract Settings" />
            <Flex gap="3em">
                <Box>
                    <Field label="Name" />
                    <Input variant="subtle"/>
                </Box>
                <Box>
                    <Field label="Symbol" />
                    <Input variant="subtle"/>
                </Box>
            </Flex>
            <LaunchpadButton text="Generate" bg="#5CB338" color="white" mt="2em"/>
        </Box>
    )
}