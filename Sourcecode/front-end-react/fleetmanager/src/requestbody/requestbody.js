import React from "react";
import "./requestbody.css"

const RequestBody = (props) => {
    return (
        <div className="cardItem requestBody container">
            <div className="infoItem requestInfo ">
            <img className="cardImg menuItem requestItem" src='/images/docIcon.png' />
                <span>Total request's</span>
                <h2>{props.user.returnValue.requests.length}</h2>
            </div>
        </div>
    )
}

export default RequestBody;