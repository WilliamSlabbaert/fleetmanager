import React, { useContext } from 'react';
import './style/menubtn.css';
import MenuIcon from './style/Hamburger_icon.svg.png'
import CarIcon from './style/car_icon.png'
import FuelcardIcon from './style/fuelcard_icon.png'
import RequestIcon from './style/request_icon.png'
import UserIcon from './style/user_icon.png'
import OffIcon from './style/logout_icon.png'
import ReactTooltip from 'react-tooltip';
import { AppContext } from '../app/appcontext';

const MenuBtn = () => {
    const {menuName, setMenuName} = useContext(AppContext)

    const HandelClick = () => {
        if (menuName[0] === "menuBtn") {
            setMenuName(["menuBtn-active", "menuList-active"])
        } else {
            setMenuName(["menuBtn", "menuList-notactive"])
        }
    }

    return (
        <div>
            <ReactTooltip />
            <div onClick={HandelClick} className={menuName[0]}>
                <img src={MenuIcon} alt="Hamburger icon" style={{ "width": "50px" }} />
            </div>
            <div className={menuName[1]}>
                <div className="menuItem" style={{ "marginTop": "5px" }}>
                    <img data-tip="Car list" src={CarIcon} alt="Car list" style={{ "width": "50px" }} />
                </div>
                <div className="menuItem"  style={{ "marginTop": "70px" }}>
                    <img data-tip="Fuelcard list" src={FuelcardIcon} alt="Fuel card list" style={{ "width": "50px" }} />
                </div>
                <div className="menuItem"  style={{ "marginTop": "145px" }}>
                    <img data-tip="Request list" src={RequestIcon} alt="Request list" style={{ "width": "50px" }} />
                </div>
                <div className="menuItem"  style={{ "marginTop": "215px" }}>
                    <img data-tip="User list" src={UserIcon} alt="User list" style={{ "width": "50px" }} />
                </div>
                <div className="menuItem logout"  style={{ "bottom": "0px","top":"auto" }}>
                    <img data-tip="Logout" src={OffIcon} alt="Logout" style={{ "width": "50px" }} />
                </div>
            </div>
        </div>
    )
}

export default MenuBtn;