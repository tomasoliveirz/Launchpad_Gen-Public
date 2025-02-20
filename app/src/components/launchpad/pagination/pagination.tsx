import { PaginationItems, PaginationNextTrigger, PaginationPrevTrigger, PaginationRoot } from "@/components/ui/pagination";
import { Group } from "@chakra-ui/react";

interface LaunchpadPaginationProps{
    page: number;
    pageCount: number;
    onPageChange: (page: number) => void;
  }
  
  export function LaunchpadPagination({ page, pageCount, onPageChange }: LaunchpadPaginationProps) {
    return (
      <PaginationRoot
        count={pageCount}
        pageSize={1}
        page={page}
        onPageChange={(e) => onPageChange(e.page)}
        variant="outline"
      >
        <Group attached>
          <PaginationPrevTrigger />
          <PaginationItems />
          <PaginationNextTrigger />
        </Group>
      </PaginationRoot>
    );
  }