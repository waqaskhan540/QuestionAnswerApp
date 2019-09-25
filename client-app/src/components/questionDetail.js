import React, { Component } from "react";

import { Header, Divider, Container, Segment, Button } from "semantic-ui-react";
import { Link } from "react-router-dom";

class QuestionDetail extends Component {
  render() {
    const { isLoading, answers, question, isUserAuthenticated } = this.props;
    if (isLoading) return <div>Loading ...</div>;

    return (
      <Container>
        <Header as="h1">{question.questionText}</Header>
        <span>{question.user.firstName}&nbsp;</span>
        <span>{question.user.lastName}</span> - &nbsp;
        <span>{new Date(question.dateTime).toLocaleDateString()}</span>
        <div>
          {isUserAuthenticated ? (
            <Link to={`/write/${question.id}`}>
              <Button content="Write an Answer" basic />
            </Link>
          ) : (
            ""
          )}
        </div>
        <Divider />
        {answers.map(ans => (
          <Segment key={ans.answerId}>
            <Header as="h3">
              {ans.user.firstName} {ans.user.lastName}
            </Header>
            <p>{ans.answerMarkup}</p>
          </Segment>
        ))}
      </Container>
    );
  }
}

export default QuestionDetail;
