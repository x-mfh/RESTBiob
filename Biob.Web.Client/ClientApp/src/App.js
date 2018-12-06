import React, { Component } from 'react';
import './App.css';
import LayOutTest from './Components/LayOutTest';
import HomePage from './Components/HomePage';
import Header from './Components/Header';
import Footer from './Components/Footer';

class App extends Component {
  render() {
    return (
      <div>
      <Header/>
        <HomePage>
          <LayOutTest/>
        </HomePage>
      <Footer/>
      </div>
    );
  }
};

export default App;
