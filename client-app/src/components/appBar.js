import React, { Component } from "react";
import { Box, Button, Heading, Menu } from "grommet";
import QuestionModal from "../components/questionModal";
import { withRouter } from "react-router-dom";
import { Avatar } from "grommet-controls";

class AppBar extends Component {
  render() {
    const { modalOpened, toggleModal, user, history } = this.props;
    return (
      <Box
        tag="header"
        elevation="xsmall"
        direction="row"
        align="start"
        justify="start"
        pad={{ vertical: "small", horizontal: "medium" }}
      >
        <Heading level={3}>QnA</Heading>
        <Button label="Home" href={"/home"} style={{ marginLeft: "20px" }} />
        {user.isAuthenticated ? (
          <>
            <Button
              label="Questions"
              href={"/myquestions"}
              style={{ marginLeft: "20px" }}
            />
            <Button
              label="Ask"
              style={{ marginLeft: "20px" }}
              onClick={toggleModal}
            />
          </>
        ) : (
          ""
        )}

        <Box direction="row" align="end" basis="3/4" justify="end">
          {user.isAuthenticated ? (
            <>
              <Menu
                //label={user.lastname}
                icon={<Avatar image={`data:image/png;base64, ${user.image}`} />}
                items={[
                  {
                    label: "Profile",
                    onClick: () => {
                      history.push("/profile");
                    }
                  },
                  {
                    label: "Log Out",
                    onClick: () => {
                      //history.push("/logout");
                    }
                  }
                ]}
              />
            </>
          ) : (
            <>
              <Button label="Login" href={"/login"} />
              <Button
                label="Register"
                href={"/register"}
                style={{ marginLeft: "20px" }}
              />
            </>
          )}
        </Box>
        <QuestionModal modalOpened={modalOpened} toggleModal={toggleModal} />
      </Box>
    );
  }
}

export default withRouter(AppBar);
