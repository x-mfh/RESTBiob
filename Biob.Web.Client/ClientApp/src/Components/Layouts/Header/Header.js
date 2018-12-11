import './Header.css';
import React, { Component } from 'react';
import {Link} from 'react-router-dom';
import Login from '../../login/Login';

class NavBar extends Component {
  render() {
    return (
    <header>
			<div className="topNavContainer">
				<nav>
          <div className="toggle">
            <i className ="fa fa-bars" aria-hidden="true"></i>
          </div>
					<ul>
						<li><Link to='/' className='navLink'>Home</Link></li>
						<li><Link to='/showtimes' className='navLink'>Showtimes</Link></li>
						<li><Link to='/movies' className='navLink'>Movies</Link></li>
					</ul>
          <Login />
				</nav>
			</div>
		</header>
    )
  }
}
export default NavBar;