import MenuBtn from '../menu/menubtn.jsx';
import "./style/app.css";
import React,{useState} from 'react';
import { AppProvider } from './appcontext.jsx';

const App = () => {

  return (
    <AppProvider>
      <div className="App">
        <MenuBtn />
      </div>
    </AppProvider>

  );
}

export default App;
