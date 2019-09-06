import React from "react";
import { Input, Menu, Icon } from "semantic-ui-react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";

class AppHeader extends React.Component {
  state = { activeItem: "home" };

  handleItemClick = (e, { name }) => {
    this.setState({ activeItem: name });
    this.props.history.push("/" + name);
  };

  render() {
    const { activeItem } = this.state;
    const { user } = this.props;

    return (
      <div>
        <Menu pointing>
          <Menu.Item
            name="home"
            active={activeItem === "home"}
            onClick={this.handleItemClick}
          />
          <Menu.Item
            name="questions"
            active={activeItem === "questions"}
            onClick={this.handleItemClick}
          />
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
                {user.firstname}
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
