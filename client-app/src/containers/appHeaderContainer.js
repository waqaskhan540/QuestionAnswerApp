import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import AppHeader from '../components/appHeader';

class AppHeaderContainer extends Component {
  constructor(props) {
    super(props);
    this.state = {
      activeItem: "home",
      modalOpened: false
    };
  }

  handleItemClick = (e, { name }) => {
    this.setState({ activeItem: name });
    this.props.history.push("/" + name);
  };

  toggleModal = () => {
    const modalStatus = this.state.modalOpened;
    this.setState({ modalOpened: !modalStatus });
  };

  render() {
      const {user} = this.props;      
      const {activeItem,modalOpened} = this.state;
      return (
          <AppHeader
            user = {user}
            activeItem = {activeItem}
            modalOpened = {modalOpened}
            toggleModal = {this.toggleModal}
            handleItemClick = {this.handleItemClick}
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
