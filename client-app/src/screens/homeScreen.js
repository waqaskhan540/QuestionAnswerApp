import React, { Component } from "react";
import { connect } from "react-redux";
import QuestionList from "../components/questionList";
import { Segment, Dimmer, Loader ,Image} from "semantic-ui-react";
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
        {/* <Grid container columns={3} padded>
          <Grid.Column width={5}></Grid.Column>
          <Grid.Column width={8}> */}
           
              {loading ? (
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
              )}
          
          {/* </Grid.Column>
          <Grid.Column width={3}></Grid.Column>
        </Grid> */}
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
