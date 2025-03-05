import { Table, TableCell, TableColumnHeaderProps, TableRootProps } from "@chakra-ui/react/table"
import { JSX, useEffect, useMemo, useState } from "react"
import { SearchInput } from "../ControlledInput/search-input"
import { Box, HStack, Spacer, StackProps, Text } from "@chakra-ui/react"
import useSearch from "./useSearch"
import useOrderBy from "./useOrderBy"
import usePagination from "./usePagination"
import { FaAngleDoubleLeft, FaAngleDoubleRight, FaAngleLeft, FaAngleRight, FaSortAlphaDown } from "react-icons/fa"

export interface EntityTableProps<T> extends TableRootProps
{
    columnDescriptions:EntityColumnHeaderProps<T>[]
    searchable?:boolean
    pageable?:boolean
    itemsPerPage?:number
    items:T[]
    rightSideElement?:(t:T)=>JSX.Element
}

export interface EntityColumnHeaderProps<T>  extends TableColumnHeaderProps
{
    dataKey:keyof(T)
    orderable?:boolean
    searchable?:boolean
    label?:string
    link?:(t:T)=>string
}

export interface EntityColumnCellProps<T> extends EntityColumnHeaderProps<T>
{
    record:T
}


export function EntityHeaderCell<T>({label,orderable, ...props}:EntityColumnHeaderProps<T>)
{
    return <Table.ColumnHeader {...props}>
                <HStack>
                    <Text>{label ?? (props.dataKey as string)}</Text>
                    {orderable && <FaSortAlphaDown/>}
                </HStack>
            </Table.ColumnHeader>
}


export function EntityRowCell<T>({record, dataKey, ...props}:EntityColumnCellProps<T>)
{
    return <Table.Cell {...props}>
                {record[dataKey] as string}
            </Table.Cell>
}

export default function EntityTable<T>({columnDescriptions, pageable, itemsPerPage, searchable, key, rightSideElement, items, ...props}:EntityTableProps<T>)
{
    const RightSideElement = rightSideElement;
    const [query, setQuery] = useState<string|undefined>("");
    const [sortKey, setSortKey] = useState<keyof T | undefined>(undefined);
    const [sortOrder, setSortOrder] = useState<"asc" | "desc">("asc");

    const dataKeys = useMemo(
        () => columnDescriptions.filter(h => h.searchable).map(h => h.dataKey),
        [columnDescriptions]
    );
    const { filter } = useSearch<T>(dataKeys);
    const { sort } = useOrderBy<T>();
    const { paginate, first, next, previous, last, currentPage, getNumberOfPages } = usePagination<T>(itemsPerPage ?? (pageable? 10:items.length));

  const filteredItems = useMemo(() => {
    return filter(items, query??"");
  }, [items, query, filter]);

  const sortedItems = useMemo(() => {
    if (sortKey) {
      return sort(filteredItems, sortKey, sortOrder);
    }
    return filteredItems;
  }, [filteredItems, sortKey, sortOrder, sort]);

  const paginatedItems = useMemo(() => {
    return paginate(sortedItems);
  }, [sortedItems, paginate]);

   function onHeaderSort(dataKey:keyof T)
   {
        sort(items, dataKey, sortKey === dataKey ? (sortOrder === "asc"?"desc":"asc"):"asc");
   }

    const leftovers = (itemsPerPage ?? 10)- paginatedItems.length;
    return <>
        {searchable && <SearchSection setQuery={setQuery} query={query}/>}
        <Table.Root size="sm" w="100%" striped {...props}>
                <Table.Header w="100%">
                    <Table.Row w="100%">
                    {columnDescriptions.map((d,cdx)=>{
                        const headerKey= "table-" + (key?.toLocaleString()??"")+"-header-column-"+cdx; 
                        return <EntityHeaderCell {...d} key={headerKey} />;
                    })}
                    {RightSideElement && <Table.ColumnHeader>
                        </Table.ColumnHeader>}
                    </Table.Row>
                </Table.Header>

                <Table.Body w="100%">
                    {paginatedItems.map((r, rdx)=>{
                        return <Table.Row key={"table-"+key+"-row-"+rdx}>
                            {columnDescriptions.map((c, cdx) =>{
                                const cellKey= "table-" + (key?.toLocaleString()??"")+"-row-"+rdx+"-column"+cdx;
                                return <EntityRowCell  record={r} {...c} key={cellKey}/> 
                            })}
                            {RightSideElement && <Table.Cell>
                                {RightSideElement(r)}
                            </Table.Cell>}
                        </Table.Row>
                    })}
                    {((pageable || itemsPerPage) && leftovers > 0) && Array.from(Array(leftovers).keys()).map((x)=>{
                        return <Table.Row>
                                <Table.Cell colSpan={columnDescriptions.length+(RightSideElement ? 1:0)}>
                                &nbsp;
                                </Table.Cell>
                                </Table.Row>
                    })}
                </Table.Body>
            </Table.Root>
            <PaginationSection first={first} next={next} previous={previous} last={last} currentPage={currentPage} getNumberOfPages={getNumberOfPages} items={items} />
            </>
}


interface SearchSectionProps<T> extends StackProps
{
    query:string|undefined
    setQuery:((a:string|undefined) => void)
}

function SearchSection<T>({query, setQuery, ...props}:SearchSectionProps<T>)
{
    return <HStack mb="0.5em" w="100%" {...props} >
                <Spacer/>
                <SearchInput bg="#00000088" w="20em" value={query} size="sm" onChange={setQuery}/>
            </HStack>
}

interface PaginationSectionProps<T> extends StackProps
{
    items:T[]
    first:(i:T[])=>void, 
    next:(i:T[])=>void, 
    previous:(i:T[])=>void, 
    last:(i:T[])=>void, 
    currentPage:number, 
    getNumberOfPages:(i:T[])=>number
}

export function PaginationSection<T>({items, first, next, previous, last, currentPage, getNumberOfPages, ...props}:PaginationSectionProps<T>)
{
    
    const handleFirst = () => {
        first(items);
    };

    const handlePrevious = () => {
        previous(items);
    };

    const handleNext = () => {
        next(items);
    };

    const handleLast = () => {
        last(items);
    };
        
    return <HStack {...props}>
                <Spacer/>
                <HStack>
                    <FaAngleDoubleLeft onClick={handleFirst}/>
                    <FaAngleLeft onClick={handlePrevious}/>
                    <Text>{currentPage} of {getNumberOfPages(items)}</Text>
                    <FaAngleRight onClick={handleNext}/>
                    <FaAngleDoubleRight onClick={handleLast}/>
                </HStack>
            </HStack>
}