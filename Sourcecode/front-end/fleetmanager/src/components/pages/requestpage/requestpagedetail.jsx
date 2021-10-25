import React, { useEffect, useState, useContext, useRef } from 'react';
import { useParams } from "react-router-dom";
import { AppContext } from 'components/context/appcontext/appcontext.jsx';
import { Card, Container } from 'react-bootstrap';
import DataTable from "react-data-table-component";
import { columnsChauffeurs, columnsVehicles,columnsMaintenance ,columnsRepairment} from './utility/datatables_requestpage.jsx';

import 'bootstrap/dist/css/bootstrap.min.css';
import './style/requestpagedetail.css'
const cardProperties = {
    width: '15rem'
    , display: "inline-block"
    , border: "none"
    , borderRadius: "20px"
    , color: "white"
}


const RequestPageDetail = () => {
    const { id } = useParams();
    const [request, setRequest] = useState({});
    const [vehicles, setVehicles] = useState([]);
    const [chauffeurs, setChauffeurs] = useState([]);
    const [maintenance, setMaintenance] = useState([]);
    const [repairment, setRepairment] = useState([]);
    const { setMenuName } = useContext(AppContext);
    const cardRef = useRef(null);


    useEffect(() => {
        const getRequest = async () => {
            const res = await fetch(`https://localhost:44346/Request/${id}`)
            const data = await res.json();
            const requestData = data.returnValue;
            setRequest({
                startDate: requestData.startDate === null ? 'null' : 
                    requestData.startDate.split("T")[0] + " " + 
                    requestData.startDate.split("T")[1].split(':')[0] + ":" + 
                    requestData.startDate.split("T")[1].split(':')[1],
                endDate: requestData.endDate === null ? 'null' : 
                    requestData.endDate.split("T")[0] + " " + 
                    requestData.endDate.split("T")[1].split(':')[0] + ":" + 
                    requestData.endDate.split("T")[1].split(':')[1],
                status: requestData.status === null ? 'null' : requestData.status,
                type: requestData.type === null ? 'null' : requestData.type,
            })
        }

        const getItems= async (URL, type) => {
            const res = await fetch(URL)
            const data = await res.json();
            const json = data.returnValue;
            if(type === 1)
                setVehicles([json]);
            else if( type === 2)
                setChauffeurs([json]);
            else if(type === 3)
                setMaintenance(json);
            else
                setRepairment(json);
        }

        getRequest();
        getItems(`https://localhost:44346/Request/${id}/Vehicle`, 1);
        getItems(`https://localhost:44346/Request/${id}/Chaffeur`, 2);
        getItems(`https://localhost:44346/Request/${id}/Maintenance`, 3);
        getItems(`https://localhost:44346/Request/${id}/Repairment`, 4);
        setMenuName(["menuBtn", "menuList-notactive"])
    }, [])

    return (
        <div className="infoDashboard">
            <Container fluid>
                <div className="cardView">

                    <Card
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Start date: <br /><br /><br />{request.startDate}</h3>
                    </Card>
                    <Card
                        ref={cardRef}
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Brand: <br /><br /><br />{request.endDate}</h3>
                    </Card>
                    <Card
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Status: <br /><br /><br />{request.status}</h3>
                    </Card>
                    <Card
                        className="mx-1 my-1 cardItems"
                        style={cardProperties}>
                        <h3>Type: <br /><br /><br />{request.type}</h3>
                    </Card>
                </div>
                <div className="vehicleTable">
                    <DataTable
                        title="Vehicles"
                        highlightOnHover
                        columns={columnsVehicles}
                        paginationPerPage={3}
                        paginationComponentOptions={{
                            noRowsPerPage: true
                        }}
                        paginationTotalRows={vehicles.count}
                        data={vehicles}
                    />
                </div>
                <div className="chauffeurTable">
                    <DataTable
                        title="Chauffeurs"
                        highlightOnHover
                        columns={columnsChauffeurs}
                        pagination
                        paginationPerPage={3}
                        paginationComponentOptions={{
                            noRowsPerPage: true
                        }}
                        paginationTotalRows={chauffeurs.count}
                        data={chauffeurs}
                    />
                </div>
                <div className="maintenanceTable">
                    <DataTable
                        title="Maintenance's"
                        highlightOnHover
                        columns={columnsMaintenance}
                        pagination
                        paginationPerPage={3}
                        paginationComponentOptions={{
                            noRowsPerPage: true
                        }}
                        paginationTotalRows={maintenance.count}
                        data={maintenance}
                    />
                </div>
                <div className="repairmentTable">
                    <DataTable
                        title="Repairments"
                        highlightOnHover
                        columns={columnsRepairment}
                        pagination
                        paginationPerPage={3}
                        paginationComponentOptions={{
                            noRowsPerPage: true
                        }}
                        paginationTotalRows={repairment.count}
                        data={repairment}
                    />
                </div>
            </Container>
        </div>
    );
}

export default RequestPageDetail;