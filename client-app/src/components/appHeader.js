import React from "react";
import { Input, Menu } from "semantic-ui-react";
import {withRouter} from "react-router-dom"

class AppHeader extends React.Component {
  state = { activeItem: "home" };

  handleItemClick = (e, { name }) => {
    this.setState({ activeItem: name });
    this.props.history.push("/"+name)
  }

  render() {
    const { activeItem } = this.state;
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
          </Menu.Menu>
        </Menu>
      </div>
    );
  }
}

export default withRouter(AppHeader);