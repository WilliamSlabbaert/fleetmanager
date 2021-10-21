import React, { useContext, useEffect, useState } from 'react'
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import CarPage from '../carpage/carpage';

import "./style/generalpage.css";

const GeneralPage = () => {
    const [pageName, setPageName] = useState("Page-notactive");

    useEffect(() => {
        setTimeout(() => {
            setPageName("Page-active");
        }, 50)
    }, []);

    return (
        <div className={pageName}>
            <Route path="/Carpage" exact>
                <CarPage />
            </Route>
        </div>
    )
}

export default GeneralPage
