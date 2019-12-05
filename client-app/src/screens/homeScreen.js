import React, { Component } from "react";
import { connect } from "react-redux";
import QuestionList from "../components/questionList";
import { Segment, Dimmer, Loader, Image } from "semantic-ui-react";
import { Box, Grid } from "grommet";
import questionService from "../services/questionsService";
import ScreenContainer from "../components/common/screenContainer";
import SideBar from "./../components/common/sideBar";
import StatsService from "./../services/statsService";

class HomeScreen extends Component {
  constructor(props) {
    super(props);
    this.state = {
      questions: [],
      loading: true,
      savedCount: 0,
      draftCount: 0
    };
  }

  componentDidMount() {
    const { isAuthenticated } = this.props.user;

    questionService.getLatestQuestions().then(response => {
      const questions = response.data.data;
      this.setState({ questions: questions, loading: false });
    });

    if (isAuthenticated) {
      StatsService.GetUserStats().then(response => {
        this.setState({
          savedCount: response[1].data.data.savedCount,
          draftCount: response[0].data.data.draftCount
        });
      });
    }
  }

  render() {
    const { loading, questions } = this.state;
    const { isAuthenticated } = this.props.user;

    return (
      <ScreenContainer
        left={
          <SideBar
            isUserAuthenticated={isAuthenticated}
            savedCount={this.state.savedCount}
            draftCount={this.state.draftCount}
          />
        }
        middle={
          loading ? (
            <Loader active></Loader>
          ) : (
            <QuestionList
              questions={questions}
              isUserAuthenticated={isAuthenticated}
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
