export interface IPaginationHeader{
    TotalCount : number;
    PageSize : number;
    CurrentPage: number;
    HasNext : boolean;
    HasPrevious : boolean;
}