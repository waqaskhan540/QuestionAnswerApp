import React, { Component } from "react";
import { connect } from "react-redux";
import QuestionList from "../components/questionList";
import { Grid, Segment } from "semantic-ui-react";

class HomeScreen extends Component {
  render() {
    return (
      <div>
        <Grid container columns={3} padded>
        <Grid.Column width={3}></Grid.Column>
          <Grid.Column width={10}>
            <QuestionList />
          </Grid.Column>
          <Grid.Column width={3}></Grid.Column>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default connect(mapStateToProps)(HomeScreen);
