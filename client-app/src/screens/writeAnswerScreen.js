import React, { Component } from "react";
import WriteAnswer from "../components/writeAnswer";
import {Grid} from 'semantic-ui-react';
import TextEditor from "../components/textEditor";
import questionService from "../services/questionsService";

class WriteAnswerScreen extends Component {

  constructor(props) {
      super(props)
      this.state = {
          isloading:true,
          question:null
      }      
  }

  componentDidMount() {
      const {id} = this.props.match.params;
      questionService.getQuestionById(id)
        .then(response => {
            const question = response.data.data;
            this.setState({isloading:false,question:question})
        })
  }
  render() {
    const { isloading,question } = this.state;
    return (
      <Grid container columns={3} padded>
        <Grid.Column width={5}></Grid.Column>
        <Grid.Column width={8}>
           {isloading ? "Loading...": <TextEditor question={question}/>}
        </Grid.Column>
        <Grid.Column width={3}></Grid.Column>
      </Grid>
    );
  }
}

export default WriteAnswerScreen;