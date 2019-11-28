import React, { Component } from "react";
import QuestionList from "../components/questionList";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import questionService from "../services/questionsService";
import { Loader } from "semantic-ui-react";
import {Box,Grid} from "grommet";

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
      <Grid
        rows={["xlarge"]}
        columns={["small", "large", "small"]}
        gap="small"
        areas={[
          { name: "left", start: [0, 0], end: [0, 0] },
          { name: "middle", start: [1, 0], end: [1, 0] },
          { name: "right", start: [2, 0], end: [2, 0] }
        ]}
      >
        <Box gridArea="left" />
        <Box gridArea="middle">
          <div>
            {loading ? (
              <Loader active></Loader>
            ) : (
              <QuestionList questions={questions} />
            )}
          </div>
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
export default withRouter(connect(mapStateToProps)(MyQuestionsScreen));
