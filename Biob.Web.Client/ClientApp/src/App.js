import React, { Component } from 'react';
import './App.css';
import Home from './Components/Home/Home';
import Showtimes from './Components/Showtimes/Showtimes';
import Header from './Components/Layouts/Header/Header';
import Footer from './Components/Layouts/Footer/Footer';
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
