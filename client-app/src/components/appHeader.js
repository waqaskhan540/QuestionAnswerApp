import React from "react";
import { Input, Menu, Icon, Dropdown, Divider } from "semantic-ui-react";
import QuestionModal from "../components/questionModal";

const AppHeader = ({
  user,
  activeItem,
  modalOpened,
  toggleModal,
  handleItemClick,
  history
}) => (
  <div>
    <Menu>
      <Menu.Item
        name="home"
        active={activeItem === "home"}
        onClick={handleItemClick}
      />

      {user.isAuthenticated ? (
        <>
          <Menu.Item
            name="myquestions"
            active={activeItem === "myquestions"}
            onClick={handleItemClick}
          >
            <Icon name="list alternate" /> My Questions
          </Menu.Item>
          <Menu.Item name="Ask Question" onClick={toggleModal}>
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
              <Dropdown.Item onClick={() => history.push("/profile")}>
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
              onClick={handleItemClick}
            />
            <Menu.Item
              name="register"
              active={activeItem === "register"}
              onClick={handleItemClick}
            />
          </>
        )}
      </Menu.Menu>
    </Menu>
    
  </div>
);

export default AppHeader;
