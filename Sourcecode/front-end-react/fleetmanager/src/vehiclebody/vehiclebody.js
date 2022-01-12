import React, { useState } from "react";
import "./vehiclebody.css"
import { cardItemDisplay } from "../body/body.functions";

const VehicleBody = (props) => {

    const vehicleCardSettings = () => {
        props.setCardItemCheck(0,true);
        cardItemDisplay();
    }
    return (
        <div onClick={() => {
            vehicleCardSettings();
        }} className={`${props.cardItemCheck[0] ? "hideItem " : "showItem "}  cardItem vehicleBody container`}>
            <div className="infoItem vehicleInfo">
                <img className="cardImg carItem" src='/images/carIcon.png' />
                <span>Total vehicle's</span>
                <h2>{props.user.returnValue.chauffeurVehicles.length}</h2>
            </div>
        </div>
    )
}

export default VehicleBody;