import React, { useEffect, useState, useContext, useRef } from 'react';
import { useParams } from "react-router-dom";
import { AppContext } from '../context/appcontext/appcontext';
import { Card, Container } from 'react-bootstrap';

import 'bootstrap/dist/css/bootstrap.min.css';
import './style/carpagedetail.css'
const cardProperties = {
    width: '15rem'
    , alignItems: "center"
    , display: "inline-block"
    , border: "none"
    , margin: "15px"
    , borderRadius: "20px"
    , color: "white"
}
const CarPageDetail = () => {
    const { id } = useParams();
    const [car, setCar] = useState({ returnValue: {} });
    const { setMenuName } = useContext(AppContext);
    const cardRef = useRef(null);

    useEffect(() => {
        const getCar = async () => {
            const res = await fetch(`https://localhost:44346/Vehicle/${id}`)
            const data = await res.json();
            const carData = data.returnValue;
            const date = carData.buildDate.split('T')[0]
            setCar({
                chassis: carData.chassis,
                brand: carData.brand,
                model: carData.model,
                fuelType : carData.fuelType,
                type: carData.type,
                buildDate: date
            })
        }
        getCar();
        setMenuName(["menuBtn", "menuList-notactive"])
    }, [])


    return (
        <div className="infoDashboard">
            <Container fluid>
                <div className="cardView">
                    
                    <Card
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Chassis: <br /><br /><br />{car.chassis}</h3>
                    </Card>
                    <Card
                        ref={cardRef}
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Brand: <br /><br /><br />{car.brand}</h3>
                    </Card>
                    <Card
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Model: <br /><br /><br />{car.model}</h3>
                    </Card>
                    <Card
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Fueltype: <br /><br /><br />{car.fuelType}</h3>
                    </Card>
                    <Card
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Type: <br /><br /><br />{car.type}</h3>
                    </Card>
                    <Card
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Build date: <br /><br /><br />{car.buildDate}</h3>
                    </Card>
                </div>
            </Container>
        </div>
    );
}

export default CarPageDetail;