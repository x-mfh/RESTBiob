import React, {Component} from 'react';
import axios from 'axios';
import Oidc from 'oidc-client';


class Login extends Component
{

  constructor(props)
  {
    super(props)
    this.state = {
      config: {
        authority: "https://localhost:44393/",
        client_id: "js",
        redirect_uri: "https://localhost:44393/callback.html",
        response_type: "id_token token",
        scope:"openid profile api1",
        post_logout_redirect_uri : "https://localhost:44393/index.html",
      },
      mgr: new Oidc.UserManager(this.state.config)
    }
  }

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
    this.state.mgr.getUser().then(user => {
      let url = 'http://localhost:5001/identity';
      axios.get(url, { headers: { 'Authorization': 'Bearer ' + user.access_token } })
      .then(data => {
        console.log(data.status, JSON.parse(data.data));
      });
    });
  }
  Logout()
  {
    this.state.mgr.signoutRedirect();
  }
  render() {
    return (
      <div>
        <button onClick={this.state.mgr.signinRedirect}>Login</button>
        <button onClick={this.Api}>Call Api</button>
        <button onClick={this.Logout}>Logout</button>
        <pre className="Results"></pre>
      </div>
    );
  }
}

export default Login;