import MenuBtn from '../menu/menubtn.jsx';
import "./style/app.css";
import React from 'react';
import { AppProvider } from '../context/appcontext/appcontext.jsx';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import GeneralPage from "../pages/generalpage/generalpage.jsx"

const App = () => {

  return (
    <AppProvider>
      <Router>
        <div className="App">
          <MenuBtn />
          <Switch>
            <Route exact path="/Carpage" component={() => <GeneralPage />} />
            <Route exact path="/Carpage/:id" component={() => <GeneralPage />} />
            <Route exact path="/Fuelcardpage" component={() => <GeneralPage />} />
            <Route exact path="/Requestpage" component={() => <GeneralPage />} />
            <Route exact path="/Requestpage/:id" component={() => <GeneralPage />} />
            <Route exact path="/Userpage" component={() => <GeneralPage />} />
          </Switch>
        </div>
      </Router>
    </AppProvider>
  );
}

export default App;
