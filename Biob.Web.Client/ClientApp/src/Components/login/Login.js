import "./Login.css";
import React, {Component} from 'react';
import axios from 'axios';
import userManager from './UserManager';


class Login extends Component
{
  Api()
  {
    userManager.getUser().then(user => {
      let url = 'https://localhost:44393/identity';
      axios.get(url, { headers: { 'Authorization': 'Bearer ' + user.access_token } })
      .then(data => {
        console.log(data.status, JSON.parse(data.data));
      });
    });
  }
  onLogoutButtonClick(event) {
    event.preventDefault();
    userManager.signoutRedirect();
    userManager.removeUser();
  }
  onLoginButtonClick(event) {
    event.preventDefault();
    userManager.signinRedirect();
  }
  render() {
    return (
      <div className="loginBar">
        <button className="loginBarButton" onClick={this.onLoginButtonClick}>Login</button>
        <button className="loginBarButton" onClick={this.Api}>Call Api</button>
        <button className="loginBarButton" onClick={this.onLogoutButtonClick}>Logout</button>
        <pre className="Results"></pre>
      </div>
    );
  }
}

export default Login;