import React,{useEffect,useContext,useState} from "react";
import { AppContext } from "../context/appcontext/appcontext";
import "./style/carpage.css"


const CarPage = () =>{
    const { setMenuName } = useContext(AppContext)
    useEffect(() => {
        setMenuName(["menuBtn", "menuList-notactive"])
    }, []) 

    const [pageName,setPageName] = useState("carPage-notactive");
    useEffect(() => {
        setTimeout(()=>{
            setPageName("carPage-active");
        },50)
    }, [])
    return (
        <div className={pageName}>
            
        </div>
    )
}

export default CarPage;