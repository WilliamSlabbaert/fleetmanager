import React, { useEffect, useState, useContext, useRef } from 'react';
import { useParams } from "react-router-dom";
import { AppContext } from 'components/context/appcontext/appcontext.jsx';
import { Card, Container } from 'react-bootstrap';
import DataTable from "react-data-table-component";
import { columnsChaffeurs,columnsRequests } from './utility/datatable_carpage';

import 'bootstrap/dist/css/bootstrap.min.css';
import './style/carpagedetail.css'
const cardProperties = {
    width: '15rem'
    , display: "inline-block"
    , border: "none"
    , borderRadius: "20px"
    , color: "white"
}


const CarPageDetail = () => {
    const { id } = useParams();
    const [car, setCar] = useState({});
    const [requests, setRequests] = useState([]);
    const [chaffeurs, setChaffeurs] = useState([]);
    const { setMenuName } = useContext(AppContext);
    const cardRef = useRef(null);


    useEffect(() => {
        const getCar = async () => {
            const res = await fetch(`https://localhost:44346/Vehicle/${id}`)
            const data = await res.json();
            const carData = data.returnValue;
            const date = carData.buildDate.split('T')[0]
            setCar({
                chassis: carData.chassis === null ? 'null' : carData.chassis,
                brand: carData.brand === null ? 'null' : carData.brand,
                model: carData.model === null ? 'null' : carData.model,
                fuelType: carData.fuelType === null ? 'null' : carData.fuelType,
                type: carData.type === null ? 'null' : carData.type,
                buildDate: date === null ? 'null' : date
            })
        }
        const getRequests = async () => {
            const res = await fetch(`https://localhost:44346/Vehicle/${id}/Request`)
            const data = await res.json();
            const requestData = data.returnValue;
            setRequests(requestData);
        }
        const getChaffeurs = async () => {
            const res = await fetch(`https://localhost:44346/Vehicle/${id}/Chaffeur`)
            const data = await res.json();
            const chaffeursData = data.returnValue;
            setChaffeurs(chaffeursData);
        }
        getCar();
        getChaffeurs();
        getRequests();
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
                <div className="requestTable">
                    <DataTable
                        title="Requests"
                        highlightOnHover
                        columns={columnsRequests}
                        pagination
                        paginationPerPage={3}
                        paginationComponentOptions={{
                            noRowsPerPage: true
                        }}
                        paginationTotalRows={requests.length   }
                        data={requests}
                    />
                </div>
                <div className="requestTable">
                    <DataTable
                        title="Chaffeurs"
                        highlightOnHover
                        columns={columnsChaffeurs}
                        pagination
                        paginationPerPage={3}
                        paginationComponentOptions={{
                            noRowsPerPage: true
                        }}
                        paginationTotalRows={chaffeurs.length   }
                        data={chaffeurs}
                    />
                </div>
            </Container>
        </div>
    );
}

export default CarPageDetail;