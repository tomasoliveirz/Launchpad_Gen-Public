import { useCallback } from 'react';

function useSearch<T>(searchKeys: (keyof T)[]) {
  const filter = useCallback(
    (items: T[], query: string): T[] => {
      if (!query) return items;
      const lowerQuery = query.toLowerCase();
      return items.filter(item =>
        searchKeys.some(key => {
          const value = item[key];
          return value != null && value.toString().toLowerCase().includes(lowerQuery);
        })
      );
    },
    [searchKeys]
  );

  return { filter, searchKeys };
}

export default useSearch;