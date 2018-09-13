import React from 'react';

function List(props) {
	let isCurrent = props.currentIndex === props.index;

	return (
		<ul className='holder'>
			<li className='question'
				onClick={() => props.handleChange(props.index)}>
				{props.question}
			</li>
			<li className={isCurrent ? 'answer open' : 'answer'}>
				{isCurrent && <p>{props.answer}</p>}
			</li>
		</ul>
	);
}

export default List;