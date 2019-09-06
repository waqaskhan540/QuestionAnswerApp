import React, { Component } from "react";
import { Formik } from "formik";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import {bindActionCreators} from "redux";
import { Form, Button, Message } from "semantic-ui-react";
import authenticationService from "../services/authenticationService";
import * as userActions from "../actions/userActions";



class LoginForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      error: ""
    };
  }

  submitHandler = (values, { setSubmitting }) => {
    const { email, password } = values;
 
    authenticationService
      .login(email, password)
      .then(response => {
        setSubmitting(false);
        localStorage.setItem("access_token", response.data.data.access_token);
        
        const user = {
          firstname: response.data.data.user.firstname,
          lastname: response.data.data.user.lastname,
          email: response.data.data.user.email,
          accessToken: response.data.data.access_token
        };
        
        this.props.actions.userLoggedIn(user)
        this.props.history.push("/");
      })
      .catch(err => {
        setSubmitting(false);  
        console.log(err);      
        this.setState({ error: err.response.data.message });
      });
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

  render() {
    const { error } = this.state;

    return (
      <div>
        <Formik
          initialValues={{ email: "", password: "" }}
          validate={this.validateForm}
          onSubmit={this.submitHandler}
        >
          {({ errors, touched, handleChange, handleSubmit, isSubmitting }) => (
            <Form
              onSubmit={handleSubmit}
              loading={isSubmitting}
              error={error.length > 0}
            >
              <Form.Input
                error={errors.email && touched.email && errors.email}
                fluid
                label="Email"
                name="email"
                placeholder="Email"
                onChange={handleChange}
              />

              <Form.Input
                error={errors.password && touched.password && errors.password}
                fluid
                label="Password"
                name="password"
                placeholder="Password"
                type="password"
                onChange={handleChange}
              />

              {error.length ? (
                <Message error header="Error" content={error} />
              ) : (
                ""
              )}

              <Button type="submit" disabled={isSubmitting}>
                Submit
              </Button>
            </Form>
          )}
        </Formik>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return { user: state.user };
};
const mapDispatchToProps = dispatch => ({
 actions : bindActionCreators(userActions,dispatch)
});
export default withRouter(
  connect(
    mapStateToProps,
    mapDispatchToProps
  )(LoginForm)
);
