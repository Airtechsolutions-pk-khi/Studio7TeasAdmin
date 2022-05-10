export type SortColumn = keyof any | '';
export type SortDirection = 'asc' | 'desc' | '';

export interface State {
  page: number;
  pageSize: number;
  searchTerm: string;
  sortColumn: SortColumn;
  sortDirection: SortDirection;
}
