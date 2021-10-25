import React, { useEffect, useContext, useState } from "react";
import { AppContext } from "../context/appcontext/appcontext";
import axios from "axios";
import DataTable from "react-data-table-component";
import { columnsCarpage } from './utility/datatable_attributes';

import "./style/carpage.css"



const CarPage = () => {
    const { setMenuName } = useContext(AppContext);
    const [cars, setCars] = useState([]);
    const [page, setPage] = useState(1);
    const countPerPage = 10;
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
    }

    return (
        <div className="carpage">
            <DataTable
                title="Cars"
                columns={columnsCarpage}
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