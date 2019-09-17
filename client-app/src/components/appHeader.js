import React from "react";
import { Input, Menu, Icon, Dropdown, Divider } from "semantic-ui-react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import QuestionModal from "../components/questionModal";

class AppHeader extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      activeItem: "home",
      modalOpened: false
    };
  }

  handleItemClick = (e, { name }) => {
    this.setState({ activeItem: name });
    this.props.history.push("/" + name);
  };

  toggleModal = () => {
    const modalStatus = this.state.modalOpened;
    this.setState({ modalOpened: !modalStatus });
  };

  render() {
    const { activeItem, modalOpened } = this.state;
    const { user } = this.props;

    return (
      <div>
        <Menu>
          <Menu.Item
            name="home"
            active={activeItem === "home"}
            onClick={this.handleItemClick}
          />

          {user.isAuthenticated ? (
            <>
              <Menu.Item
                name="myquestions"
                active={activeItem === "myquestions"}
                onClick={this.handleItemClick}
              >
                <Icon name="list alternate" /> My Questions
              </Menu.Item>
              <Menu.Item name="Ask Question" onClick={this.toggleModal}>
                <Icon name="question circle" /> Ask Question
              </Menu.Item>
            </>
          ) : (
            ""
          )}

          <Menu.Menu position="right">
            <Menu.Item>
              <Input icon="search" placeholder="Search..." />
            </Menu.Item>

            {user.isAuthenticated ? (
              <Dropdown item simple icon="user">
                <Dropdown.Menu>
                  <Dropdown.Item onClick = {() => this.props.history.push("/profile")}>
                    <span className="text">Profile</span>
                  </Dropdown.Item>
                  <Divider />

                  <Dropdown.Item>Logout</Dropdown.Item>
                </Dropdown.Menu>
              </Dropdown>
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
        <QuestionModal
          modalOpened={modalOpened}
          toggleModal={this.toggleModal}
        />
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
