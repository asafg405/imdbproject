import React, { Component } from 'react';
let Img = require('react-image');
var request = require('request-promise');


window['reloadData'] = function() {
  alert('reloading....');
}

class App extends Component {
  state = {
    actors: [],
    img:[],
  };



  async componentDidMount(data) {
    const url = 'https://localhost:44315/api/Celebs';
    console.log(url);
    await fetch(url)
    .then(async res => await res.json())
    .then((data) => {
      this.setState({ actors: data })
      console.log(this.state.actors)
    })
    .catch(console.log)
  }

  reloadData = async function() {
    console.log('reloading data....', this);
    const url='https://localhost:44315/api/Celebs'
    await request.post({url:url}).then((data) => {
      console.log('***** Finished Reset request');
      this.componentDidMount();
    }).catch((data)=>console.log(data));
  }


  DeleteData = async function(data) {
    console.log('deleting data....', data);
    const url='https://localhost:44315/api/Celebs?celebName='+data;
    await request.delete({url:url}).then((data) => {
      console.log('***** Finished delete request');
     this.componentDidMount();
    }).catch((data)=>console.log(data));
  }

  render() {

    return (
       <div className="container">
        <div className="col-xs-12">
        <h1>100 ACTORS</h1>
        <button onClick={this.reloadData.bind(this)} id='refresh'>Refresh</button>
         {this.state.actors.map((actor,i) => (
          <div className="card">
           <div className="card-body" key={i}>
              <button onClick={this.DeleteData.bind(this,actor.name)} id='delete'>delete</button>
              <h5 className="card-title">{actor.name}</h5>
              <h5 className="card-id">{actor.Role}</h5>
              <Img src={actor.URL} />
              <h6 className="card-subtitle mb-2 text-muted">          
              </h6>
            </div>
          </div>
        ))}
        </div>
       </div>
    );
  }
}
export default App;