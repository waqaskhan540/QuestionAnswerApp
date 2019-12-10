import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Loader } from "semantic-ui-react";
import QuestionList from "../components/questionList";
import questionService from "../services/questionsService";
import ScreenContainer from "../components/common/screenContainer";
import SideBar from "./../components/common/sideBar";
import StatsService from "./../services/statsService";
import WritePost from "./../containers/writePostContainer";
import * as UserActions from "./../actions/userActions";
import { Box } from "grommet";

class HomeScreen extends Component {
  componentDidMount() {
    const { isAuthenticated } = this.props.user;
    questionService.getLatestQuestions().then(response => {
      const questions = response.data.data;
      this.props.actions.userQuestionsLoaded(questions);
    });

    if (isAuthenticated) {
      this.props.actions.userStatsUpdating(true);
      StatsService.GetUserStats().then(response => {
        const savedCount = response[1].data.data.savedCount;
        const draftCount = response[0].data.data.draftCount;
        this.props.actions.userStatsUpdated({ savedCount, draftCount });
        this.props.actions.userStatsUpdating(false);
      });
    }
  }

  render() {
    const {
      loading,
      questions,
      isAuthenticated,
      savedCount,
      draftCount
    } = this.props.user;

    return (
      <>
        {isAuthenticated && (
          <Box
            align="center"
            alignContent="center"
            fill
            pad={{ horizontal: "small", vertical: "xsmall" }}
          >
            <WritePost />
          </Box>
        )}
        <ScreenContainer
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
      </>
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
export default connect(mapStateToProps, mapDispatchToProps)(HomeScreen);
