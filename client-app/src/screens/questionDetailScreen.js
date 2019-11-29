import React, { Component } from "react";
import QuestionDetail from "../components/questionDetail";
import AnswerService from "../services/answerService";
import QuestionService from "../services/questionsService";
import { connect } from "react-redux";
import ScreenContainer from "../components/common/screenContainer";

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
     
      <ScreenContainer
        middle={
          <QuestionDetail
            isLoading={isloading}
            answers={answers}
            question={question}
            isUserAuthenticated={isAuthenticated}
          />
        }
      />
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default connect(mapStateToProps)(QuestionDetailScreen);
