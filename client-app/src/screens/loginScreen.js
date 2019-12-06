import React from "react";
import LoginForm from "../components/loginForm";
import { Header } from "semantic-ui-react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { Box, Grid } from "grommet";
import authenticationService from "../services/authenticationService";
import * as userActions from "../actions/userActions";
import FacebookLogin from "react-facebook-login";
import { GoogleLogin } from "react-google-login";
import axios from "axios";
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
  facebookLoginCallback(response) {   
    const { accessToken } = response;
    const { userLoggedIn } = this.props.actions;
    const { history } = this.props;

    axios
      .post("http://localhost:5000/api/auth/external-login", {
        provider: "facebook",
        accessToken: accessToken
      })
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

  responseGoogle(response) {
    
    if(!response)
      return;
    const { accessToken } = response;
    const { userLoggedIn } = this.props.actions;
    const { history } = this.props;

    axios
      .post("http://localhost:5000/api/auth/external-login", {
        provider: "google",
        accessToken: accessToken
      })
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
      <div>
        <Grid
          areas={[
            { name: "nav", start: [0, 0], end: [0, 0] },
            { name: "main", start: [1, 0], end: [1, 0] }
          ]}
          columns={["medium", "medium"]}
          rows={["medium", "small"]}
          gap="small"
          margin="medium"
        >
          <Box gridArea="nav" />

          <Box
            gridArea="main"
            pad="medium"
            elevation="small"
            alignSelf="center"
          >
            <Header as="h3">Login</Header>
            <LoginForm
              submitHandler={this.submitHandler}
              validateForm={this.validateForm}
              error={this.state.error}
            />

            {/* <FacebookLogin
              appId="1170436143158785"
              autoLoad={true}
              fields="name,email,picture"
              callback={this.facebookLoginCallback}
            /> */}
           
            <GoogleLogin
              clientId={"1095144691030-h93b853sljjf31f3pico1g9jjibvjcrc.apps.googleusercontent.com"}
              //scope="https://www.googleapis.com/auth/analytics"
              onSuccess={this.responseGoogle}
              onFailure={this.responseGoogle}
              onRequest={this.responseGoogle}
              offline={false}
              approvalPrompt="force"
              responseType="id_token"
              isSignedIn
              theme="dark"
              // disabled
              // prompt="consent"
              // className='button'
              // style={{ color: 'red' }}
            ></GoogleLogin>
          </Box>
        </Grid>
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
  connect(mapStateToProps, mapDispatchToProps)(LoginScreen)
);
