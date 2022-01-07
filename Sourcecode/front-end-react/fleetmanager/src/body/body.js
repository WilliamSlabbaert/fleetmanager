import React,{useState,useEffect} from "react";
import { Button } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import "./body.css"
import Nav from "../nav/nav";

const Body = () => {
    const [userData , setUserData] = useState([])

    useEffect(()=>{
        fetch(`https://localhost:44346/Chauffeur/1`)
        .then((res)=> res.json())
        .then(setUserData);
        console.log(userData)
    },[])
    return (
        <div className="container-sm cardPlate">
            <Nav/>

        </div>
    );
}

export default Body;