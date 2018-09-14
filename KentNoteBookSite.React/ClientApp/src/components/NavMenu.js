import React, { Component } from 'react';
import './NavMenu.css';

export class NavMenu extends Component {
	displayName = '登录';

	render() {
		return (
			<nav className='navbar navbar-expand navbar-bg fixed-top'>
				<a className='navbar-brand text-white'>
					<img src='../../assets/app-icon.png' className="rounded mr-2" width="41" height="32" alt="..." />
					法兰密封结构泄漏评价与风险控制系统
				</a>
			</nav>
		);
	}
}
