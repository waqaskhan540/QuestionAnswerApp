import React, { Component } from "react";
import { Box, Button, Heading, Menu } from "grommet";
import QuestionModal from "../components/questionModal";
import { withRouter } from "react-router-dom";
import { Avatar } from "grommet-controls";
import { Icon } from "semantic-ui-react";
import "./styles/appBar.css";

class AppBar extends Component {
  render() {
    const { modalOpened, toggleModal, user, history } = this.props;
    return (
      <Box
        tag="header"
        alignContent="center"
        direction="row"
        align="start"
        justify="start"
        pad={{ vertical: "small", horizontal: "small" }}
      >
        <Heading level={2} style={{ fontFamily: "Pacifico" }}>
          QnA
        </Heading>
        <Button
          primary={true}
          label="Home"
          href={"/home"}
          style={{ marginLeft: "20px" }}
        />
        {user.isAuthenticated ? (
          <>
            <Button
              label="Questions"
              primary={true}
              href={"/myquestions"}
              style={{ marginLeft: "20px" }}
            />
            <Button
              label="Ask"
              primary={true}
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
                icon={
                  user.image ? (
                    <Avatar image={`data:image/png;base64, ${user.image}`} />
                  ) : (
                    <Icon disabled name="user circle" size="big" />
                  )
                }
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
                      localStorage.removeItem("state");
                      if (window.gapi) {
                        const auth = window.gapi.auth2.getAuthInstance();
                        if (auth != null) {
                          auth
                            .signOut()
                            .then(re => console.log("logout from google"));
                        }
                      }

                      if (window.FB && window.FB.getAccessToken()) {
                        window.FB.logout();
                      }
                      if (history.location.pathname === "/")
                        window.location.reload();
                      else history.push("/");
                    }
                  }
                ]}
              />
            </>
          ) : (
            <>
              <Button primary={true} label="Login" href={"/login"} />
              <Button
                primary={true}
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
