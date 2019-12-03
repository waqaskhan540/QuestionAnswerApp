import React, { Component } from "react";
import TextEditor from "../components/textEditor";
import questionService from "../services/questionsService";
import AnswerService from "../services/answerService";
import DraftService from "../services/draftService";
import { withRouter } from "react-router-dom";
import { Grid, Box } from "grommet";
import { Loader } from "semantic-ui-react";
import {connect} from "react-redux";

class WriteAnswerScreen extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isloading: true,
      question: null,
      publishingAnswer: false,
      savingDraft: false
    };
  }

  postAnswer = answer => {
    const { id } = this.props.match.params;
    this.setState({ publishingAnswer: true });
    AnswerService.postAnswer(id, answer)
      .then(response => {
        this.setState({ publishingAnswer: false });
        this.props.history.push(`/question/${id}`);
      })
      .catch(err => console.log(err));
  };

  saveDraft = draft => {
    this.setState({ savingDraft: true });
   
    DraftService.saveDraft(draft)
      .then(response => {
        this.setState({ savingDraft: false });
        console.log(response);
      })
      .catch(err => {
        this.setState({ savingDraft: false });
        console.log(err);
      });
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
          margin="small"
        >
          <Box gridArea="left" />
          <Box gridArea="middle">
            {isloading ? (
              <Loader active inline="centered" />
            ) : (
              <TextEditor
                onSaveDraft={this.saveDraft}
                onPostAnswer={this.postAnswer}
                publishingAnswer={this.state.publishingAnswer}
                savingDraft={this.state.savingDraft}
                question={question}
              />
            )}
          </Box>
          <Box gridArea="right" />
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default connect(mapStateToProps)(WriteAnswerScreen);
