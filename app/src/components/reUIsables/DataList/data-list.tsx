import { Box, Flex } from "@chakra-ui/react"
import { Link } from "react-router-dom";
import { Text } from "@chakra-ui/react";
import { JSX } from "react";
import { IconType } from "react-icons";
import { ImageOrIcon } from "../ImageOrIcon/image-or-icon";

export interface DataListItemProps<T> {
    dataKey: keyof (T)
    label?: string
    icon?: IconType | string
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
            {columns?.map(({ dataKey, label, icon, url, formatCell, format }) => {
                const content = formatCell
                    ? formatCell(item[dataKey])
                    : format
                        ? format(String(item[dataKey]))
                        : String(item[dataKey]);

                return (
                    <Box key={String(dataKey)}>
                        <Flex>
                            {icon && <ImageOrIcon w="1.5em" mr="1em" value={icon} />}
                            <Text mr="0.5em" fontWeight="bold">{label}</Text>
                        </Flex>
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