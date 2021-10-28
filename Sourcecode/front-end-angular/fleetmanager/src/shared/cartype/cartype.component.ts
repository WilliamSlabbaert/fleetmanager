import { Component, Input, Output,EventEmitter } from "@angular/core";

@Component({
    selector: 'pm-cartype',
    templateUrl: './cartype.component.html'
})

export class CarTypeComponent {
    @Input() type: string = "";
    @Output() messageTest : EventEmitter<string> = new EventEmitter<string>();
    onClickEmit(){
        this.messageTest.emit('okokoko');
    }

}