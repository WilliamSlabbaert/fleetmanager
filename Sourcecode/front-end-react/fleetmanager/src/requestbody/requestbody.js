import React from "react";
import "./requestbody.css"
import { cardItemDisplay } from "../body/body.functions";

const RequestBody = (props) => {

    const requestCardSettings = () => {
        props.setCardItemCheck(2, true);
        cardItemDisplay();
    }
    return (
        <div onClick={() => {
            requestCardSettings();
        }} className={`${props.cardItemCheck[2] ? "hideItem " : "showItem "} cardItem requestBody container`}>
            <div className="infoItem requestInfo ">
                <img className="cardImg requestItem" src='/images/docIcon.png' />
                <span>Total request's</span>
                <h2>{props.user.returnValue.requests.length}</h2>
            </div>
        </div>
    )
}

export default RequestBody;