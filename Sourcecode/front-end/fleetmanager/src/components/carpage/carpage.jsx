import React, { useEffect, useContext, useState } from "react";
import { AppContext } from "../context/appcontext/appcontext";
import { AgGridColumn, AgGridReact } from 'ag-grid-react';
import axios from "axios";

import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-material.css';
import "./style/carpage.css"

const AddButton = (props) => {
    const id = props.value
    return (
        <div>
            <button>EDIT</button>
        </div>
    )
}

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
        setMenuName(["menuBtn", "menuList-notactive"])
    }, [])

    const [pageName, setPageName] = useState("carPage-notactive");
    useEffect(() => {
        setTimeout(() => {
            setPageName("carPage-active");
        }, 50)
    }, [])

    useEffect(() => {
        axios.get("https://localhost:44346/Vehicle?PageNumber=1&PageSize=50")
            .then(response => {
                setCars([]);
                rebuildJson(response.data.returnValue);
            }).catch(error => console.log(error));
    }, [])

    return (
        <div className={pageName}>
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
        </div>
    )
}

export default CarPage;