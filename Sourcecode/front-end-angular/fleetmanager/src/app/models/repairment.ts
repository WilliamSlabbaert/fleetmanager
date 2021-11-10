export interface IRepairment {
    date: Date;
    description: string;
    company: string;
}
export class Repairment implements IRepairment {
    date: Date;
    description: string;
    company: string;

    constructor(date: Date, description: string, company: string) {
        this.date = date;
        this.description = description;
        this.company = company;
    }
}