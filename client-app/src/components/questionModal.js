import React, { Component } from "react";
import questionService from "../services/questionsService";
import {
  Modal,
  Segment,
  Header,
  Icon,
  Button,
  Form,
  Label,
  Input
} from "semantic-ui-react";
import {withRouter} from "react-router-dom"



class QuestionModal extends Component {
  constructor(props) {
    super(props);
    this.state = {
      postingQue: false,
      showSuccessMessage: false,
      questionError: false,
      questionText: "",
      postedQuestionId: null // id of the question successfully posted
    };
  }

  toggleModal = () => {
    const modalStatus = this.props.modalOpened;
    if (modalStatus) {
      //when the modal is closed, we want to make sure
      //that it gets reset to default state;
      this.setState({ showSuccessMessage: false });
      this.setState({ questionText: "" });
    }

    //call the base toglleModal to update the state
    //so that modal could be closed
    this.props.toggleModal();
  };

  gotoQuestion = () => {
    const { postedQuestionId } = this.state;    
    this.toggleModal();
    this.props.history.push(`/question/${postedQuestionId}`);
  };
  postQuestion = () => {
    const { questionText } = this.state;

    if (!questionText.length) {
      this.setState({ questionError: true });
      return;
    }

    this.setState({ postingQue: true });
    questionService
      .postQuestion({ questionText })
      .then(response => {
        this.setState({ postingQue: false });
        this.setState({ showSuccessMessage: true });
        const { id } = response.data.data;
        this.setState({ postedQuestionId: id });
      })
      .catch(err => console.log(err));
  };

  onQueTextChange = e => {
    const text = e.target.value;
    this.setState({ questionText: text });
    if (!text.length) this.setState({ questionError: true });
    else this.setState({ questionError: false });
  };
  render() {
    const { postingQue, showSuccessMessage, questionError } = this.state;
    const { modalOpened } = this.props;
    return (
      <Modal open={modalOpened}>
        <Modal.Header>Ask Question</Modal.Header>
        <Modal.Content>
          {showSuccessMessage ? (
            <Segment placeholder>
              <Header icon>
                <Icon name="check circle" />
                Your question has been posted successfully.
              </Header>

              <Button onClick={this.gotoQuestion} positive>
                {" "}
                Goto My Question{" "}
              </Button>
            </Segment>
          ) : (
            <Form>
              {/* <Input
                value={this.state.questionText}
                onChange={this.onQueTextChange}
                error={questionError}
                placeholder="Type your question here"
              / */}
              <Form.Field>
                <Input
                  value={this.state.questionText}
                  onChange={this.onQueTextChange}
                  error={questionError}
                  placeholder="Type your question here"
                />
                {questionError ? (
                  <Label basic color="red" pointing>
                    Please enter a question
                  </Label>
                ) : (
                  ""
                )}
              </Form.Field>
            </Form>
          )}
        </Modal.Content>

        <Modal.Actions>
          <Button onClick={this.toggleModal} negative={!showSuccessMessage}>
            {showSuccessMessage ? "OK" : "Cancel"}
          </Button>
          {showSuccessMessage ? (
            ""
          ) : (
            <Button
              onClick={this.postQuestion}
              positive
              labelPosition="right"
              icon="checkmark"
              loading={postingQue}
              content={postingQue ? "Posting..." : "Post Question"}
            />
          )}
        </Modal.Actions>
      </Modal>
    );
  }
}

//export default withRouter(QuestionModal);
export default withRouter(QuestionModal);
