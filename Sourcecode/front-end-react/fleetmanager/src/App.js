import logo from './logo.svg';
import './App.css';
import Footer from './footer/footer';
import Header from './header/header';

const App = () => {
  return (
    <div className="App">
      <Header headerName = "testHeader"/>
      <h1>body</h1>
      <Footer footerName = "testFooter"/>
    </div>
  );
}

export default App;
