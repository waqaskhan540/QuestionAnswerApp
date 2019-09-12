import React, { Component } from "react";
import MyQuestions from "../components/myQuestions";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";

class MyQuestionsScreen extends Component {
  componentWillMount() {
    const { history, user } = this.props;
    if (!user.isAuthenticated) {
        let returnUrl = "/myquestions"
        history.push(`/login?returnUrl=${returnUrl}`)
    }
  }
  render() {
    return <MyQuestions />;
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default withRouter(connect(mapStateToProps)(MyQuestionsScreen));
