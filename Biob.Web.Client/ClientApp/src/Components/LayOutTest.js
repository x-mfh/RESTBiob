import React, { Component } from 'react';
import axios from 'axios';

var url = 'https://localhost:44390/api/v1/movies/163c03b3-a057-426d-afa3-1a2631a693e2';

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
        {this.state.movies.map(movies => <div>movies</div>)}
      </u>
    );
  }
}

export default LayOutTest;