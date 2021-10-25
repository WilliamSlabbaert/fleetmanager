import React, { useEffect, useState } from 'react'
import { Route } from "react-router-dom";
import CarPage from '../carpage/carpage';
import CarPageDetail from '../carpage/carpagedetail';
import RequestPage from '../requestpage/requestpage';
import RequestPageDetail from '../requestpage/requestpagedetail';

import "./style/generalpage.css";

const GeneralPage = () => {
    const [pageName, setPageName] = useState("Page-notactive");

    useEffect(() => {
        setTimeout(() => {
            setPageName("Page-active");
        }, 50);
    }, []);

    return (
        <div className={pageName}>
            <Route path="/Carpage" exact>
                <CarPage />
            </Route>
            <Route path="/Carpage/:id" exact>
                < CarPageDetail />
            </Route>
            <Route path="/Requestpage" exact>
                <RequestPage />
            </Route>
            <Route path="/Requestpage/:id" exact>
                <RequestPageDetail />
            </Route>
        </div>
    )
}

export default GeneralPage
