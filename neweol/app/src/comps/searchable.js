import React from 'react';
import ReactDOM from 'react-dom';
import CustomerService from '../services/customer.js'

class Searchable extends React.Component {

    constructor(props) {
        super(props);
        this.state = { keyword: '', debounceTimer: {}, items:[] }
        this.handldeChange = this.handldeChange.bind(this);
        this.search = this.search.bind(this);
    }

    componentWillMount() {
        
    }

    render() {
        const resultList = [];
        this.state.items.forEach((elem, index)=>{
            //console.log(elem);
            resultList.push( <div key={index}>{elem._source.ID}</div>);
        });

        return (<div>
                    <input value={this.state.keyword} type="text" onChange={this.handldeChange} />
                    <div>{resultList}</div>
                </div>);
    }

    handldeChange(e) {
        e.persist();
        clearTimeout(this.state.debounceTimer);
        let currentDebounce = setTimeout(this.search, 500, e);
        this.setState({
            keyword: e.target.value,
            debounceTimer: currentDebounce
        });
    }

    search(e) {
        let service = new CustomerService();
        service.search(e.target.value, (data) => {
            this.setState({items:data.hits.hits});
            console.log(data.hits.hits[0]._source.ID);
        });
    }
}

export default Searchable;