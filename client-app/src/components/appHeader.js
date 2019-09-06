import React from "react";
import {
  Input,
  Menu,
  Icon,
  Modal,
  Form,
  Segment,
  Header,
  TextArea,
  Button
} from "semantic-ui-react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import questionService from "../services/questionsService";

class AppHeader extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      activeItem: "home",
      modalOpened: false,
      postingQue: false,
      questionText: "",
      showSuccessMessage: false
    };
  }

  handleItemClick = (e, { name }) => {
    this.setState({ activeItem: name });
    this.props.history.push("/" + name);
  };

  toggleModal = () => {
    const modalStatus = this.state.modalOpened;
    if (modalStatus) {
      this.setState({ showSuccessMessage: false });
      this.setState({ questionText: "" });
    }

    this.setState({ modalOpened: !modalStatus });
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
    const {
      activeItem,
      modalOpened,
      postingQue,
      showSuccessMessage
    } = this.state;
    const { user } = this.props;

    return (
      <div>
        <Menu pointing>
          <Menu.Item
            name="home"
            active={activeItem === "home"}
            onClick={this.handleItemClick}
          />

          {user.isAuthenticated ? (
            <Menu.Item name="Ask Question" onClick={this.toggleModal}>
              <Icon name="question circle" /> Ask Question
            </Menu.Item>
          ) : (
            ""
          )}

          <Menu.Menu position="right">
            <Menu.Item>
              <Input icon="search" placeholder="Search..." />
            </Menu.Item>

            {user.isAuthenticated ? (
              <Menu.Item
                name="user"
                active={activeItem === "user"}
                onClick={this.handleItemClick}
              >
                <Icon name="user" />
                {user.lastname}
              </Menu.Item>
            ) : (
              <>
                <Menu.Item
                  name="login"
                  active={activeItem === "login"}
                  onClick={this.handleItemClick}
                />
                <Menu.Item
                  name="register"
                  active={activeItem === "register"}
                  onClick={this.handleItemClick}
                />
              </>
            )}
          </Menu.Menu>
        </Menu>
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
              {showSuccessMessage ? "OK":"Cancel"}
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
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default withRouter(connect(mapStateToProps)(AppHeader));
