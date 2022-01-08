import logo from './logo.svg';
import './App.css';
import Footer from './footer/footer';
import Header from './header/header';
import { useState, useEffect, useReducer } from 'react';
import Body from './body/body';


const App = (props) => {
  return (
    <>
      <div className="App">
        <Body />
      </div>
    </>
  )
}

export default App;
