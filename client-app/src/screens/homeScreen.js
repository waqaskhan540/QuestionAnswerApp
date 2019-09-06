import React, { Component } from "react";
import { connect } from "react-redux";
import QuestionList from "../components/questionList";
import { Grid, Segment } from "semantic-ui-react";

class HomeScreen extends Component {
  render() {
    return (
      <div>
        <Grid container columns={2} padded>
          <Grid.Column>
            <QuestionList />
          </Grid.Column>
          <Grid.Column></Grid.Column>
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
