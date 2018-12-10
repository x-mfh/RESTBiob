import React, { Component } from 'react';
import axios from 'axios';

var url = 'https://localhost:44390/api/v1/movies/';

class LayOutTest extends React.Component {
  state = {
    movies: [],
  };

  componentDidMount() {
    axios.get(url)
    .then(res => {
      this.setState({movies: res.data})
    })
  }

  render() {
    return (
      <u>
        {this.state.movies.map(movies => <div>{movies.id}</div>)}
      </u>
    );
  }
}

export default LayOutTest;