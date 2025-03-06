import { Table, TableCell, TableColumnHeaderProps, TableRootProps } from "@chakra-ui/react/table"
import { JSX, useEffect, useMemo, useState } from "react"
import { SearchInput } from "../ControlledInput/search-input"
import { Box, HStack, Spacer, StackProps, Text } from "@chakra-ui/react"
import useSearch from "./useSearch"
import useOrderBy from "./useOrderBy"
import usePagination from "./usePagination"
import { FaAngleDoubleLeft, FaAngleDoubleRight, FaAngleLeft, FaAngleRight, FaSortAlphaDown, FaSortAlphaUp, FaSortAmountDown, FaSortAmountUp, FaSortNumericDown, FaSortNumericUp } from "react-icons/fa"
import { Link } from "react-router-dom"

export interface EntityTableProps<T> extends TableRootProps
{
    columnDescriptions:EntityColumnHeaderProps<T>[]
    searchable?:boolean
    pageable?:boolean
    itemsPerPage?:number
    items:T[]
    rowLastColumn?:(t:T)=>JSX.Element
    topLeftElement?:JSX.Element
}

export interface EntityColumnHeaderProps<T>  extends TableColumnHeaderProps
{
    dataKey:keyof(T)
    orderable?:boolean
    searchable?:boolean
    displayable?:boolean
    formatCell?:(s:string)=>JSX.Element
    format?:(s:string)=>string
    dataType?:"text"|"number"|"amount"
    onSort?:(k:keyof T)=>void
    currentSortKey?:keyof T
    currentSortOrder?:"asc"|"desc"
    label?:string
    link?:(t:T)=>string
}

export interface EntityColumnCellProps<T> extends EntityColumnHeaderProps<T>
{
    record:T
}


export function EntityHeaderCell<T>({label,orderable, dataKey, currentSortKey, currentSortOrder, dataType, onSort, ...props}:EntityColumnHeaderProps<T>)
{
    const sortOperation = onSort ? ()=>onSort(dataKey):()=>{};
    const headerLabel = <Text>{label ?? (dataKey as string)}</Text>;
    const orderIcon = (dataKey === currentSortKey && currentSortOrder === "asc") ?
                        (dataType === "amount" ? <FaSortAmountDown/> :
                         dataType === "number" ? <FaSortNumericDown/>:
                                                 <FaSortAlphaDown/>):
                        (dataType === "amount" ? <FaSortAmountUp/> :
                            dataType === "number" ? <FaSortNumericUp/>:
                                                    <FaSortAlphaUp/>)
    
    return <Table.ColumnHeader {...props}>
                {orderable ? 
                <HStack onClick={sortOperation}>
                    {headerLabel}
                    <Spacer/>
                    {orderIcon}
                </HStack>:
                <HStack>{headerLabel}</HStack>    
            }
            </Table.ColumnHeader>
}


export function EntityRowCell<T>({record, link, format, formatCell,dataKey, ...props}:EntityColumnCellProps<T>)
{
    const content= record[dataKey] as string;
    const cellContent = formatCell ? formatCell(content) : <Text>{format ? format(content) : content}</Text>;
    return <Table.Cell {...props}>
                {link ? <Link to={link(record)}>{cellContent}</Link> : cellContent}
            </Table.Cell>
}

export default function EntityTable<T>({columnDescriptions, topLeftElement, pageable, itemsPerPage, searchable, key, rowLastColumn, items, ...props}:EntityTableProps<T>)
{
    const RightSideElement = rowLastColumn;
    const [query, setQuery] = useState<string|undefined>("");
    const [sortKey, setSortKey] = useState<keyof T | undefined>(columnDescriptions[0].dataKey);
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
        if(sortKey !== dataKey)
        {
            setSortKey(dataKey);
            setSortOrder("asc");
        }
        else setSortOrder(sortOrder === "asc" ? "desc":"asc")
   }

    const leftovers = (itemsPerPage ?? 10)- paginatedItems.length;
    return <>
        {searchable && <SearchSection topLeftElement={topLeftElement} setQuery={setQuery} query={query}/>}
        <Table.Root size="sm" striped {...props}>
                <Table.Header>
                    <Table.Row>
                    {columnDescriptions.filter(x => x.displayable).map((d,cdx)=>{
                        const headerKey= "table-" + (key?.toLocaleString()??"")+"-header-column-"+cdx; 
                        return <EntityHeaderCell {...d} onSort={onHeaderSort} key={headerKey} />;
                    })}
                    {RightSideElement && <Table.ColumnHeader w="100%">
                        </Table.ColumnHeader>}
                    </Table.Row>
                </Table.Header>
                <Table.Body>
                    {paginatedItems.map((r, rdx)=>{
                        return <Table.Row key={"table-"+key+"-row-"+rdx}>
                            {columnDescriptions.filter(x => x.displayable).map((c, cdx) =>{
                                const cellKey= "table-" + (key?.toLocaleString()??"")+"-row-"+rdx+"-column"+cdx;
                                return <EntityRowCell w={(cdx!=0 && cdx===columnDescriptions.length-1)? "100%" : "initial"} record={r} {...c} key={cellKey}/> 
                            })}
                            {RightSideElement && <Table.Cell w="100%">
                                {RightSideElement(r)}
                            </Table.Cell>}
                        </Table.Row>
                    })}
                    {((pageable || itemsPerPage) && leftovers > 0) && Array.from(Array(leftovers).keys()).map((x)=>{
                        const cellKey= "table-" + (key?.toLocaleString()??"")+"-row-"+paginatedItems.length+1+x;
                        return <Table.Row  key={cellKey}>
                                <Table.Cell colSpan={columnDescriptions.filter(x => x.displayable).length+(RightSideElement ? 1:0)}>
                                &nbsp;
                                </Table.Cell>
                                </Table.Row>
                    })}
                </Table.Body>
            </Table.Root>
            <PaginationSection first={first} next={next} previous={previous} last={last} currentPage={currentPage} getNumberOfPages={getNumberOfPages} items={sortedItems} />
            </>
}


interface SearchSectionProps<T> extends StackProps
{
    query:string|undefined
    setQuery:((a:string|undefined) => void)
    topLeftElement?:JSX.Element
}

function SearchSection<T>({query, topLeftElement, setQuery, ...props}:SearchSectionProps<T>)
{
    const TopLeftElement = topLeftElement;
    return <HStack mb="0.5em" {...props} >
                {TopLeftElement??<></>}
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