import React, { Component } from 'react';
import './App.css';
import Home from './Components/Home/Home';
import Movies from './Components/Movies/Movies';
import Showtimes from './Components/Showtimes/Showtimes';
import Header from './Components/Layouts/Header/Header';
import Footer from './Components/Layouts/Footer/Footer';
import Callback from './Components/login/Callback';
import PostMovie from './Components/PostMovie/PostMovie';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';
import { ConnectedRouter } from 'connected-react-router'
import { createBrowserHistory } from 'history'
import { connect } from "react-redux";

const history = createBrowserHistory()


class App extends Component {
  render() {
    console.log(this.props);
    return (
      <Router>
        <div>
          <Header/>
            <OidcProvider store={store} userManager={userManager}>
              <Switch>
               <Route path="/" component ={Home} exact/>
               <Route path="/showtimes" component ={Showtimes} exact/>
               <Route path="/movies" component ={Movies} exact/>
               <Route path="/PostMovie" component ={PostMovie} exact/>
               <Route path="/callback" component={Callback} />
             </Switch>
             </OidcProvider>
          <ConnectedRouter history={history}>
            <Switch>
              <Route path='/' component ={Home} exact/>
              <Route path='/showtimes' component ={Showtimes} exact/>
              <Route path="/callback" component={Callback} />
            </Switch>
            </ConnectedRouter>
          <Footer/>
        </div>
      </Router>
    );
  }
};

export default connect((state) => {
  return {
      location: state.router.location.pathname
  }
})(App);
