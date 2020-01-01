import React, { Component } from "react";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { Loader } from "semantic-ui-react";
import QuestionList from "../components/questionList";
import questionService from "../services/questionsService";
import ScreenContainer from "../components/common/screenContainer";
import WritePost from "./../containers/writePostContainer";
import * as UserActions from "./../actions/userActions";
import * as FeedActions from "./../actions/feedActions";
import { Box } from "grommet";
import { Spinning } from "grommet-controls";

class HomeScreen extends Component {
  // state = {
  //   page: 1,
  //   questions: [],
  //   loading: false
  // };

  loadFeed = () => {
    const { page } = this.props.feed;
    // this.setState({loading:true})
    this.props.feedActions.isFeedLoading(true);
    questionService.getFeedData(page).then(response => {
      const questions = response.data.data;
      // this.props.actions.userQuestionsLoaded(questions);

      // this.setState({
      //   page: page + 1,
      //   questions: [...this.state.questions, ...questions],
      //   loading: false
      // });

      this.props.feedActions.updateQuestions(questions);
      this.props.feedActions.isFeedLoading(false);
    });
  };

  initFeed = () => {
    this.props.feedActions.isFeedLoading(true);
    questionService.getFeedData(1).then(response => {
      const questions = response.data.data;
      // this.props.actions.userQuestionsLoaded(questions);

      // this.setState({
      //   page: page + 1,
      //   questions: [...this.state.questions, ...questions],
      //   loading: false
      // });

      this.props.feedActions.loadQuestionsFirstTime(questions);
      this.props.feedActions.isFeedLoading(false);
    });
  };

  componentDidMount() {
    //const { isAuthenticated } = this.props.user;

    this.initFeed();
    //this.props.feedActions.resetPage();
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
    const { isAuthenticated } = this.props.user;
    const { questions, loading, postingToFeed } = this.props.feed;

    return (
      <>
        <ScreenContainer
          left={
            loading ? (
              <Loader active></Loader>
            ) : (
              <>
                
                {postingToFeed ? (
                  <Box align="center" alignContent="center">
                    <Spinning kind="three-bounce" />
                  </Box>
                ) : null}
               <QuestionList
                  questions={questions}
                  isUserAuthenticated={isAuthenticated}
                  onloadMore={this.loadFeed}
                /> 
              </>
            )
          }
        />
      </>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user,
    feed: state.feed
  };
};
const mapDispatchToProps = dispatch => {
  return {
    actions: bindActionCreators(UserActions, dispatch),
    feedActions: bindActionCreators(FeedActions, dispatch)
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(HomeScreen);
