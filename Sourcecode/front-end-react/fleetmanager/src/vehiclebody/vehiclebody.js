import React from "react";
import "./vehiclebody.css"

const VehicleBody = (props) =>{
    return(
        <div className="cardItem vehicleBody container">
            <div className="infoItem vehicleInfo">
                <img className="cardImg menuItem carItem" src='/images/carIcon.png' />
                <span>Total vehicle's</span>
                <h2>{props.user.returnValue.chauffeurVehicles.length}</h2>
            </div>
        </div>
    )
}

export default VehicleBody;