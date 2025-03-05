function useOrderBy<T>() {
  const sort = (items: T[], newSortKey: keyof T, order: "asc" | "desc"): T[] => {
    if (!newSortKey) return items;
    return [...items].sort((a, b) => {
      const aVal = a[newSortKey];
      const bVal = b[newSortKey];

      if (aVal < bVal) return order === "asc" ? -1 : 1;
      if (aVal > bVal) return order === "asc" ? 1 : -1;
      return 0;
    });
  };

  return { sort };
}

export default useOrderBy;