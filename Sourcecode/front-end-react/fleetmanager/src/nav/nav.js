import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./nav.css"
import ReactTooltip from 'react-tooltip';

const Nav = () => {
    return (
        <div className="cardNav">
            <ReactTooltip />
            <h1 data-tip="Home" className="menuItem fleetTitle">Fleetmanager</h1>
            <ReactTooltip />
            <img data-tip="Vehicle's" className="menuItem carItem" src='/images/carIcon.png' />
            <ReactTooltip />
            <img data-tip="Fuelcard's" className="menuItem fuelcardItem" src='/images/cardIcon.png' />
            <ReactTooltip />
            <img data-tip="Request's" className="menuItem requestItem" src='/images/docIcon.png' />
        </div>
    )
}

export default Nav;