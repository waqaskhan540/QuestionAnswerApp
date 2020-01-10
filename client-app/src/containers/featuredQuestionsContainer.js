import React, { Component } from "react";
import FeaturedQuestions from "../components/featuredQuestions";
import questionService from "../services/questionsService";

export default class FeaturedQuestionsContainer extends Component {
  state = {
    featuredQuestions: [],
    loading: true
  };

  componentDidMount() {
    questionService
      .getFeaturedQuestions()
      .then(response =>
        this.setState({ featuredQuestions: response.data.data, loading: false })
      )
      .catch(err => {
        console.error(err);
        this.setState({ loading: false });
      });
  }
  render() {
    const { loading, featuredQuestions } = this.state;
    return (
      <FeaturedQuestions
        loading={loading}
        featuredQuestions={featuredQuestions}
      />
    );
  }
}
