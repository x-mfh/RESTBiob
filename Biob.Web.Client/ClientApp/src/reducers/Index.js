import { combineReducers } from 'redux';
import { reducer as oidcReducer } from 'redux-oidc';
import subscriptionsReducer from './Subscriptions';

const reducer = combineReducers(
  {
    oidc: oidcReducer,
    subscriptions: subscriptionsReducer
  }
);

export default reducer;