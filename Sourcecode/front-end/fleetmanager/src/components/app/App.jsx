import MenuBtn from '../menu/menubtn.jsx';
import "./style/app.css";
import React from 'react';
import { AppProvider } from '../context/appcontext/appcontext.jsx';
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import CarPage from '../carpage/carpage.jsx';
import GeneralPage from '../generalpage/generalpage.jsx';

const App = () => {

  return (
    <AppProvider>
      <Router>
        <div className="App">
          <MenuBtn />
          <Switch>
            <Route path="/Carpage" exact>
              <GeneralPage />
            </Route>
          </Switch>
        </div>
      </Router>
    </AppProvider>
  );
}

export default App;
