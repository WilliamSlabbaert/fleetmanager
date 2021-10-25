import React, { useEffect, useContext, useState } from "react";
import {AppContext} from "components/context/appcontext/appcontext.jsx"
import axios from "axios";
import DataTable from "react-data-table-component";
import { columnsRequestpage } from "./utility/datatables_requestpage";


const RequestPage = () => {
    const { setMenuName } = useContext(AppContext);
    const [requests, setRequests] = useState([]);
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
        axios.get(`https://localhost:44346/Request?PageNumber=${page}&PageSize=${countPerPage}`)
            .then(res => {
                setTotalCount(JSON.parse(res.headers["x-pagination"]).TotalCount)
                setRequests(res.data);
            }).catch(err => {
                setRequests({});
            });
    }

    return (
        <div className="carpage">
            <DataTable
                title="Requests"
                columns={columnsRequestpage}
                data={requests.returnValue}
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
export default RequestPage;