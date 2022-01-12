import { useState } from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./nav.css"
import ReactTooltip from 'react-tooltip';
import { cardItemDisplay, resetCardItemDisplay } from "../body/body.functions";

const Nav = (props) => {
    const [navCardCheck, setNavCardCheck] = useState(false);

    const navMenuSettings = (cardIndex) => {
        props.setCardItemCheck(cardIndex, false);
        cardItemDisplay();
    }
    return (
        <div className="cardNav">
            <ReactTooltip />
            <h1 onClick={() => {
                navMenuSettings();
            }} data-tip="Home" className="menuItem fleetTitle">Fleetmanager</h1>

            <div onClick={() => {
                navMenuSettings(0);
            }} className="navItem carNav">
                <div className="itemPosition">
                    <img className="menuItem carItem" src='/images/carIcon.png' />
                    <span>Vehicle's</span>
                </div>
            </div>

            <div onClick={() => {
                navMenuSettings(1);
            }} className="navItem fuelcardNav">
                <div className="itemPosition">
                    <img className="menuItem fuelcardItem" src='/images/cardIcon.png' />
                    <span>Fuelcard's</span>
                </div>
            </div>

            <div onClick={() => {
                navMenuSettings(2);
            }} className="navItem requestNav">
                <div className="itemPosition">
                    <img className="menuItem requestItem" src='/images/docIcon.png' />
                    <span>Request's</span>
                </div>

            </div>
        </div>
    )
}

export default Nav;