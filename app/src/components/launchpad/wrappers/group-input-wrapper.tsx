import { Heading, Stack, StackProps, VStack } from "@chakra-ui/react";

export interface GroupInputWrapperProps extends StackProps {
    label: string;
}

export function GroupInputWrapper({ label, children, ...props }: GroupInputWrapperProps) {
    return <>
        <VStack alignItems="start" py="1em" borderBottom="2px solid white" {...props}>
            <Heading as="h6" fontSize="1em">{label}</Heading>
            <Stack>{children}</Stack>
        </VStack>
    </>
}