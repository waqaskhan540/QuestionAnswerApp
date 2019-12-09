import React, { Component } from "react";
import QuestionList from "../components/questionList";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import questionService from "../services/questionsService";
import { Loader } from "semantic-ui-react";
import ScreenContainer from "../components/common/screenContainer";

import { bindActionCreators } from "redux";
import * as UserActions from "./../actions/userActions";

class MyQuestionsScreen extends Component {
 
  componentDidMount() {
    const { user } = this.props;
    questionService.getMyQuestions(user.userId).then(response => {
      const questions = response.data.data;      
      this.props.actions.userMyQuestionsLoaded(questions);
      this.props.actions.userMyQuestionsLoading(false);
    });
  }
  componentWillMount() {
    const { history, user } = this.props;
    if (!user.isAuthenticated) {
      let returnUrl = "/myquestions";
      history.push(`/login?returnUrl=${returnUrl}`);
    }
  }
  render() {
    const { loadingMyQuestions, myQuestions } = this.props.user;
    return (
      <ScreenContainer
        middle={
          loadingMyQuestions ? (
            <Loader active></Loader>
          ) : (
            <QuestionList questions={myQuestions} />
          )
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
const mapDispatchToProps = dispatch => {
  return {
    actions: bindActionCreators(UserActions, dispatch)
  };
};
export default withRouter(
  connect(mapStateToProps, mapDispatchToProps)(MyQuestionsScreen)
);
