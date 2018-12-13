import "./MenuDropdown.css";
import React, { Component } from 'react';
import {Link} from 'react-router-dom';

    class MenuDropdown extends Component {
        
        // handleClick = () => this.props.onClick(this.props.index)
        
        render() {  
            return(
                <div className="menuDropdown">
                {/* This is unfortunately not a dropdown, or is it? */}
                <ul>
						<li><Link to='/' className='dropdownItem niceHeaderButtonEffect'>Home</Link></li>
						<li><Link to='/showtimes' className='dropdownItem niceHeaderButtonEffect'>Showtimes</Link></li>
						<li><Link to='/movies' className='dropdownItem niceHeaderButtonEffect'>Movies</Link></li>
                </ul>
                </div>
            )
            
            // return <button
            //     type='button'
            //     className={
            //     this.props.isActive ? 'active' : 'album'
            //     }
            //     onClick={ this.handleClick }
            // >
            //     <span>{ this.props.name }</span>
            // </button>
        }
    }

export default MenuDropdown;