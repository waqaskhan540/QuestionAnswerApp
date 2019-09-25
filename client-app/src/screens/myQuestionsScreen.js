import React, { Component } from "react";
import QuestionList from "../components/questionList";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import questionService from "../services/questionsService";

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
      // <Grid container columns={3} padded>
      //   <Grid.Column width={5}></Grid.Column>
      //   <Grid.Column width={8}>
      <div>
        {loading ? (
          <div>Loading...</div>
        ) : (
          <QuestionList questions={questions} />
        )}
      </div>
      //   </Grid.Column>
      //   <Grid.Column width={3}></Grid.Column>
      // </Grid>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default withRouter(connect(mapStateToProps)(MyQuestionsScreen));
