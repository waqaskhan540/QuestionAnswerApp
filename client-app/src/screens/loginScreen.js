import React from "react";
import LoginForm from "../components/loginForm";
import { Header } from "semantic-ui-react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { Box } from "grommet";
import authenticationService from "../services/authenticationService";
import * as userActions from "../actions/userActions";
import FacebookLogin from "react-facebook-login";
import { GoogleLogin } from "react-google-login";
import ScreenContainer from "./../components/common/screenContainer";
import qs from "qs";

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

    authenticationService
      .loginExternal(provider, access_token)
      .then(response => {
        const { user, access_token } = response.data.data;
        const userInfo = {
          firstname: user.firstname,
          lastname: user.lastname,
          email: user.email,
          userId: user.userId,
          image: user.image,
          accessToken: access_token
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

        const { user, access_token } = response.data.data;
        const userInfo = {
          firstname: user.firstname,
          lastname: user.lastname,
          email: user.email,
          userId: user.userId,
          image: user.image,
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
      <ScreenContainer
        left={
          <Box pad="medium" elevation="small" margin="medium" gap="small">
            <Header as="h3">Login</Header>
            <LoginForm
              submitHandler={this.submitHandler}
              validateForm={this.validateForm}
              error={this.state.error}
            />
            <Box pad="small" margin="small" direction="row" gap="small">
              <FacebookLogin
                appId="1170436143158785"
                fields="name,email,picture"
                icon="fa-facebook"
                callback={this.facebookLoginCallback}
              />
              <GoogleLogin
                clientId={
                  "1095144691030-h93b853sljjf31f3pico1g9jjibvjcrc.apps.googleusercontent.com"
                }
                onSuccess={this.responseGoogle}
                onFailure={this.responseGoogle}               
                offline={false}
                approvalPrompt="force"
                responseType="id_token"                
              ></GoogleLogin>
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
