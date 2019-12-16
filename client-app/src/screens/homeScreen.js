import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Loader } from "semantic-ui-react";
import QuestionList from "../components/questionList";
import questionService from "../services/questionsService";
import ScreenContainer from "../components/common/screenContainer";
import StatsService from "./../services/statsService";
import WritePost from "./../containers/writePostContainer";
import * as UserActions from "./../actions/userActions";
import { Box } from "grommet";

class HomeScreen extends Component {
  state = {
    page: 1,
    questions: [],
    loading: false
  };

  loadFeed = () => {
    const { page } = this.state;
    // this.setState({loading:true})
    questionService.getFeedData(page).then(response => {
      const questions = response.data.data;
     // this.props.actions.userQuestionsLoaded(questions);

      this.setState({
        page: page + 1,
        questions: [...this.state.questions, ...questions],
        loading: false
      });
    });
  };

  componentWillUnmount() {
    this.props.actions.userResetPage();
  }
  componentDidMount() {
    const { isAuthenticated } = this.props.user;

    this.loadFeed();
    // if (isAuthenticated) {
    //   this.props.actions.userStatsUpdating(true);
    //   StatsService.GetUserStats().then(response => {
    //     const savedCount = response[1].data.data.savedCount;
    //     const draftCount = response[0].data.data.draftCount;
    //     this.props.actions.userStatsUpdated({ savedCount, draftCount });
    //     this.props.actions.userStatsUpdating(false);
    //   });
    // }
  }

  render() {
    const { isAuthenticated} = this.props.user;
    const { questions, loading } = this.state;

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
                onloadMore={this.loadFeed}
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
