import { ContractVariant} from "@/models/ContractVariant";
import { Box, Table, TableRootProps } from "@chakra-ui/react";
import { LaunchpadButton } from "../buttons/button";
import { FaPencilAlt, FaPlus, FaTrashAlt } from "react-icons/fa";
import { LaunchpadPagination } from "../pagination/pagination";
import { Link } from "react-router-dom";

export interface LaunchpadContractVariantTableProps extends Omit<TableRootProps, "page"> {
    items: ContractVariant[],
    pageCount: number,
    page: number,
    setPage: (page: number) => void;
    variantDetailsLink: (item: ContractVariant) => string;
    typeDetailsLink: (item: ContractVariant) => string;
    addCharacteristic: (item: ContractVariant) => void;
    editButtonOnClick: (item: ContractVariant) => void;
    removeButtonOnClick: (item: ContractVariant) => void;
}
export function LaunchpadContractVariantTable({ items, pageCount, page, setPage, addCharacteristic, editButtonOnClick, removeButtonOnClick, variantDetailsLink, typeDetailsLink, ...props }: LaunchpadContractVariantTableProps) {
    return <>
        <Table.Root zIndex="0" size="sm" w="90%" striped {...props}>
            <Table.Header>
                <Table.Row>
                    <Table.ColumnHeader>Name</Table.ColumnHeader>
                    <Table.ColumnHeader>Contract Type</Table.ColumnHeader>
                    <Table.ColumnHeader></Table.ColumnHeader>
                </Table.Row>
            </Table.Header>
            <Table.Body>
                {items.map((item) => (
                    <Table.Row key={item.uuid}>
                    <Table.Cell ps="1em"><Link style={{ color: "white" }} to={variantDetailsLink(item)}>{item.name}</Link></Table.Cell>
                    <Table.Cell ps="1em"><Link style={{ color: "white" }} to={typeDetailsLink(item)}>{item.contractType.name}</Link></Table.Cell>
                    <Table.Cell w="14em">
                        <LaunchpadButton onClick={() => addCharacteristic?.(item)} icon={FaPlus} color="white" bg="none" />
                        <LaunchpadButton onClick={() => editButtonOnClick?.(item)} icon={FaPencilAlt} color="white" bg="none" />
                        <LaunchpadButton onClick={() => removeButtonOnClick?.(item)} icon={FaTrashAlt} color="white" bg="none" />
                    </Table.Cell>
                </Table.Row>
                ))}
            </Table.Body>
        </Table.Root>
        <Box mt="1em" w="90%" display="flex" justifyContent="flex-end">
            <LaunchpadPagination
                page={page}
                pageCount={pageCount}
                onPageChange={setPage}
            />
        </Box>
    </>
}
