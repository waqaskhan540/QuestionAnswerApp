import React, { Component } from "react";
import { Item } from "semantic-ui-react";
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
      <Item.Group>
        {questions.map(question => (
          <Item key={question.id}>
            <Item.Image size="tiny" src="https://via.placeholder.com/100" />

            <Item.Content>
              <Item.Header as="a">{question.questionText}</Item.Header>
              <Item.Meta>Description</Item.Meta>

              <Item.Extra>Additional Details</Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    );
  }
}

export default QuestionsList;
