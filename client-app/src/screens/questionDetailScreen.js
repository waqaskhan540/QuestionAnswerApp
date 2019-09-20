import React, { Component } from "react";
import QuestionDetail from "../components/questionDetail";
import AnswerService from "../services/answerService";
import QuestionService from "../services/questionsService";

class QuestionDetailScreen extends Component {
  state = {
    isloading: true,
    question: null,
    answers: []
  };
  componentDidMount() {
    const { id } = this.props.match.params;

    QuestionService.getQuestionById(id).then(response => {
      this.setState({ question: response.data.data });
    });

    AnswerService.getAnswersByQuestionId(id).then(response => {
      this.setState({ isloading: false });
      this.setState({ answers: response.data.data });
    });
  }

  render() {
    const { isloading, answers, question } = this.state;
    return (
      <QuestionDetail
        isLoading={isloading}
        answers={answers}
        question={question}
      />
    );
  }
}

export default QuestionDetailScreen;
