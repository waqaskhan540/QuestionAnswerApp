import React, { Component } from "react";
import { Box, Button, Heading, Menu, Header, Anchor, TextInput } from "grommet";
import QuestionModal from "../components/questionModal";
import { withRouter } from "react-router-dom";
//import { Avatar } from "grommet-controls";
import {
  Search,
  Edit,
  Notification,
  Article,
  Notes,
  Organization,
  Login
} from "grommet-icons";
import { Icon } from "semantic-ui-react";
import {Avatar} from "./common/avatar";
import "./styles/appBar.css";

export const SearchBar = () => (
  <Box
    width="large"
    direction="row"
    align="center"
    pad={{ horizontal: "small", vertical: "xsmall" }}
    round="small"
    // elevation={suggestionOpen ? "medium" : undefined}
    //elevation="medium"
    border={{
      side: "all",
      color: "border"
    }}
    // style={
    //   suggestionOpen
    //     ? {
    //         borderBottomLeftRadius: "0px",
    //         borderBottomRightRadius: "0px"
    //       }
    //     : undefined
    // }
  >
    <Search color="brand" />
    <TextInput
      type="search"
      //dropTarget={boxRef.current}
      plain
      //value={value}
      //onChange={onChange}
      // onSelect={onSelect}
      // suggestions={renderSuggestions()}
      placeholder="Search..."
      //onSuggestionsOpen={() => setSuggestionOpen(true)}
      // onSuggestionsClose={() => setSuggestionOpen(false)}
    />
  </Box>
);


class AppBar extends Component {
  render() {
    const { modalOpened, toggleModal, user, history } = this.props;
    return (
      <Header background="light-4" pad="small">
        <Box gap={"medium"} direction={"row"}>
          <Heading level={3} style={{ fontFamily: "Pacifico" }}>
            QnA
          </Heading>
        </Box>
       
        <Box direction="row" gap="small" alignSelf="center" width="medium">
          <Button
            icon={<Article />}
            label="Feed"
            href="/"
            plain
            //color="light-3"
          />
          <Button icon={<Notes />} label="Topics" plain href="/topics" />
          {user.isAuthenticated && (
            <Button
              icon={<Organization />}
              label="Organizations"
              plain
              href="/organizations"
            />
          )}
          {/* </Box> */}
        </Box>

        <SearchBar />

        {user.isAuthenticated && <Button icon={<Notification />} />}

        {user.isAuthenticated && (
          <Button
            icon={<Edit />}
            label="Ask"
            primary
            onClick={toggleModal}
            color="dark-3"
          />
        )}

        {user.isAuthenticated && (
          <Menu           
            icon={<Avatar image={user.image} />}
            items={[
              { label: "Profile", onClick: () => history.push("/profile") },
              {
                label: "Logout",
                onClick: () => {
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
        )}
        {!user.isAuthenticated && (
          <Button icon={<Login />} label="Login" plain href="/login" />
        )}

        <QuestionModal modalOpened={modalOpened} toggleModal={toggleModal} />
      </Header>      
    );
  }
}

export default withRouter(AppBar);
