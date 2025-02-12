import { EntityWithNameAndDescription } from "@/models/EntityWithNameAndDescription";
import { Table, TableRootProps } from "@chakra-ui/react";
import { useState } from "react";
import { LauchpadButton } from "../buttons/button";
import { FaPencilAlt, FaTrashAlt } from "react-icons/fa";

export interface LauchpadNameTableProps extends TableRootProps {
    items: EntityWithNameAndDescription[]
  }
  export function LauchpadNameTable({ items, ...props }: LauchpadNameTableProps) {
    const [open, setOpen] = useState<boolean>(false);
    return <Table.Root zIndex="0" size="sm" w="40%" striped {...props}>
      <Table.Header>
        <Table.Row>
          <Table.ColumnHeader>Name</Table.ColumnHeader>
          <Table.ColumnHeader></Table.ColumnHeader>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {items.map((item) => (
          <Table.Row key={item.uuid}>
            <Table.Cell>{item.name}</Table.Cell>
            <Table.Cell w="10em">
              <LauchpadButton onClick={() => setOpen(!open)} icon={FaPencilAlt} color="white" bg="none" />
              <LauchpadButton onClick={() => setOpen(!open)} icon={FaTrashAlt} color="white" bg="none" />
            </Table.Cell>
          </Table.Row>
        ))}
      </Table.Body>
    </Table.Root>
  }