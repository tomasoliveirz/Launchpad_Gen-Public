import { Box, Flex } from "@chakra-ui/react"
import { Link } from "react-router-dom";
import { Text } from "@chakra-ui/react";
import { JSX } from "react";

export interface DataListItemProps<T> {
    dataKey: keyof (T)
    label?: string
    dataType?: "text" | "number" | "amount",
    url?: string
    formatCell?: (s: T[keyof T]) => JSX.Element
    format?: (s: string) => string
}

export interface DataListProps<T> {
    columns?: DataListItemProps<T>[]
    item: T
}

export function DataList<T>({ columns, item }: DataListProps<T>) {
    return (
        <Flex w="100%" p="1em" flexDir="column" gap="1em">
            {columns?.map(({ dataKey, label, url, formatCell, format }) => {
                const content = formatCell
                    ? formatCell(item[dataKey])
                    : format
                    ? format(String(item[dataKey]))
                    : String(item[dataKey]);

                return (
                    <Box key={String(dataKey)}>
                        <Text mr="0.5em" fontWeight="bold">{label}</Text>
                        {url ? (
                            <Link style={{ color: "white" }} to={url}>
                                <Text fontWeight="thin">{content !== "" ? content : "None"}</Text>
                            </Link>
                        ) : (
                            <Text fontWeight="thin">{content !== "" ? content : "None"}</Text>
                        )}
                    </Box>
                );
            })}
        </Flex>
    );
}