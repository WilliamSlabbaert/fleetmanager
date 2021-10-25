import React, { useEffect, useState, useContext, useRef } from 'react';
import { useParams } from "react-router-dom";
import { AppContext } from 'components/context/appcontext/appcontext.jsx';
import { Card, Container } from 'react-bootstrap';
import DataTable from "react-data-table-component";
import { columnsChauffeurs,columnsRequests,columnsPlates,columnsKilometers } from './utility/datatable_carpage';

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
    const [chauffeurs, setChauffeurs] = useState([]);
    const [plates, setPlates] = useState([]);
    const [kilometers, setKilometers] = useState([]);
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

        const getItems= async (URL, type) => {
            const res = await fetch(URL)
            const data = await res.json();
            const json = data.returnValue;
            if(type === 1)
                setPlates(json);
            else if( type === 2)
                setRequests(json);
            else if(type === 3)
            setChauffeurs(json);
            else
                setKilometers(json);
        }

        getCar();
        getItems(`https://localhost:44346/Vehicle/${id}/Licenseplate`,1)
        getItems(`https://localhost:44346/Vehicle/${id}/Request`,2)
        getItems(`https://localhost:44346/Vehicle/${id}/Chaffeur`,3)
        getItems(`https://localhost:44346/Vehicle/${id}/KilometerHistory`,4)
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
                        title="Chauffeurs"
                        highlightOnHover
                        columns={columnsChauffeurs}
                        pagination
                        paginationPerPage={3}
                        paginationComponentOptions={{
                            noRowsPerPage: true
                        }}
                        paginationTotalRows={chauffeurs.length   }
                        data={chauffeurs}
                    />
                </div>
                <div className="platesTable">
                    <DataTable
                        title="Licenseplates"
                        highlightOnHover
                        columns={columnsPlates}
                        pagination
                        paginationPerPage={3}
                        paginationComponentOptions={{
                            noRowsPerPage: true
                        }}
                        paginationTotalRows={plates.length   }
                        data={plates}
                    />
                </div>
                <div className="kilometersTable">
                    <DataTable
                        title="Kilometer history"
                        highlightOnHover
                        columns={columnsKilometers}
                        pagination
                        paginationPerPage={3}
                        paginationComponentOptions={{
                            noRowsPerPage: true
                        }}
                        paginationTotalRows={kilometers.length}
                        data={kilometers}
                    />
                </div>
            </Container>
        </div>
    );
}

export default CarPageDetail;