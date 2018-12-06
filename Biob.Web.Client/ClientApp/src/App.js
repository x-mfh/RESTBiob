import React, { Component } from 'react';
import './App.css';
import LayOutTest from './Components/LayOutTest'

const API = 'https://hn.algolia.com/api/v1/search?query=';
const DEFAULT_QUERY = 'redux';

class App extends Component {
  render() {
    return (
      <LayOutTest/>
    );
  }
};

export default App;
