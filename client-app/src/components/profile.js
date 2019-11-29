import React, { Component } from "react";
import { Item, Image, Icon } from "semantic-ui-react";
import { Box } from "grommet";
import axios from "axios";
import userProfileService from "./../services/userProfileService";

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
      <Box margin={"large"}>
        {image && image.length ? (
          <Image src={`data:image/png;base64, ${image}`} size={"small"} />
        ) : (
          <div>
            <input
              type="file"
              name="file"
              ref={ref => {
                this.uploadInput = ref;
              }}
            />
            <button onClick={this.uploadImage}>Upload</button>
          </div>
        )}
        <div>{firstname}</div>
        <div>{lastname}</div>
      </Box>
    );
  }
}

export default Profile;
