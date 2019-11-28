import React, { Component } from "react";
import TextEditor from "../components/textEditor";
import questionService from "../services/questionsService";
import AnswerService from "../services/answerService";
import { withRouter } from "react-router-dom";
import {Grid,Box} from "grommet";
import {Loader} from "semantic-ui-react";

class WriteAnswerScreen extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isloading: true,
      question: null
    };
  }

  postAnswer = answer => {
    const { id } = this.props.match.params;
    AnswerService.postAnswer(id, answer)
      .then(response => this.props.history.push(`/question/${id}`))
      .catch(err => console.log(err));
  };

  componentDidMount() {
    const { id } = this.props.match.params;
    questionService.getQuestionById(id).then(response => {
      const question = response.data.data;
      this.setState({ isloading: false, question: question });
    });
  }
  render() {
    const { isloading, question } = this.state;

    return (
      // <Grid container columns={3} padded>
      //   <Grid.Column width={5}></Grid.Column>
      //   <Grid.Column width={8}>
      <div>
        <Grid
          rows={["xlarge"]}
          columns={["small", "large", "small"]}
          gap="small"
          areas={[
            { name: "left", start: [0, 0], end: [0, 0] },
            { name: "middle", start: [1, 0], end: [1, 0] },
            { name: "right", start: [2, 0], end: [2, 0] }
          ]}
          margin= "small"
        >
          <Box gridArea="left" />
          <Box gridArea="middle">
            {isloading ? (
              <Loader active inline='centered' />
            ) : (
              <TextEditor onPostAnswer={this.postAnswer} question={question} />
            )}
          </Box>
          <Box gridArea="right" />
        </Grid>
      </div>
    );
  }
}

export default withRouter(WriteAnswerScreen);
