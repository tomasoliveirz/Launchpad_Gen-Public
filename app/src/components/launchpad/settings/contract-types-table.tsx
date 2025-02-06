import { Table } from "@chakra-ui/react"
import ContractTypesModal from "./contract-types-modal"



export default function({items, ...props})
{
    return <> <Table.Root {...props}>
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
                  <ContractTypesModal item={item} />
                </Table.Row>
              ))}
            </Table.Body>
          </Table.Root>
            </>
}