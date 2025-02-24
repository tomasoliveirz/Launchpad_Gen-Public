import { Box, BoxProps, HStack, VStack } from "@chakra-ui/react";
import { LaunchpadNewButton } from "../buttons/button";

export interface TableWrapperProps extends BoxProps {
    newButtonOnClick: () => void
}

export function TableWrapper({ children, newButtonOnClick, ...props }: TableWrapperProps) {
    return <Box justifySelf="center" w="80%" {...props}>
        <VStack w="100%" h="100%" py="1em">
            <HStack w="100%">
                <LaunchpadNewButton onClick={newButtonOnClick} />
            </HStack>
        </VStack>
        {children}
    </Box>
}