import React, { Component } from 'react';
import './App.css';
import Home from './Components/Home';
import Showtimes from './Components/Showtimes';
import Header from './Components/layout/Header';
import Footer from './Components/layout/Footer';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';


class App extends Component {
  render() {
    return (
      <Router>
        <div>
          <Header/>
          <div className={'topNavContainer'}>
            <Switch>
              <Route path='/' component ={Home} exact/>
              <Route path='/showtimes' component ={Showtimes} exact/>
            </Switch>
          </div>
          <Footer/>
        </div>
      </Router>
    );
  }
};

export default App;
