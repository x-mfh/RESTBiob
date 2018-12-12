import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { Provider } from 'react-redux';
import * as serviceWorker from './serviceWorker';
import store from './Stores/ConfigureStore';
import { OidcProvider } from 'redux-oidc';
import userManager from "./Components/login/UserManager";

ReactDOM.render(<Provider store={store} ><OidcProvider store={store} userManager= {userManager}><App /></OidcProvider></Provider>, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister();
