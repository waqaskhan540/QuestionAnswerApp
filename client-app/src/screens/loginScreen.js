import React from "react";
import LoginForm from "../components/loginForm";
import {  Header } from "semantic-ui-react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import authenticationService from "../services/authenticationService";
import * as userActions from "../actions/userActions";
import qs from "qs";

class LoginScreen extends React.Component {
  state = {
    error: ""
  };

  validateForm = values => {
    let errors = {};
    if (!values.email) {
      errors.email = "Email is required";
    } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)) {
      errors.email = "Invalid email address";
    }

    if (!values.password) {
      errors.password = "Password is required";
    } else if (values.password.length < 6) {
      errors.password = "Password must be atleast 6 characters long.";
    }
    return errors;
  };

  submitHandler = (values, { setSubmitting }) => {
    const { email, password } = values;
    const { returnUrl } = qs.parse(this.props.location.search, {
      ignoreQueryPrefix: true
    });

    authenticationService
      .login(email, password)
      .then(response => {
        setSubmitting(false);

        const { user, access_token } = response.data.data;
        const userInfo = {
          firstname: user.firstname,
          lastname: user.lastname,
          email: user.email,
          userId: user.userId,
          accessToken: access_token
        };

        this.props.actions.userLoggedIn(userInfo);
        if (returnUrl) this.props.history.push(returnUrl);
        else this.props.history.push("/");
      })
      .catch(err => {
        setSubmitting(false);
        console.log(err);
        this.setState({ error: err.response.data.message });
      });
  };
  render() {
   

    return (
      <div>
        {/* <Grid container columns={3} padded>
          <Grid.Column></Grid.Column>
          <Grid.Column> */}
            <Header as="h3">Login</Header>
            <hr />
            <LoginForm
              submitHandler = {this.submitHandler}
              validateForm = {this.validateForm}
              error = {this.state.error}
            />
          {/* </Grid.Column>
          <Grid.Column></Grid.Column>
        </Grid> */}
      </div>
    );
  }
}

const mapStateToProps = state => {
  return { user: state.user };
};
const mapDispatchToProps = dispatch => ({
  actions: bindActionCreators(userActions, dispatch)
});
export default withRouter(
  connect(
    mapStateToProps,
    mapDispatchToProps
  )(LoginScreen)
);