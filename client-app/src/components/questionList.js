import React, { Component } from "react";
import { Segment } from "semantic-ui-react";
import questionService from "../services/questionsService";

class QuestionsList extends Component {
  constructor(props) {
    super(props);

    this.state = {
      questions: [],
      loading: false
    };
  }

  componentDidMount() {
    this.setState({ loading: true });
    questionService.getLatestQuestions().then(response => {
      const questions = response.data.data;
      this.setState({ questions: questions, loading: false });
    });
  }
  render() {
    const { loading, questions } = this.state;
    return (
      <div>
        {questions.map(question => (
          <Segment raised key={question.id}>{question.questionText}</Segment>
        ))}
      </div>
    );
  }
}

export default QuestionsList;
