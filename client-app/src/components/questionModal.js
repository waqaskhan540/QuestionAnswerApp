import React, { Component } from "react";
import questionService from "../services/questionsService";
import {
  Modal,
  Segment,
  Header,
  Icon,
  Button,
  Form,
  TextArea
} from "semantic-ui-react";
class QuestionModal extends Component {
  constructor(props) {
    super(props);
    this.state = {
      postingQue: false,
      showSuccessMessage: false,
      questionText: ""
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
  postQuestion = () => {
    this.setState({ postingQue: true });
    const { questionText } = this.state;

    questionService
      .postQuestion({ questionText })
      .then(response => {
        this.setState({ postingQue: false });
        this.setState({ showSuccessMessage: true });
      })
      .catch(err => console.log(err));
  };

  onQueTextChange = e => {
    this.setState({ questionText: e.target.value });
  };
  render() {
    const { postingQue, showSuccessMessage } = this.state;
    const { modalOpened, toggleModal } = this.props;
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
              <Button primary>Goto My Question</Button>
            </Segment>
          ) : (
            <Form>
              <TextArea
                value={this.state.questionText}
                onChange={this.onQueTextChange}
                placeholder="Type your question here"
              />
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

export default QuestionModal;
