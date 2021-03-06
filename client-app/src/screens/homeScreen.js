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
import { Box, Button } from "grommet";
import { Spinning } from "grommet-controls";
import { toast } from "react-toastify";
import FeaturedQuestions from "../containers/featuredQuestionsContainer";
import * as signalR from "@aspnet/signalr";
import { ItemList } from "../theme/sidebars/ItemList";

class HomeScreen extends Component {
  // state = {
  //   page: 1,
  //   questions: [],
  //   loading: false
  // };
  state = {
    showLoadMore: true,
    loadingMore: false,
    hubConnection: null
  };

  onAnswerQuestion = () => {
    if (this.state.hubConnection) {
      const connection = this.state.hubConnection;
      console.log(connection);
      connection.send("QuestionAnswered", 17);
    }
  };
  loadFeed = () => {
    const { page } = this.props.feed;
    this.setState({ loadingMore: true });
    // this.props.feedActions.isFeedLoading(true);
    questionService.getFeedData(page).then(response => {
      const questions = response.data.data;
      // this.props.actions.userQuestionsLoaded(questions);

      // this.setState({
      //   page: page + 1,
      //   questions: [...this.state.questions, ...questions],
      //   loading: false
      // });
      this.setState({ loadingMore: false });
      if (questions.length < 5) {
        this.setState({ showLoadMore: false });
        return;
      }
      this.props.feedActions.updateQuestions(questions);
      // this.props.feedActions.isFeedLoading(false);
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

  followQuestion = questionId => {
    questionService
      .followQuestion(questionId)
      .then(resp => {
        if (resp.data.data.questionId !== undefined)
          this.props.actions.userFollowQuestion(resp.data.data.questionId);
      })
      .catch(err => toast.error("Something went wrong!"));
  };

  unFollowQuestion = questionId => {
    questionService
      .unFollowQuestion(questionId)
      .then(resp => {
        // toast.success(resp.data.data.message);
        if (resp.data.data.questionId !== undefined)
          this.props.actions.userUnFollowQuestion(resp.data.data.questionId);
      })
      .catch(err => toast.error("Something went wrong!"));
  };

  saveQuestion = questionId => {
    questionService
      .saveQuestion(questionId)
      .then(response => {
        if (response.data.data.questionId !== undefined)
          this.props.actions.userSavedQuestion(response.data.data.questionId);
      })
      .catch(err => toast.error("Something went wrong!"));
  };

  unSaveQuestion = questionId => {
    questionService
      .unSaveQuestion(questionId)
      .then(response => {
        if (response.data.data.questionId !== undefined)
          this.props.actions.userUnSavedQuestion(response.data.data.questionId);
      })
      .catch(err => toast.error("Something went wrong!"));
  };
  componentDidMount() {
    const { isAuthenticated, accessToken } = this.props.user;

    this.initFeed();
    // this.props.feedActions.resetPage();
    // if (isAuthenticated) {
    //   this.props.actions.userStatsUpdating(true);
    //   StatsService.GetUserStats().then(response => {
    //     const savedCount = response[1].data.data.savedCount;
    //     const draftCount = response[0].data.data.draftCount;
    //     this.props.actions.userStatsUpdated({ savedCount, draftCount });
    //     this.props.actions.userStatsUpdating(false);
    //   });
    // }
    if (isAuthenticated) {
      const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5000/followings", {
          accessTokenFactory: () => accessToken
        })
        .build();
      this.setState({ hubConnection: connection });
      connection.start().catch(err => console.error(err));

      connection.on("QuestionAnswered", response => {
        debugger;
        console.log(response);
      });
    }
  }

  render() {
    const {
      isAuthenticated,
      questionsFollowing,
      questionsSaved
    } = this.props.user;
    const { questions, loading, postingToFeed } = this.props.feed;

    return (
      <>
        <ScreenContainer
          left={<ItemList align={"end"} />}
          middle={
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
                  questionsFollowing={questionsFollowing}
                  questionsSaved={questionsSaved}
                  onFollow={this.followQuestion}
                  onUnFollow={this.unFollowQuestion}
                  onSave={this.saveQuestion}
                  onUnSave={this.unSaveQuestion}
                  toggleLoadMore={this.state.showLoadMore}
                  loadingMore={this.state.loadingMore}
                />
              </>
            )
          }
          right={<ItemList align={"start"} />}
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
