import React, { Component } from "react";
import QuestionDetail from "../components/questionDetail";
import AnswerService from "../services/answerService";
import QuestionService from "../services/questionsService";
import { connect } from "react-redux";
import ScreenContainer from "../components/common/screenContainer";
import { bindActionCreators } from "redux";
import * as QuestionDetailActions from "./../actions/questionDetailActions";

class QuestionDetailScreen extends Component {
  componentDidMount() {
    window.scrollTo(0, 0);
    const { id } = this.props.match.params;
    this.props.actions.isLoading(true);
    QuestionService.getQuestionById(id).then(response => {
      this.props.actions.questionLoaded(response.data.data);
      AnswerService.getAnswersByQuestionId(id).then(response => {
        this.props.actions.answersLoaded(response.data.data);
        this.props.actions.isLoading(false);
      });
    });
  }

  render() {
    const { isloading, answers, question } = this.props.questionDetail;
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
    user: state.user,
    questionDetail: state.questionDetail
  };
};

const mapDispatchToProps = dispatch => {
  return {
    actions: bindActionCreators(QuestionDetailActions, dispatch)
  };
};
export default connect(
  mapStateToProps,
  mapDispatchToProps
)(QuestionDetailScreen);
