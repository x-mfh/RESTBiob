import React, { Component } from 'react';
class NavBar extends Component {
  render() {
    return (
    <div id="header">
			<h1>Logo</h1>
			<div id="nav">
				<ul>
					<li><a href="">Home</a></li>
					<li><a href="">Showtimes</a></li>
					<li><a href="">Movies</a></li>
					<li><a href="">About</a></li>
					<li><a href="">Contact</a></li>
				</ul>
			</div>
		</div>
    )
  }
}
export default NavBar;