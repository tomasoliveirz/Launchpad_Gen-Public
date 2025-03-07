import { Box, Flex } from "@chakra-ui/react"
import { Link } from "react-router-dom";
import { Text } from "@chakra-ui/react";

export interface DataListProps {
    columns?: [label: string, value: string, link?: string][]
    item: any
}

export function DataList({ columns }: DataListProps) {
    return <Flex w="100%" p="1em" flexDir="column" gap="1em">
        {columns?.map(([label, value, link]) => (
            <Box key={label}>
                {link ? (
                    <Box fontSize="xl">
                        <Text mr="0.5em" fontWeight="bold">{label}</Text>
                        <Link style={{ color: "white" }} to={link ?? ""}>
                            <Text fontWeight="thin"> {value !== "" ? value : "None"} </Text>
                        </Link>
                    </Box>
                ) : (
                    <Box fontSize="xl">
                        <Text mr="0.5em" fontWeight="bold">{label}</Text>
                        <Text fontWeight="thin"> {value !== "" ? value : "None"} </Text>
                    </Box>
                )}
            </Box>
        ))}
    </Flex>
}