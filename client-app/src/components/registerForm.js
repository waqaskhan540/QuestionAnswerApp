import React, { Component } from "react";
import { Formik } from "formik";
import { withRouter } from "react-router-dom";
import { Form, Button, Message } from "semantic-ui-react";
import authenticationService from "../services/authenticationService";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as userActions from "../actions/userActions";

class RegisterForm extends Component {
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
    const { error } = this.state;

    return (
      <div>
        <Formik
          initialValues={{
            firstname: "",
            lastname: "",
            email: "",
            password: "",
            confirmPassword: ""
          }}
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
                error={
                  errors.firstname && touched.firstname && errors.firstname
                }
                fluid
                label="First Name"
                name="firstname"
                placeholder="First Name"
                onChange={handleChange}
              />
              <Form.Input
                error={errors.lastname && touched.lastname && errors.lastname}
                fluid
                label="Last Name"
                name="lastname"
                placeholder="Last Name"
                onChange={handleChange}
              />
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

              <Form.Input
                error={
                  errors.confirmPassword &&
                  touched.confirmPassword &&
                  errors.confirmPassword
                }
                fluid
                label="Confirm Password"
                name="confirmPassword"
                placeholder="Confirm Password"
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
  return {
    user : state.user
  }
}

const mapDispatchToProps = dispatch => {
  return {
    actions : bindActionCreators(userActions,dispatch)
  }
}
export default withRouter(connect(mapStateToProps,mapDispatchToProps)(RegisterForm));
