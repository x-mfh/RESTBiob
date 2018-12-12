import React, { Component } from 'react';
import { Provider } from 'react-redux';
import { OidcProvider } from 'redux-oidc';
import store from './Stores/ConfigureStore';
import userManager from "./Components/login/UserManager";
import './App.css';
import Home from './Components/Home/Home';
import Movies from './Components/Movies/Movies';
import Showtimes from './Components/Showtimes/Showtimes';
import Header from './Components/Layouts/Header/Header';
import Footer from './Components/Layouts/Footer/Footer';
import Callback from './Components/login/Callback';
import PostMovie from './Components/PostMovie/PostMovie';
import { BrowserRouter as Router, Route, Switch} from 'react-router-dom';


class App extends Component {
  render() {
    return (
      <Provider store={store} >
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
          <Footer/>
        </div>
      </Router>
      </Provider>
    );
  }
};

export default App;
