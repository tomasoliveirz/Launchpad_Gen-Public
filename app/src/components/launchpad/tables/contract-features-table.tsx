import { EntityWithNameAndDescription } from "@/models/EntityWithNameAndDescription";
import { Box, Table, TableRootProps } from "@chakra-ui/react";
import { LaunchpadButton } from "../buttons/button";
import { FaPencilAlt, FaTrashAlt } from "react-icons/fa";
import { LaunchpadPagination } from "../pagination/pagination";
import { Link } from "react-router-dom";
import { ContractFeature } from "@/models/ContractFeature";

export interface ContractFeaturesTableProps extends Omit<TableRootProps, "page"> {
  items: ContractFeature[],
  pageCount: number,
  page: number,
  setPage: (page: number) => void;
  editButtonOnClick?: (item: ContractFeature) => void;
  detailsLink: (item: ContractFeature) => string;
  removeButtonOnClick?: (item: ContractFeature) => void;
}
export function ContractFeaturesTable({ items, pageCount, page, setPage, editButtonOnClick, removeButtonOnClick, detailsLink, ...props }: ContractFeaturesTableProps) {
  return <>
    <Table.Root zIndex="0" size="sm" w="90%" striped {...props}>
      <Table.Header>
        <Table.Row>
          <Table.ColumnHeader>Name</Table.ColumnHeader>
          <Table.ColumnHeader>DataType</Table.ColumnHeader>
          <Table.ColumnHeader></Table.ColumnHeader>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {items.map((item) => (
          <Table.Row key={item.uuid}>
            <Table.Cell ps="1em"><Link style={{ color: "white" }} to={detailsLink(item)}>{item.name}</Link></Table.Cell>
            <Table.Cell ps="1em">{item.dataType}</Table.Cell>
            <Table.Cell w="10em">
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

