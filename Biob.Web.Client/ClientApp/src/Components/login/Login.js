import React, {Component} from 'react';
import axios from 'axios';
import userManager from './UserManager';


class Login extends Component
{
  GetUser() {
    this.state.mgr.getUser().then(user => {
      if (user) {
        console.log('User logged in', user.profile);
      }
      else
      {
        console.log('User not logged in');
      }
    });
  }

  Api()
  {
    userManager.getUser().then(user => {
      let url = 'http://localhost:5001/identity';
      axios.get(url, { headers: { 'Authorization': 'Bearer ' + user.access_token } })
      .then(data => {
        console.log(data.status, JSON.parse(data.data));
      });
    });
  }
  Logout = (event) =>
  {
    event.preventDefault();
    userManager.signoutRedirect();
    userManager.removeUser();
  }
  login() {

  }
  render() {
    return (
      <div>
        <button onClick={userManager.signinRedirect}>Login</button>
        <button onClick={this.Api}>Call Api</button>
        <button onClick={(event) => this.Logout(event)}>Logout</button>
        <pre className="Results"></pre>
      </div>
    );
  }
}

export default Login;