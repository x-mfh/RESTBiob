import React, { Component } from 'react';
import './App.css';
import Home from './components/Home';
import Showtimes from './components/Showtimes';
import Header from './components/layout/Header';
import Footer from './components/layout/Footer';
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
