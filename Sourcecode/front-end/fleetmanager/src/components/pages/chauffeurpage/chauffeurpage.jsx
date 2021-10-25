import React, { useEffect, useContext, useState } from "react";
import {AppContext} from "components/context/appcontext/appcontext.jsx"
import axios from "axios";
import DataTable from "react-data-table-component";
import { columnsChaffeurpage } from "./utility/datatable_chauffeurpage";




const ChauffeurPage = () => {
    const { setMenuName } = useContext(AppContext);
    const [chauffeurs, setChauffeurs] = useState([]);
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
        axios.get(`https://localhost:44346/Chaffeur?PageNumber=${page}&PageSize=${countPerPage}`)
            .then(res => {
                setTotalCount(JSON.parse(res.headers["x-pagination"]).TotalCount)
                setChauffeurs(res.data);
            }).catch(err => {
                setChauffeurs({});
            });
    }

    return (
        <div className="chauffeurPage">
            <DataTable
                title="Chauffeurs"
                columns={columnsChaffeurpage}
                data={chauffeurs.returnValue}
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
export default ChauffeurPage;