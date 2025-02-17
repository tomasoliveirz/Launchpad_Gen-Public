import { EntityWithNameAndDescription } from "@/models/EntityWithNameAndDescription";
import { Box, Table, TableRootProps } from "@chakra-ui/react";
import { useState } from "react";
import { LaunchpadButton } from "../buttons/button";
import { FaPencilAlt, FaTrashAlt } from "react-icons/fa";
import { LaunchpadPagination} from "../pagination/pagination";

export interface LaunchpadNameTableProps extends Omit<TableRootProps, "page"> {
  items: EntityWithNameAndDescription[],
  pageCount: number,
  page: number,
  setPage: (page: number) => void;

}
export function LaunchpadNameTable({ items, pageCount, page, setPage, ...props }: LaunchpadNameTableProps) {
  const [open, setOpen] = useState<boolean>(false);

  return <>
    <Table.Root zIndex="0" size="sm" w="60%" striped {...props}>
      <Table.Header>
        <Table.Row>
          <Table.ColumnHeader>Name</Table.ColumnHeader>
          <Table.ColumnHeader></Table.ColumnHeader>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {items.map((item) => (
          <Table.Row key={item.uuid}>
            <Table.Cell ps="1em">{item.name}</Table.Cell>
            <Table.Cell w="10em">
              <LaunchpadButton onClick={() => setOpen(!open)} icon={FaPencilAlt} color="white" bg="none" />
              <LaunchpadButton onClick={() => setOpen(!open)} icon={FaTrashAlt} color="white" bg="none" />
            </Table.Cell>
          </Table.Row>
        ))}
      </Table.Body>
    </Table.Root>
    <Box mt="2em">
    <LaunchpadPagination 
        page={page}
        pageCount={pageCount}
        onPageChange={setPage}
      />
    </Box>
  </>
}

