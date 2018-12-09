import './Home.css';
import React, { Component } from 'react';
import axios from 'axios';

var url = 'https://localhost:44390/api/v1/movies';

class Movies extends Component {
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
			<div class="wrapper">
      {this.state.movies.map(movies =>
        <div class="pictureContent">
        <img src={movies.poster}></img>
          <h1>{movies.title}</h1>
          <p>{movies.description}</p>
        </div>)}
			</div>
    )
  }

}
export default Movies;