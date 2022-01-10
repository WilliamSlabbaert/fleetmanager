import React, { useState, useEffect } from "react";
import { Button } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import "./body.css"
import Nav from "../nav/nav";
import VehicleBody from "../vehiclebody/vehiclebody";
import FuelCardBody from "../fuelcardbody/fuelcardbody";
import RequestBody from "../requestbody/requestbody";

const Body = () => {
    const [userData, setUserData] = useState([])
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
    return (
        
        <div className="container-sm cardPlate">
            
            <Nav loaded={loaded} user={userData} />
            
            {userData.length !== 0 &&
                <div className="cardBody container">
                    <VehicleBody user={userData} />
                    <FuelCardBody user={userData}/>
                    <RequestBody user={userData}/>
                </div>
            }
            {userData.length === 0 &&
                <div className="container-sm errorDiv"><i className="material-icons">traffic</i><br /><h2>Something went wrong</h2></div>
            }

           

        </div>
    );
}

export default Body;