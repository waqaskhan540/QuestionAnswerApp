import React, { Component } from "react";
import TextEditor from "../components/textEditor";
import questionService from "../services/questionsService";
import AnswerService from "../services/answerService";
import DraftService from "../services/draftService";
import { Grid, Box } from "grommet";
import { Loader } from "semantic-ui-react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as WriteAnswerActions from "./../actions/writeAnswerActions";
import ScreenContainer from "./../components/common/screenContainer";

class WriteAnswerScreen extends Component {
  postAnswer = answer => {
    const { id } = this.props.match.params;
    this.props.actions.savingAnswer(true);
    AnswerService.postAnswer(id, answer)
      .then(response => {
        this.props.actions.savingAnswer(false);
        this.props.history.push(`/question/${id}`);
      })
      .catch(err => console.log(err));
  };

  saveDraft = draft => {
    this.props.actions.savingDraft(true);
    DraftService.saveDraft(draft)
      .then(response => {
        this.props.actions.savingDraft(false);
        console.log(response);
      })
      .catch(err => {
        this.props.actions.savingDraft(false);
        console.log(err);
      });
  };

  componentDidMount() {
    const { id } = this.props.match.params;
    questionService.getQuestionById(id).then(response => {
      const question = response.data.data;
      this.props.actions.questionLoaded(question);
    });
  }
  render() {
    const {
      question,
      publishingAnswer,
      savingDraft,
      loadingQuestion
    } = this.props.writeAnswer;
    return (
      <ScreenContainer
        middle={
          loadingQuestion ? (
            <Loader active inline="centered" />
          ) : (
            <TextEditor
              onSaveDraft={this.saveDraft}
              onPostAnswer={this.postAnswer}
              publishingAnswer={publishingAnswer}
              savingDraft={savingDraft}
              question={question}
            />
          )
        }
      />
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user,
    writeAnswer: state.writeAnswer
  };
};
const mapDispatchToProps = dispatch => {
  return {
    actions: bindActionCreators(WriteAnswerActions, dispatch)
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(WriteAnswerScreen);
