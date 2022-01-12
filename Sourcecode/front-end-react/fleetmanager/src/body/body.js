import React, { useState, useEffect } from "react";
import { Button } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import "./body.css"
import Nav from "../nav/nav";
import VehicleBody from "../vehiclebody/vehiclebody";
import FuelCardBody from "../fuelcardbody/fuelcardbody";
import RequestBody from "../requestbody/requestbody";
import Grid from "../grid/grid";

const Body = () => {
    const [userData, setUserData] = useState([])
    const [cardItemCheck, setCardItemCheck] = useState([false, false, false])
    const [loaded, setLoaded] = useState(false)
    const [error, setError] = useState(null);

    useEffect(() => {
        async function fetchApi() {
            setLoaded(false);
            try {
                let response = await fetch(`https://localhost:44346/Chauffeur/1`);
                response = await response.json();
                setUserData(response);
                if (userData.length !== 0) {
                    setLoaded(true);
                }
            }
            catch (err) {
                setError(err);
            }
        }
        fetchApi();
    }, [])

    const cardItemToBodyCheck = (index, check) => {
        let temp = [false, false, false]
        if (check) {
            if (cardItemCheck[0] === false && 
                cardItemCheck[1] === false && 
                cardItemCheck[2] === false) {
                temp = setCardItems(index)
            }
        } else {
            temp = setCardItems(index)
        }
        setCardItemCheck(temp);
    }
    const setCardItems = (index) => {
        let temp = [false, false, false];
        if (index === 0) {
            temp = [false, true, true];
        }
        else if (index === 1) {
            temp = [true, false, true];
        }
        else if (index === 2) {
            temp = [true, true, false];
        }
        return temp;
    }
    return (

        <div className="container-sm cardPlate">

            <Nav loaded={loaded} user={userData} setCardItemCheck={cardItemToBodyCheck} />

            {userData.length !== 0 &&
                <div className="cardBody container">
                    <VehicleBody user={userData} setCardItemCheck={cardItemToBodyCheck} cardItemCheck={cardItemCheck} />
                    <FuelCardBody user={userData} setCardItemCheck={cardItemToBodyCheck} cardItemCheck={cardItemCheck} />
                    <RequestBody user={userData} setCardItemCheck={cardItemToBodyCheck} cardItemCheck={cardItemCheck} />
                    <Grid/>
                </div>
            }
            {userData.length === 0 &&
                <div className="container-sm errorDiv"><i className="material-icons">traffic</i><br /><h2>Something went wrong</h2></div>
            }



        </div>
    );
}

export default Body;