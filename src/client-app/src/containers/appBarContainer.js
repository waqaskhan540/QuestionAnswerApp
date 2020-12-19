import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import AppBar from '../components/appBar';

class AppHeaderContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {    
      modalOpened: false
    };
  }

  toggleModal = () => {
    const modalStatus = this.state.modalOpened;
    this.setState({ modalOpened: !modalStatus });
  };

  render() {
      const {user} = this.props;      
      const {modalOpened} = this.state;
      return (
          <AppBar
            user = {user}          
            modalOpened = {modalOpened}
            toggleModal = {this.toggleModal}           
            history = {this.props.history}
          />
      )
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};
export default withRouter(connect(mapStateToProps)(AppHeaderContainer));
