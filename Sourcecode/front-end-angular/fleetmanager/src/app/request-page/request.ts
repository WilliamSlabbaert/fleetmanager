export interface IRequest {
    startDate: Date;
    endDate: Date;
    status: string;
    type: number;
}
export class Request implements IRequest {
    startDate: Date;
    endDate: Date;
    status: string;
    type: number;
    constructor(start: Date, end: Date, status: string, type: number) {
        this.startDate = start;
        this.endDate = end;
        this.status = status;
        this.type = type;
    }

}