import { BlockchainNetwork } from "@/models/BlockchainNetwork";
import { Box, Table, TableRootProps } from "@chakra-ui/react";
import { Image } from '@chakra-ui/react';
import { LaunchpadButton } from "../buttons/button";
import { FaPencilAlt, FaQuestion, FaTrashAlt } from "react-icons/fa";
import { LaunchpadPagination } from "../pagination/pagination";

export interface BlockchainNetworkTableProps extends Omit<TableRootProps, "page"> {
    items: BlockchainNetwork[],
    pageCount: number,
    page: number,
    setPage: (page: number) => void;
    editButtonOnClick?: (item: BlockchainNetwork) => void;
    removeButtonOnClick?: (item: BlockchainNetwork) => void;
}
export function BlockchainNetworksTable({ items, pageCount, page, setPage, editButtonOnClick, removeButtonOnClick, ...props }: BlockchainNetworkTableProps) {
    return <>
        <Table.Root zIndex="0" size="sm" w="90%" striped {...props}>
            <Table.Header>
                <Table.Row>
                    <Table.ColumnHeader></Table.ColumnHeader>
                    <Table.ColumnHeader>Name</Table.ColumnHeader>
                    <Table.ColumnHeader></Table.ColumnHeader>
                </Table.Row>
            </Table.Header>
            <Table.Body>
                {items.map((item) => (
                    <Table.Row key={item.uuid}>
                        <Table.Cell w="5rem">
                            {item.image ? (
                                <Image
                                    height="60px"
                                    src={item.image}
                                    alt={item.name}
                                />) : (
                                    <Box fontSize="50px">
                                        <FaQuestion/>
                                    </Box>
                            )}
                        </Table.Cell>
                        <Table.Cell ps="1em">{item.name}</Table.Cell>
                        <Table.Cell w="10em">
                            <LaunchpadButton onClick={() => editButtonOnClick?.(item)} icon={FaPencilAlt} color="white" bg="none" />
                            <LaunchpadButton onClick={() => removeButtonOnClick?.(item)} icon={FaTrashAlt} color="white" bg="none" />
                        </Table.Cell>
                    </Table.Row>
                ))}
            </Table.Body>
        </Table.Root>
        <Box mt="1em" w="90%" maxW="100%" display="flex" justifyContent="flex-end">
            <LaunchpadPagination
                page={page}
                pageCount={pageCount}
                onPageChange={setPage}
            />
        </Box>
    </>
}