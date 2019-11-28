import React, { Component } from "react";
import QuestionDetail from "../components/questionDetail";
import AnswerService from "../services/answerService";
import QuestionService from "../services/questionsService";
import { connect } from "react-redux";
import {Grid,Box} from "grommet";

class QuestionDetailScreen extends Component {
  state = {
    isloading: true,
    question: null,
    answers: []
  };
  componentDidMount() {
    const { id } = this.props.match.params;

    QuestionService.getQuestionById(id).then(response => {
      this.setState({ question: response.data.data });

      AnswerService.getAnswersByQuestionId(id).then(response => {
        this.setState({ isloading: false });
        this.setState({ answers: response.data.data });
      });
    });
  }

  render() {
    const { isloading, answers, question } = this.state;
    const { isAuthenticated } = this.props.user;

    return (
      <Grid
        rows={["xlarge"]}
        columns={["small", "large", "small"]}
        gap="small"
        margin = "small"
        areas={[
          { name: "left", start: [0, 0], end: [0, 0] },
          { name: "middle", start: [1, 0], end: [1, 0] },
          { name: "right", start: [2, 0], end: [2, 0] }
        ]}
      >
        <Box gridArea="left" />
        <Box gridArea="middle">
          <QuestionDetail
            isLoading={isloading}
            answers={answers}
            question={question}
            isUserAuthenticated={isAuthenticated}
          />
        </Box>
        <Box gridArea="right" />
      </Grid>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default connect(mapStateToProps)(QuestionDetailScreen);
