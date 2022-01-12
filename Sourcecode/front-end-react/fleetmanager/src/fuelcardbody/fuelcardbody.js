import React, { useState } from "react";
import "./fuelcardbody.css";
import { cardItemDisplay } from "../body/body.functions";

const FuelCardBody = (props) => {

    const fuelCardSettings = () => {
        props.setCardItemCheck(1,true);
        cardItemDisplay();
    }
    return (
        <div onClick={() => {
            fuelCardSettings();
        }} className={`${props.cardItemCheck[1] ? "hideItem " : "showItem "}cardItem fuelCardBody container`}>
            <div className="infoItem fuelcardInfo">
                <img className="cardImg fuelcardItem" src='/images/cardIcon.png' />
                <span>Total fuelcard's</span>
                <h2>{props.user.returnValue.chauffeurFuelCards.length}</h2>
            </div>
        </div>
    )
};

export default FuelCardBody;