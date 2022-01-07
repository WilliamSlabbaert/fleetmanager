import logo from './logo.svg';
import './App.css';
import Footer from './footer/footer';
import Header from './header/header';
import { useState, useEffect, useReducer } from 'react';
import Body from './body/body';


const App = (props) => {
  const [data, setData] = useState([]);
  useState(() => {
    fetch(`https://localhost:44346/Vehicle?PageNumber=1&PageSize=10`)
      .then(response => response.json())
      .then(setData);
  }, [])




  return (
    <>
      <div className="App">
        <Body />
      </div>
    </>
  )
}

export default App;
