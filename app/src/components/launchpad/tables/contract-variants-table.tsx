import { ContractVariant } from "@/models/ContractVariant";
import { Box, Table, TableRootProps } from "@chakra-ui/react";
import { LaunchpadButton } from "../buttons/button";
import { FaPencilAlt, FaTrashAlt } from "react-icons/fa";
import { LaunchpadPagination } from "../pagination/pagination";
import { useEntity } from "@/services/launchpad/testService";
import { ContractType } from "@/models/ContractType";

export interface LaunchpadContractVariantTableProps extends Omit<TableRootProps, "page"> {
    items: ContractVariant[],
    pageCount: number,
    page: number,
    setPage: (page: number) => void;
    editButtonOnClick?: (item: ContractVariant) => void;
    removeButtonOnClick?: (item: ContractVariant) => void;
}
export function LaunchpadContractVariantTable({ items, pageCount, page, setPage, editButtonOnClick, removeButtonOnClick, ...props }: LaunchpadContractVariantTableProps) {
    return <>
        <Table.Root zIndex="0" size="sm" w="60%" striped {...props}>
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
                    <Table.Cell ps="1em">{item.name}</Table.Cell>
                    <ContractTypeCell uuid={item.contractType.uuid} />
                    <Table.Cell w="10em">
                        <LaunchpadButton onClick={() => editButtonOnClick?.(item)} icon={FaPencilAlt} color="white" bg="none" />
                        <LaunchpadButton onClick={() => removeButtonOnClick?.(item)} icon={FaTrashAlt} color="white" bg="none" />
                    </Table.Cell>
                </Table.Row>
                ))}
            </Table.Body>
        </Table.Root>
        <Box mt="2em" w="60%" display="flex" justifyContent="flex-end">
            <LaunchpadPagination
                page={page}
                pageCount={pageCount}
                onPageChange={setPage}
            />
        </Box>
    </>
}

export interface ContractTypeCellProps {
    uuid: string;
}
export function ContractTypeCell({ uuid }: ContractTypeCellProps) {
    const entityApiContractType = useEntity<ContractType>("ContractTypes");

    const { data: contractType } = entityApiContractType.get(uuid);
    const contractTypeData = contractType as ContractType;

    return (
        <Table.Cell ps="1em">
            {contractTypeData.name}
        </Table.Cell>
    );
};