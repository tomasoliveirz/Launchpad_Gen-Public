import { Box, Flex } from "@chakra-ui/react";
import { LaunchpadButton } from "../buttons/button";
import { FaPencilAlt, FaQuestion, FaTrashAlt } from "react-icons/fa";
import { Text } from "@chakra-ui/react";
import { Link } from "react-router-dom";
import { Image } from '@chakra-ui/react';

export interface EntityDetailsProps {
    columns?: [label: string, value: string, link?: string, image?: boolean][]
    editButtonOnClick?: (item: any) => void;
    removeButtonOnClick?: (item: any) => void;
    item: any
}

export function EntityDetails({ columns, editButtonOnClick, removeButtonOnClick, item, ...props }: EntityDetailsProps) {
    return <Box p="2em" {...props}>
        <Flex w="100%" gap="1em" placeContent="end" mb="1em">
            <LaunchpadButton icon={FaPencilAlt} text="Edit" onClick={() => editButtonOnClick?.(item)} color="white" bg="#FF7518" />
            <LaunchpadButton icon={FaTrashAlt} text="Delete" onClick={() => removeButtonOnClick?.(item)} color="white" bg="#dd1717" />
        </Flex>
        <Flex bg="#26282B" w="100%" p="1em" flexDir="column" gap="1em">
            {columns?.map(([label, value, link, image]) => (
                <Box key={label}>
                    {image ? (
                        value !== "" ? (
                            <Image
                                height="60px"
                                src={value}
                                alt={label}
                            />
                        ) : (
                            <Box fontSize="50px">
                                <FaQuestion />
                            </Box>
                        )
                    ) : (
                        link ? (
                            <Link style={{ color: "white" }} to={link}>
                                <Text fontSize="xl">{label}: {value !== "" ? value : "None"}</Text>
                            </Link>
                        ) : (
                            <Text fontSize="xl">{label}: {value !== "" ? value : "None"}</Text>
                        )
                    )}
                </Box>
            ))}
        </Flex>
    </Box >
}
