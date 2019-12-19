import React, { Component } from "react";
import WritePost from "./../components/writePost";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as UserActions from "./../actions/userActions";
import * as FeedActions from "./../actions/feedActions";
import questionService from "./../services/questionsService";

class WritePostContainer extends Component {
  state = {
    value: "",
    isLoading: false
  };

  onChange = e => {
    this.setState({ value: e.target.value });
  };

  onKeyPress = e => {
    if (e.key === "Enter") {
      this.setState({ isLoading: true });
      const questionText = e.target.value;
      if (!questionText.length) return;

      this.props.feedActions.postingToFeed();
      questionService
        .postQuestion({ questionText })
        .then(response => {
          // this.props.actions.userUpdateQuestions(response.data.data);
          this.props.feedActions.postedToFeed(response.data.data);
          this.setState({ isLoading: false, value: "" });
        })
        .catch(err => {
          console.log(err);
          this.setState({ isLoading: false });
        });
    }
  };
  render() {
    const { isLoading } = this.state;

    return (
      <WritePost
        value={this.state.value}
        onChange={this.onChange}
        onKeyPress={this.onKeyPress}
        isLoading={isLoading}
      />
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
    // feed : state.feed
  };
};

const mapDispatchToProps = dispatch => {
  return {
    actions: bindActionCreators(UserActions, dispatch),
    feedActions: bindActionCreators(FeedActions, dispatch)
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(WritePostContainer);
