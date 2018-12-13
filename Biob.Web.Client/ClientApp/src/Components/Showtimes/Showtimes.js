import './Showtimes.css';
import React, { Component } from 'react';
import axios from 'axios';

// function Showtime(props) {
//     return (
//       // decide how to wrap showtime, keep it SEPERATED FROM MOVIE
//       <div onClick={() => alert('Redirecting to order tickets...')}>
//         <h1>{props.showtime.hallId}</h1>
//         <h1>{props.showtime.timeOfPlaying}</h1>
//         <h1>{props.showtime.threeDee = true ? '3D' : ''}</h1>
//       </div>
//     )
// }

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
      <div onClick={() => alert('Redirecting to order tickets...')}>
        <h1>Hall: {this.state.hall.hallNo}</h1>
        <h1>{this.props.showtime.timeOfPlaying}</h1>
        <h1>{this.props.showtime.threeDee = true ? '3D' : ''}</h1>
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

    // Get hallNo with axios call and pass on to showtime?
  }
        
  render() {
    return (
			<div className="wrapper">
        <div className="pictureContent">
          <img src={this.props.movie.poster} alt="" ></img>
          <div className="textContent">
            <h1>{this.props.movie.title}</h1>
          </div>
          {this.state.showtimes.map(showtime => (
            //<div key={showtime.id}>
              <Showtime
                key={showtime.id}
                showtime={showtime}
              />
            //</div> 
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
			<div className="wrapper">
        {this.state.movies.map(movie => (
              <Movie
                key={movie.id}
                movie={movie}
              />
          ))}
		  </div>
    )
  }
}

export default Body;