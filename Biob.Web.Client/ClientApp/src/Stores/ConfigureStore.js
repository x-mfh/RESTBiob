import { applyMiddleware, createStore } from 'redux';
import { loadUser } from "redux-oidc";
import { createLogger } from 'redux-logger';
import reducer from "../reducers/Index";
import userManager from "../Components/login/UserManager";

//const defaultState = {};

const logger = createLogger({
    collapsed: true
});


var store = createStore(reducer, applyMiddleware(logger));
loadUser(store, userManager);

export default store;