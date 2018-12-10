import React, { Component } from 'react';
import './App.css';
import Home from './Components/Pages/Showtimes/Home';
import Showtimes from './Components/Pages/Showtimes/Showtimes';
import Header from './Components/Layout/Header/Header';
import Footer from './Components/Layout/Footer/Footer';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';


class App extends Component {
  render() {
    return (
      <Router>
        <div>
          <Header/>
            <Switch>
              <Route path='/' component ={Home} exact/>
              <Route path='/showtimes' component ={Showtimes} exact/>
            </Switch>
          <Footer/>
        </div>
      </Router>
    );
  }
};

export default App;
