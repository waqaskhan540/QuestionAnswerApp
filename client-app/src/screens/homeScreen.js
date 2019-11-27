import React, { Component } from "react";
import { connect } from "react-redux";
import QuestionList from "../components/questionList";
import { Segment, Dimmer, Loader, Image } from "semantic-ui-react";
import {Box,Grid} from "grommet"
import questionService from "../services/questionsService";

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
      <div>
        {/* {loading ? (
                <Segment>
                  <Dimmer active inverted>
                    <Loader inverted>Loading</Loader>
                  </Dimmer>

                  <Image src="https://react.semantic-ui.com/images/wireframe/short-paragraph.png" />
                </Segment>
              ) : (
                <QuestionList 
                  questions={questions} 
                  isUserAuthenticated = {this.props.user.isAuthenticated}
                  />
              )} */}

        <Grid
          rows={["xlarge"]}
          columns={["small", "large", "small"]}
          gap="small"
          areas={[
            { name: "left", start: [0, 0], end: [0, 0] },
            { name: "middle", start: [1, 0], end: [1, 0] },
            { name: "right", start: [2, 0], end: [2, 0] }
          ]}
        >
          <Box gridArea="left"/>
          <Box gridArea="middle">
            <QuestionList
              questions={questions}
              isUserAuthenticated={this.props.user.isAuthenticated}
            />
          </Box>
          <Box gridArea="right" />
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
