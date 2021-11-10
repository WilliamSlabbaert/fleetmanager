export interface IInvoice{
    invoiceImage : string;
}

export class Invoice implements IInvoice{
    invoiceImage: string;
    constructor(invoice : string){
        this.invoiceImage = invoice;
    }
}