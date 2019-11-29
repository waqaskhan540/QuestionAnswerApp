import React, { Component } from "react";
import Profile from "../components/profile";
import ScreenContainer from "../components/common/screenContainer";
import { connect } from "react-redux";

class ProfileScreen extends Component {
  render() {
    return <ScreenContainer middle={<Profile user={this.props.user} />} />;
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default connect(mapStateToProps)(ProfileScreen);
