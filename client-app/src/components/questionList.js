import React, { Component } from "react";
import { Item, Label, Segment } from "semantic-ui-react";
import questionService from "../services/questionsService";
import { Link } from "react-router-dom";

class QuestionsList extends Component {
  constructor(props) {
    super(props);

    // this.state = {
    //   questions: [],
    //   loading: false
    // };
  }

  componentDidMount() {
    // this.setState({ loading: true });
    // questionService.getLatestQuestions().then(response => {
    //   const questions = response.data.data;
    //   this.setState({ questions: questions, loading: false });
    // });
  }
  render() {
    const { questions } = this.props;

    return (
      <Item.Group>
        {questions.map(question => (
         
            <Item key={question.id}>
              <Item.Image size="tiny" src="https://via.placeholder.com/150" />

              <Item.Content>
                <Item.Header>
                  <Link to={`/question/${question.id}`}>
                    {question.questionText}
                  </Link>
                </Item.Header>
                <Item.Meta></Item.Meta>
                <Item.Extra>
                  {/* <Label>14 Answers</Label> */}
                  {/* <Label content='Additional Languages' /> */}
                </Item.Extra>
              </Item.Content>
            </Item>
         
        ))}
      </Item.Group>
    );
  }
}

export default QuestionsList;
