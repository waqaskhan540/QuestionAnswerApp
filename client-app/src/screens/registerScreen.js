import React from "react";
import RegisterForm from "../components/registerForm";
import {  Header } from "semantic-ui-react";
import { withRouter } from "react-router-dom";
import authenticationService from "../services/authenticationService";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as userActions from "../actions/userActions";

class RegisterScreen extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      error: "" //error from server
    };
  }

  validateForm = values => {
    let errors = {};

    if (!values.firstname) {
      errors.firstname = "first name is required.";
    }

    if (!values.lastname) {
      errors.lastname = "last name is required.";
    }

    if (!values.email) {
      errors.email = "email is required";
    } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)) {
      errors.email = "invalid email address";
    }

    if (!values.password) {
      errors.password = "password is required";
    } else if (values.password.length < 6) {
      errors.password = "password must be atleast 6 characters long.";
    }

    if (values.password !== values.confirmPassword) {
      errors.confirmPassword = "passwords do not match";
    }
    return errors;
  };

  submitHandler = (values, { setSubmitting }) => {
    authenticationService
      .register(values)
      .then(response => {
        setSubmitting(false);
        const { user, access_token } = response.data.data;
        const userInfo = {
          firstname: user.firstname,
          lastname: user.lastname,
          email: user.email,
          accessToken: access_token
        };

        this.props.actions.userLoggedIn(userInfo);
        this.props.history.push("/");
      })
      .catch(err => {
        setSubmitting(false);
        this.setState({ error: err.response.data.message });
      });
  };

  render() {
    return (
      <div>
        {/* <Grid container columns={3} padded>
          <Grid.Column></Grid.Column>
          <Grid.Column> */}
            <Header as="h3">Register</Header>
            <hr />
            <RegisterForm
              error={this.state.error}
              validateForm={this.validateForm}
              submitHandler={this.submitHandler}
            />
          {/* </Grid.Column>
          <Grid.Column></Grid.Column>
        </Grid> */}
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.user
  };
};

const mapDispatchToProps = dispatch => {
  return {
    actions: bindActionCreators(userActions, dispatch)
  };
};
export default withRouter(
  connect(
    mapStateToProps,
    mapDispatchToProps
  )(RegisterScreen)
);
