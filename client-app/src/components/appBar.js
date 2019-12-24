import React, { Component } from "react";
import { Box, Button, Heading, Menu, Header, Anchor, TextInput } from "grommet";
import QuestionModal from "../components/questionModal";
import { withRouter } from "react-router-dom";
//import { Avatar } from "grommet-controls";
import { Search, Home, UserManager, Edit ,Notification} from "grommet-icons";
import { Icon } from "semantic-ui-react";
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

export const Avatar = ({ image }) => (
  <Box
    height="xxsmall"
    width="xxsmall"
    round="full"
    alignSelf="center"
    background={`url(data:image/png;base64, ${image})`}
  />
);
class AppBar extends Component {
  render() {
    const { modalOpened, toggleModal, user, history } = this.props;
    return (
      <Header background="light-4" pad="small">
        <Box gap="medium">
          <Heading level={3} style={{ fontFamily: "Pacifico" }}>
            QnA
          </Heading>
        </Box>
        <Box direction="row" gap="xsmall">
          <Button
            icon={<Home />}
            label="Home"
            onClick={e => {
              e.preventDefault();
            }}
            primary
            color="light-3"
          />
          <Button
            icon={<UserManager />}
            label="Profile"
            onClick={e => {
              e.preventDefault();
            }}
            primary
            color="light-3"
          />
        </Box>
        <Button icon={<Notification />}/>
        <SearchBar />
        <Button
          icon={<Edit />}
          label="Ask"
          onClick={e => {
            e.preventDefault();
          }}
          primary
          // color="dark-3"
        />
        <Avatar image={user.image} />
      </Header>

      // <Box
      //   tag="header"
      //   alignContent="center"
      //   direction="row"
      //   align="start"
      //   justify="start"
      //   pad={{ vertical: "small", horizontal: "small" }}
      // >
      //   <Heading level={2} style={{ fontFamily: "Pacifico" }}>
      //     QnA
      //   </Heading>
      //   <Button
      //     primary={true}
      //     label="Home"
      //     href={"/home"}
      //     style={{ marginLeft: "20px" }}
      //   />
      //   {user.isAuthenticated ? (
      //     <>
      //       <Button
      //         label="Questions"
      //         primary={true}
      //         href={"/myquestions"}
      //         style={{ marginLeft: "20px" }}
      //       />
      //       <Button
      //         label="Ask"
      //         primary={true}
      //         style={{ marginLeft: "20px" }}
      //         onClick={toggleModal}
      //       />
      //     </>
      //   ) : (
      //     ""
      //   )}

      //   <Box direction="row" align="end" basis="3/4" justify="end">
      //     {user.isAuthenticated ? (
      //       <>
      //         <Menu
      //           icon={
      //             user.image ? (
      //               <Avatar image={`data:image/png;base64, ${user.image}`} />
      //             ) : (
      //               <Icon disabled name="user circle" size="big" />
      //             )
      //           }
      //           items={[
      //             {
      //               label: "Profile",
      //               onClick: () => {
      //                 history.push("/profile");
      //               }
      //             },
      //             {
      //               label: "Log Out",
      //               onClick: () => {
      //                 //history.push("/logout");
      //                 localStorage.removeItem("state");
      //                 if (window.gapi) {
      //                   const auth = window.gapi.auth2.getAuthInstance();
      //                   if (auth != null) {
      //                     auth
      //                       .signOut()
      //                       .then(re => console.log("logout from google"));
      //                   }
      //                 }

      //                 if (window.FB && window.FB.getAccessToken()) {
      //                   window.FB.logout();
      //                 }
      //                 if (history.location.pathname === "/")
      //                   window.location.reload();
      //                 else history.push("/");
      //               }
      //             }
      //           ]}
      //         />
      //       </>
      //     ) : (
      //       <>
      //         <Button primary={true} label="Login" href={"/login"} />
      //         <Button
      //           primary={true}
      //           label="Register"
      //           href={"/register"}
      //           style={{ marginLeft: "20px" }}
      //         />
      //       </>
      //     )}
      //   </Box>
      //   <QuestionModal modalOpened={modalOpened} toggleModal={toggleModal} />
      // </Box>
    );
  }
}

export default withRouter(AppBar);
