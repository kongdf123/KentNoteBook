import React from 'react';
import ReactDOM from 'react-dom';
import Accordion from './app';

function Helloworld() {
	return <h1>Hello world</h1>;
}

ReactDOM.render(<Accordion />, document.getElementById('root'));