import React, { Component } from "react";

import { Box } from "grommet";
import axios from "axios";
import userProfileService from "./../services/userProfileService";
import { Card, Icon, Image } from "semantic-ui-react";

class Profile extends Component {
  constructor(props) {
    super(props);
    this.state = {
      file: null
    };
  }

  onFileSelected = () => {
    let file = this.uploadInput.files[0];
    this.setState({ file });
  };
  uploadImage = () => {
    const data = new FormData();
    data.append("file", this.uploadInput.files[0]);
    userProfileService
      .uploadImage(data)
      .then(resp => alert("img uploaded"))
      .catch(err => alert("error uploading img"));
  };
  render() {
    const { firstname, lastname, image } = this.props.user;
    return (
      <Box
        direction="row"
        width="large"
        pad="large"
        border={{ side: "bottom", size: "small" }}
      >
        <Box width="xsmall">
          {image && image.length ? (
            <Image
              src={`data:image/png;base64, ${image}`}
              size="small"
              wrapped
            />
          ) : (
            <Image
              src="https://react.semantic-ui.com/images/avatar/large/daniel.jpg"
              size="small"
              wrapped
            />
          )}
        </Box>
        <Box width="medium">
          <Box direction="column">
            <h1>Muhammad Waqas</h1>
            <p>Admin</p>
          </Box>
        </Box>
      </Box>
      // <Card fluid>
      //   <Box round align="center" pad={"medium"} border="small">
      //     {image && image.length ? (
      //       <Image src={`data:image/png;base64, ${image}`} size="small" wrapped/>
      //     ) : (
      //       <Image
      //         src="https://react.semantic-ui.com/images/avatar/large/daniel.jpg"
      //         size="small"
      //         wrapped
      //       />
      //     )}
      //   </Box>
      //   <Card.Content>
      //     <Card.Header>Daniel</Card.Header>
      //     <Card.Meta>Joined in 2016</Card.Meta>
      //     <Card.Description>
      //       Daniel is a comedian living in Nashville.
      //     </Card.Description>
      //   </Card.Content>
      //   <Card.Content extra>
      //     <a>
      //       <Icon name="user" />
      //       10 Friends
      //     </a>
      //   </Card.Content>
      // </Card>
    );
  }
}

export default Profile;
