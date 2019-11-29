import React, { Component } from "react";
import QuestionList from "../components/questionList";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import questionService from "../services/questionsService";
import { Loader } from "semantic-ui-react";
import { Box, Grid } from "grommet";
import ScreenContainer from "../components/common/screenContainer";

class MyQuestionsScreen extends Component {
  constructor(props) {
    super(props);
    this.state = {
      questions: [],
      loading: true
    };
  }

  componentDidMount() {
    const { user } = this.props;
    questionService.getMyQuestions(user.userId).then(response => {
      const questions = response.data.data;
      this.setState({ questions: questions, loading: false });
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
    const { loading, questions } = this.state;
    return (
      
      <ScreenContainer
        middle={
          loading ? (
            <Loader active></Loader>
          ) : (
            <QuestionList questions={questions} />
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
export default withRouter(connect(mapStateToProps)(MyQuestionsScreen));
