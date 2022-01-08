import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./nav.css"
import ReactTooltip from 'react-tooltip';

const Nav = (props) => {
    return (
        <div className="cardNav">
            <ReactTooltip />
            <h1 data-tip="Home" className="menuItem fleetTitle">Fleetmanager</h1>

            <div className="navItem carNav">
                <div className="itemPosition">
                    <img className="menuItem carItem" src='/images/carIcon.png' />
                    <span>Vehicle's</span>
                </div>
            </div>

            <div className="navItem fuelcardNav">
                <div className="itemPosition">
                    <img className="menuItem fuelcardItem" src='/images/cardIcon.png' />
                    <span>Fuelcard's</span>
                </div>
            </div>

            <div className="navItem requestNav">
                <div className="itemPosition">
                    <img className="menuItem requestItem" src='/images/docIcon.png' />
                    <span>Request's</span>
                </div>

            </div>
        </div>
    )
}

export default Nav;