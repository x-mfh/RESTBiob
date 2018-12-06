import React, { Component } from 'react';
class HomePage extends Component {
  render() {
    return (
        <div id="content">
		    <div id="feature">
				<p>Feature</p>
			</div>
			<div class="article column1">
				<p>Column One</p>
			</div>
			<div class="article column2">
				<p>Column Two</p>
			</div>
			<div class="article column3">
				<p>Column Three</p>
			</div>
		</div>
    )
  }
}
export default HomePage;