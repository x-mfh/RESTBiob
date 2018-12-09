import './Header.css';
import React, { Component } from 'react';
import {Link} from 'react-router-dom';

class NavBar extends Component {
  render() {
    return (
    <header>
			<div className="topNavContainer">
				<nav>
          <div class="toggle">
            <i class ="fa fa-bars" aria-hidden="true"></i>
          </div>
					<ul>
						<li><Link to='/' className='navLink'><a>Home</a></Link></li>
						<li><Link to='/showtimes' className='navLink'><a>Showtimes</a></Link></li>
						<li><Link to='/' className='navLink'><a>Movies</a></Link></li>
					</ul>
				</nav>
			</div>
		</header>
    )
  }
}
export default NavBar;