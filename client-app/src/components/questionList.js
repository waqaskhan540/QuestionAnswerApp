import React, { Component } from "react";
import { Item, Label } from "semantic-ui-react";
import { Link } from "react-router-dom";

class QuestionsList extends Component {
  render() {
    const { questions, isUserAuthenticated } = this.props;

    return (
      <Item.Group divided>
        {questions.map(question => (
          <Item key={question.id}>
            <Item.Image size="tiny" src="https://via.placeholder.com/150" />

            <Item.Content>
              <Item.Header>
                <Link to={`/question/${question.id}`}>
                  {question.questionText}
                </Link>
              </Item.Header>
              <Item.Meta>
                <span>{question.user.firstName}</span>
                <span>{question.user.lastName}</span> -&nbsp;
                <span>{new Date(question.dateTime).toLocaleDateString()}</span>
              </Item.Meta>
              <Item.Extra>
                <Label content="Answers (12)" />
                {isUserAuthenticated ? (
                  <Label as="a" basic color="blue">
                    Write Answer
                  </Label>
                ) : (
                  ""
                )}
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    );
  }
}

export default QuestionsList;
