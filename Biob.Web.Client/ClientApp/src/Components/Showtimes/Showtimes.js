import './Showtimes.css';
import React, { Component } from 'react';
import axios from 'axios';

var url = 'https://localhost:44390/api/v1/movies';

// LOOPER UENDELIG KALD, IKKE KØR

function Showtime(props) {
    return (
      <div key={props.showtime.id}>
        <h1>{props.showtime.hallId}</h1>
        <h1>{props.showtime.timeOfPlaying}</h1>
        <h1>{props.showtime.threeDee}</h1>
      </div>

      // <div>
      //   {props.showtimes.map(showtime =>
      //   (<div key={showtime.id}>
      //     <h1>{showtime.hallId}</h1>
      //     <h1>{showtime.timeOfPlaying}</h1>
      //     <h1>{showtime.threeDee}</h1>
      //   </div>))}
      // </div>
    )
}

class Movie extends Component {
  constructor(props) {
    super(props)
    this.state = {
      showtimes: [],
    };
  }
  
  componentWillMount() {
    const url2 = 'https://localhost:44390/api/v1/movies/'+this.props.movie.id+'/showtimes';
    axios.get(url2)
    .then(res => {
      this.setState({showtimes: res.data})
    })
  }
  

  // renderShowtime() {
  //   return (
  //     <Showtime
  //       showtimes={this.state.showtimes}
  //     />
  //   );
  // }

  // foreach movie, fill with showtime
  render() {
    return (
			<div className="wrapper">
        <div key={this.props.movie.id}>
              <div className="pictureContent">
                <img src={this.props.movie.poster} alt="" ></img>
                <div className="textContent">
                  <h1>{this.props.movie.title}</h1>
                </div>
                {this.state.showtimes.map(showtime => (
                  <div key={showtime.id}>
                    <Showtime
                      showtime={showtime}
                    />
                  </div> 
                ))}
              </div>
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
    axios.get(url)
    .then(res => {
      this.setState({movies: res.data})
    })
  }
  
  
  render() {
    //const AllMovies = this.state.movies;
    return (
			<div className="wrapper">
        {this.state.movies.map(movie => (
          <div key={movie.id}>
              <Movie
                movie={movie}
              />
          </div>
          )
        )}
		  </div>
    )
  }
}

export default Body;

/*
function getShowtimes(movieId) {
  const showtimeUrl = 'https://localhost:44390/api/v1/movies/'+movieId+'/showtimes';
    axios.get(showtimeUrl)
    .then(res => {
      this.setState({showtimes: res.data})
    })
    return showtimes;
}
*/