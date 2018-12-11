import React from "react";
import { connect } from "react-redux";
import { CallbackComponent } from "redux-oidc";
import createHistory from 'history/createBrowserHistory'
import userManager from "../login/UserManager";

const { push } = createHistory()

class CallbackPage extends React.Component {
  render() {
    // just redirect to '/' in both cases
    return (
      <CallbackComponent
        userManager={userManager}
        successCallback={() => this.props.dispatch(push("/"))}
        errorCallback={error => {
          this.props.dispatch(push("/"));
          console.error(error);
        }}
        >
        <div>Redirecting...</div>
      </CallbackComponent>
    );
  }
}

export default connect()(CallbackPage);