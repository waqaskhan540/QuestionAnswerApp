import React from "react";
import LoginForm from "../components/loginForm";
import { Header } from "semantic-ui-react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { Box, Heading, Anchor, Button } from "grommet";
import authenticationService from "../services/authenticationService";
import * as userActions from "../actions/userActions";
import FacebookLogin from "react-facebook-login/dist/facebook-login-render-props";
import { GoogleLogin } from "react-google-login";
import ScreenContainer from "./../components/common/screenContainer";
import qs from "qs";

import AccountScreenContainer from "./../components/common/accountScreenContainer";
import { FacebookOption, Google } from "grommet-icons";

class LoginScreen extends React.Component {
  state = {
    error: ""
  };

  constructor(props) {
    super(props);
    this.facebookLoginCallback = this.facebookLoginCallback.bind(this);
    this.responseGoogle = this.responseGoogle.bind(this);
  }

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

  facebookLoginCallback(response) {
    const { accessToken } = response;
    this.loginExternal("facebook", accessToken);
  }
  responseGoogle(response) {
    if (!response) return;
    const { accessToken } = response;
    this.loginExternal("google", accessToken);
  }
  loginExternal(provider, access_token) {
    const { userLoggedIn } = this.props.actions;
    const { history } = this.props;
    debugger;
    authenticationService
      .loginExternal(provider, access_token)
      .then(response => {
        const { user, accessToken } = response.data.data;
        const userInfo = {
          firstname: user.firstName,
          lastname: user.lastName,
          email: user.email,
          userId: user.userId,
          image: user.image,
          accessToken: accessToken
        };

        userLoggedIn(userInfo);
        //if (returnUrl) this.props.history.push(returnUrl);
        history.push("/");
      })
      .catch(err => console.log(err));
  }

  submitHandler = (values, { setSubmitting }) => {
    const { email, password } = values;
    const { returnUrl } = qs.parse(this.props.location.search, {
      ignoreQueryPrefix: true
    });

    authenticationService
      .login(email, password)
      .then(response => {
        setSubmitting(false);

        
        const { user, accessToken } = response.data.data;
        const userInfo = {
          firstname: user.firstName,
          lastname: user.lastName,
          email: user.email,
          userId: user.userId,
          image: user.image,
          accessToken: accessToken
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
      <AccountScreenContainer
        form={
          <Box
            pad="medium"
            gap="small"
            width="medium"
            elevation="small"
            background="brand"
            alignSelf="center"
          >
            <Box align="center" margin="medium">
              <Heading level={1} style={{ fontFamily: "Pacifico" }}>
                QnA
              </Heading>
            </Box>
            <LoginForm
              submitHandler={this.submitHandler}
              validateForm={this.validateForm}
              error={this.state.error}
            />
            <Box align="center">
              Don't have an account yet?
              <Anchor href="/register">Sign Up</Anchor>
            </Box>
            <Box pad="small" margin="small" direction="column" gap="small">
              <FacebookLogin
                appId="1170436143158785"
                fields="name,email,picture"
                icon="fa-facebook"
                render={renderProps => (
                  <Button
                    onClick={renderProps.onClick}
                    label="Login with Facebook"
                    background="neutral-2"
                    primary
                    icon={<FacebookOption />}
                  />
                )}
                callback={this.facebookLoginCallback}
              />
              <GoogleLogin
                clientId={
                  "1095144691030-h93b853sljjf31f3pico1g9jjibvjcrc.apps.googleusercontent.com"
                }
                render={renderProps => (
                  <Button
                    onClick={renderProps.onClick}
                    label="Login with Google"
                    background="neutral-2"
                    primary
                    icon={<Google />}
                  />
                )}
                onSuccess={this.responseGoogle}
                onFailure={this.responseGoogle}
                offline={false}
                approvalPrompt="force"
                responseType="id_token"
              />
            </Box>
          </Box>
        }
      />
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
  connect(mapStateToProps, mapDispatchToProps)(LoginScreen)
);
