import React, { useEffect, useContext, useState } from "react";
import { AppContext } from "../context/appcontext/appcontext";
import { AgGridColumn, AgGridReact } from 'ag-grid-react';
import axios from "axios";
import { Link } from "react-router-dom";
import GeneralPage from "../generalpage/generalpage";

import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-material.css';
import "./style/carpage.css"



const CarPage = () => {
    const { setMenuName } = useContext(AppContext);
    const [cars, setCars] = useState([]);

    const rebuildJson = (data) => {
        console.log(data)
        data.forEach(element => {
            setCars(cars => [...cars, {
                chassis: element.chassis,
                brand: element.brand,
                fuelType: element.fuelType,
                type: element.type,
                model: element.model,
                edit: element.id
            }])
        });
    }



    useEffect(() => {
        axios.get("https://localhost:44346/Vehicle?PageNumber=1&PageSize=50")
            .then(response => {
                setCars([]);
                rebuildJson(response.data.returnValue);
            }).catch(error => console.log(error));
        setMenuName(["menuBtn", "menuList-notactive"])

    }, [])



    return (
        <div className="ag-theme-material " >
            <AgGridReact
                frameworkComponents={{
                    addBtn: AddButton,
                }}
                defaultColDef={{
                    sortable: true,
                    filter: true,
                    flex: 1,
                }}
                enableRangeSelection={true}
                pagination={true}
                paginationPageSize={5}
                rowData={cars}>
                <AgGridColumn field="chassis" sortable={true} filter={true}></AgGridColumn>
                <AgGridColumn field="brand" sortable={true} filter={true}></AgGridColumn>
                <AgGridColumn field="model" sortable={true} filter={true}></AgGridColumn>
                <AgGridColumn field="type" sortable={true} filter={true}></AgGridColumn>
                <AgGridColumn field="fuelType" sortable={true} filter={true}></AgGridColumn>
                <AgGridColumn field="edit" cellRenderer="addBtn"></AgGridColumn>
            </AgGridReact>
        </div>
    )
}

const AddButton = (props) => {
    const id = "/Car/" + props.value
    return (
        <div>
            <Link to={id} style={{ "display": "contents" }}>
                <button>EDIT</button>
            </Link>
        </div>
    )
}

export default CarPage;