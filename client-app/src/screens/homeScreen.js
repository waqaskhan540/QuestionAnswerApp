import React, { Component } from "react";
import { connect } from "react-redux";
import QuestionList from "../components/questionList";
import { Segment, Dimmer, Loader, Image } from "semantic-ui-react";
import { Box, Grid } from "grommet";
import questionService from "../services/questionsService";
import ScreenContainer from "../components/common/screenContainer";


class HomeScreen extends Component {
  constructor(props) {
    super(props);
    this.state = {
      questions: [],
      loading: true
    };
  }

  componentDidMount() {
    questionService.getLatestQuestions().then(response => {
      const questions = response.data.data;
      this.setState({ questions: questions, loading: false });
    });
  }

  render() {
    const { loading, questions } = this.state;

    return (     
      <ScreenContainer
        middle={
          loading ? (
            <Loader active></Loader>
          ) : (
            <QuestionList
              questions={questions}
              isUserAuthenticated={this.props.user.isAuthenticated}
            />
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
export default connect(mapStateToProps)(HomeScreen);
