import React, { Component } from "react";
import questionService from "../services/questionsService";
import {
  Header,
  Divider,
  Container,
  Image,
  Segment,
  Button
} from "semantic-ui-react";
import { Link } from "react-router-dom";

class QuestionDetail extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isLoading: true,
      question: null
    };
  }

  componentDidMount() {
    const { questionId } = this.props;

    questionService
      .getQuestionById(questionId)
      .then(response => {
        const questionData = response.data.data;
        this.setState({ question: questionData });
        this.setState({ isLoading: false });
      })
      .catch(err => this.setState({ isLoading: false }));
  }
  render() {
    const { isLoading, question } = this.state;
    if (isLoading) return <div>Loading question ...</div>;

    return (
      <Container>
        <Header as="h1">{question.questionText}</Header>
        <span>{question.user.firstName}&nbsp;</span>
        <span>{question.user.lastName}</span> - &nbsp;
        <span>{new Date(question.dateTime).toLocaleDateString()}</span>
        <div>
          <Link to={`write/${question.id}`}>
            <Button content="Write an Answer" basic />
          </Link>
        </div>
        <Divider />
        <Segment>
          <Header as="h3">Answer One</Header>
          <Image src="http://semantic-ui.com/images/wireframe/paragraph.png" />

          <Divider section />

          <Header as="h3">Answer Two</Header>
          <Image src="http://semantic-ui.com/images/wireframe/paragraph.png" />
        </Segment>
      </Container>
    );
  }
}

export default QuestionDetail;
