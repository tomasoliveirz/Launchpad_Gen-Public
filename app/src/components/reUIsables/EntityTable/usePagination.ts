import { useState, useCallback } from 'react';

function usePagination<T>(itemsPerPage: number) {
  const [currentPage, setCurrentPage] = useState(1);


  const paginate = useCallback((items: T[]): T[] => {
    const numberOfItems = items.length;
    const numberOfPages = Math.ceil(numberOfItems / itemsPerPage);
    const validPage = Math.min(Math.max(currentPage, 1), numberOfPages);
    const startIndex = (validPage - 1) * itemsPerPage;
    return items.slice(startIndex, startIndex + itemsPerPage);
  }, [currentPage, itemsPerPage]);

  const getPage = useCallback((items: T[], page: number): T[] => {
    const numberOfItems = items.length;
    const numberOfPages = Math.ceil(numberOfItems / itemsPerPage);
    const validPage = Math.min(Math.max(page, 1), numberOfPages);
    setCurrentPage(validPage);
    const startIndex = (validPage - 1) * itemsPerPage;
    return items.slice(startIndex, startIndex + itemsPerPage);
  }, [itemsPerPage]);

  const first = useCallback((items: T[]): T[] => getPage(items, 1), [getPage]);
  
  const last = useCallback((items: T[]): T[] => {
    const numberOfItems = items.length;
    const numberOfPages = Math.ceil(numberOfItems / itemsPerPage);
    return getPage(items, numberOfPages);
  }, [getPage, itemsPerPage]);
  
  const next = useCallback((items: T[]): T[] => getPage(items, currentPage + 1), [getPage, currentPage]);
  
  const previous = useCallback((items: T[]): T[] => getPage(items, currentPage - 1), [getPage, currentPage]);

  const getNumberOfPages = useCallback((items: T[]): number => {
    const numberOfItems = items.length;
    return Math.ceil(numberOfItems / itemsPerPage);
  }, [itemsPerPage]);


  const getCurrentPage = useCallback((items: T[]): T[] => {
    return paginate(items);
  }, [paginate]);

  return {
    currentPage,
    setCurrentPage,
    paginate,
    getPage,
    first,
    last,
    next,
    previous,
    getNumberOfPages,
    getCurrentPage,
  };
}

export default usePagination;