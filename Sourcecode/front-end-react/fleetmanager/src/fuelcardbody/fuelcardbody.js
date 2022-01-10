import React from "react";
import "./fuelcardbody.css";

const FuelCardBody = (props) =>{
    return (
        <div className="cardItem fuelCardBody container">
            <div className="infoItem fuelcardInfo">
            <img className="cardImg menuItem fuelcardItem" src='/images/cardIcon.png' />
                <span>Total fuelcard's</span>
                <h2>{props.user.returnValue.chauffeurFuelCards.length}</h2>
            </div>
        </div>
    )
};

export default FuelCardBody;