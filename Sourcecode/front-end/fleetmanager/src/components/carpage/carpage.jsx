import React, { useEffect, useContext, useState } from "react";
import { AppContext } from "../context/appcontext/appcontext";
import axios from "axios";
import DataTable from "react-data-table-component";

import "./style/carpage.css"

const columns = [
    {
        name: '',
        cell: row => <button height="30px" width="30px" href={row.id}>EDIT</button>,
    },
    {
        name: 'Chassis',
        selector: row => row.chassis,
    },
    {
        name: 'Model',
        selector: row => row.model
    },
    {
        name: 'Brand',
        selector: row => row.brand
    },
    {
        name: 'Type',
        selector: row => row.type
    },
    {
        name: 'Fueltype',
        selector: row => row.fuelType
    }
];

const CarPage = () => {
    const { setMenuName } = useContext(AppContext);
    const [cars, setCars] = useState([]);
    const [page, setPage] = useState(1);
    const countPerPage = 3;
    const [totalCount, setTotalCount] = useState(0);

    useEffect(() => {
        setMenuName(["menuBtn", "menuList-notactive"])
    }, [])

    useEffect(() => {
        getUserList();
    }, [page]);

    const getUserList = () => {
        axios.get(`https://localhost:44346/Vehicle?PageNumber=${page}&PageSize=${countPerPage}`)
            .then(res => {
                setTotalCount(JSON.parse(res.headers["x-pagination"]).TotalCount)
                setCars(res.data);
            }).catch(err => {
                setCars({});
            });

        fetch(`https://localhost:44346/Vehicle?PageNumber=${page}&PageSize=${countPerPage}`)
            .then(res => {
            });
    }

    return (
        <div className="carpage">
            <DataTable
                title="Employees"
                columns={columns}
                data={cars.returnValue}
                highlightOnHover
                pagination
                paginationServer
                paginationTotalRows={totalCount}
                paginationPerPage={countPerPage}
                paginationComponentOptions={{
                    noRowsPerPage: true
                }}
                onChangePage={page => setPage(page)}
            />
        </div>
    )
}
export default CarPage;