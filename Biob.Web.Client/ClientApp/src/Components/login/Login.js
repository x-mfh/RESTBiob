import "./Login.css";

import React, {Component} from 'react';
import axios from 'axios';
import userManager from './UserManager';
import {Link} from 'react-router-dom';


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
  // goToAdmin(event) {
  //   event.preventDefault();
  //   <Route component={Admin} path='./../Admin/Admin' />
  // }

  render() {
    return (
      <ul className="loginBar">
      {/* <li><Link to='/' className='navLink toolbarItem niceHeaderButtonEffect'>Home</Link></li> */}
        <li><Link to='/Admin' className="loginBarButton toolbarItem niceHeaderButtonEffect">Admin</Link></li>
        {/* </li>/<li><Link to='/movies' className='navLink toolbarItem'>Movies</Link></li> */}
        <li className="loginBarButton toolbarItem niceHeaderButtonEffect" onClick={this.Api}>Call Api</li>
        <li className="loginBarButton toolbarItem niceHeaderButtonEffect" onClick={this.onLoginButtonClick}>Login</li>
        <li className="loginBarButton toolbarItem niceHeaderButtonEffect" onClick={this.onLogoutButtonClick}>Logout</li>
        <pre className="Results"></pre>
      </ul>
    );
  }
}

export default Login;