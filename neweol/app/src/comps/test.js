import React from 'react';
import ReactDOM from 'react-dom'

class Test extends React.Component {
  render() {
    return <h1>Some {this.props.name}</h1>;
  }
}

export default Test;