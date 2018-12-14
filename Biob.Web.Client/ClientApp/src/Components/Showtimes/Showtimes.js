import './Showtimes.css';
import React, { Component } from 'react';
import axios from 'axios';

class Showtime extends Component {
  constructor(props) {
    super(props)
    this.state = {
      hall: []
    }
  }


  componentWillMount() {
    const hallForShowtimeApiCall = 'https://localhost:44390/api/v1/halls/'+this.props.showtime.hallId;
    axios.get(hallForShowtimeApiCall)
    .then(res => {
      this.setState({hall: res.data})
    })
  }
  
  render() {
    return (
      // decide how to wrap showtime, keep it SEPERATED FROM MOVIE
      <div 
        onClick={() => alert('Redirecting to order tickets for movie in hall '+this.state.hall.hallNo+'...')}
        className="showtime"
      >
        <p>Hall: {this.state.hall.hallNo}</p>
        <p>{this.props.showtime.timeOfPlaying}</p>
        <p>{this.props.showtime.threeDee = true ? '3D' : ''}</p>
      </div>
    )
  }
}

class Movie extends Component {
  constructor(props) {
    super(props)
    this.state = {
      showtimes: [],
    };
  }
  
  componentWillMount() {
    const showtimesForMovieApiCall = 'https://localhost:44390/api/v1/movies/'+this.props.movie.id+'/showtimes';
    axios.get(showtimesForMovieApiCall)
    .then(res => {
      this.setState({showtimes: res.data})
    })
  }
        
  render() {
    return (
      <div className="content">
			  <div className="movie">
          {/* <img src={this.props.movie.poster} alt="" ></img> */}
            <h1>{this.props.movie.title}</h1>
          {this.state.showtimes.map(showtime => (
              <Showtime
                key={showtime.id}
                showtime={showtime}
              />
          ))}
        </div>
      </div>
    )
  }
}

class Body extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      movies: [],
    };
  }

  componentWillMount() {
    const moviesApiCall = 'https://localhost:44390/api/v1/movies';
    axios.get(moviesApiCall)
    .then(res => {
      this.setState({movies: res.data})
    })
  }
  
  render() {
    return (
      <main className="flex-main">
        {this.state.movies.map(movie => (
              <Movie
                key={movie.id}
                movie={movie}
              />
          ))}
      </main>
    )
  }
}

export default Body;