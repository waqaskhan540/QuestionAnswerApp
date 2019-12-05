import React, { Component } from "react";
import Profile from "../components/profile";
import ProfileScreenContainer from "../components/common/profileScreenContainer";
import { connect } from "react-redux";

class ProfileScreen extends Component {
  render() {
    // return <ScreenContainer middle={} />;
    return (
      <ProfileScreenContainer>
        <Profile user={this.props.user} />
      </ProfileScreenContainer>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default connect(mapStateToProps)(ProfileScreen);
