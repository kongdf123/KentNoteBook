import React from 'react';
import ReactDOM from 'react-dom';
import App from './app';

function Helloworld() {
	return <h1>Hello world</h1>;
}

ReactDOM.render(<App />, document.getElementById('root'));