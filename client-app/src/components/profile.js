import React, { Component } from "react";
import {
  Box,
  Stack,
  Button,
  Layer,
  Grid,
  Text,
  FormField,
  TextInput
} from "grommet";
import userProfileService from "./../services/userProfileService";
import { Image } from "semantic-ui-react";
import ImageUploader from "react-images-upload";
import axios from "axios";

class Profile extends Component {
  constructor(props) {
    super(props);

    const { firstname, lastname, image, email } = this.props.user;
    this.state = {
      file: null,
      isHovering: false,
      showModal: false,
      profilePicture: null,
      updatingProfile: false,
      disableUpdateButton: true,
      firstname,
      lastname,
      image,
      email
    };
    //this.toggleUpdateButton  = this.toggleUpdateButton.bind(this);
  }

  onMouseEnter = () => this.setState({ isHovering: true });
  onMouseLeave = () => this.setState({ isHovering: false });
  showImageModal = () => this.setState({ showModal: true });
  hideImageModal = () => this.setState({ showModal: false });
  onFirstNameChange = e => {
    const firstname = e.target.value;
    this.setState({ firstname });

    if (firstname.length && firstname != this.props.user.firstname)
      this.setState({ disableUpdateButton: false });
    else this.setState({ disableUpdateButton: true });
  };
  onLastNameChange = e => {
    const lastname = e.target.value;
    this.setState({ lastname });

    if (lastname.length && lastname != this.props.user.lastname)
      this.setState({ disableUpdateButton: false });
    else this.setState({ disableUpdateButton: true });
  };

  onImageSelect = (files, urls) => {
    console.log(files[0]);
    this.setState({ profilePicture: files[0] });
  };

  submitForm = () => {
    this.setState({ updatingProfile: true });
    const data = {
      firstname: this.state.firstname,
      lastname: this.state.lastname
    };

    const { accessToken } = JSON.parse(localStorage.getItem("state")).user;
    axios
      .post("http://localhost:5000/api/profile", data, {
        headers: {
          Authorization: `Bearer ${accessToken}`
        }
      })
      .then(respo => {
        console.log(respo);
        this.setState({ updatingProfile: false });
      })
      .catch(err => {
        console.log(err);
        this.setState({ updatingProfile: false });
      });
  };

  uploadImage = () => {
    console.log(this.state.profilePicture);
    const data = new FormData();
    data.append("file", this.state.profilePicture);
    userProfileService
      .uploadImage(data)
      .then(resp => alert("img uploaded"))
      .catch(err => alert("error uploading img"));
  };
  render() {
    const { firstname, lastname, image, email } = this.state;
    const { isHovering, showModal } = this.state;

    return (
      <Grid
        rows={["large"]}
        columns={["small", "medium", "small"]}
        gap="small"
        areas={[
          { name: "left", start: [0, 0], end: [0, 0] },
          { name: "middle", start: [1, 0], end: [1, 0] },
          { name: "right", start: [2, 0], end: [2, 0] }
        ]}
      >
        <Box
          gridArea="left"
          onMouseEnter={this.onMouseEnter}
          onMouseLeave={this.onMouseLeave}
        >
          <Box width="xsmall">
            <Image src={image} />
            <Text size="xlarge">
              {this.props.user.firstname} {this.props.user.lastname}
            </Text>
            <Button
              label="Edit"
              onClick={this.showImageModal}
              style={{
                fontSize: "10px",

                zIndex: 1000
              }}
            />
          </Box>
        </Box>
        <Box gridArea="middle">
          <FormField
            label="First name"
            margin={"small"}
            required
            error={
              this.state.firstname.length ? "" : "please provide first name"
            }
          >
            <TextInput
              onChange={this.onFirstNameChange}
              placeholder="first name"
              disabled={this.state.updatingProfile}
              value={firstname}
            />
          </FormField>
          <FormField
            label="Last name"
            margin={"small"}
            required
            error={this.state.lastname.length ? "" : "please provide last name"}
          >
            <TextInput
              onChange={this.onLastNameChange}
              placeholder="last name"
              disabled={this.state.updatingProfile}
              value={lastname}
            />
          </FormField>
          <FormField label="Email" margin={"small"}>
            <TextInput placeholder="type here" value={email} disabled />
          </FormField>
          <Button
            disabled={
              this.state.disableUpdateButton || this.state.updatingProfile
            }
            label={
              this.state.updatingProfile ? "Updating..." : "Update Profile"
            }
            primary
            alignSelf="start"
            onClick={this.submitForm}
          />
        </Box>

        <Box gridArea="right" />
        {showModal && (
          <Layer
            onEsc={this.hideImageModal}
            onClickOutside={this.hideImageModal}
          >
            <Box fill pad={"medium"}>
              <ImageUploader
                withIcon={true}
                buttonText="Choose image"
                withPreview={true}
                singleImage={true}
                onChange={this.onImageSelect}
                imgExtension={[".jpg", ".jpeg", ".gif", ".png", ".gif"]}
                maxFileSize={5242880}
              />

              <Button onClick={this.uploadImage} label="Upload" />
            </Box>
          </Layer>
        )}
      </Grid>
    );
  }
}

export default Profile;
