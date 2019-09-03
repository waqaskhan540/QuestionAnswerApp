import React from "react";
import ReactDOM from "react-dom";
import { Button, Checkbox, Form, Message } from "semantic-ui-react";
import Axios from "axios";
import { withRouter } from "react-router-dom";

const validEmailRegex = RegExp(
  /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i
);

class RegisterForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      firstname: null,
      lastname: null,
      email: null,
      password: null,
      confirmPassword: null,
      errors: {
        firstname: "",
        lastname: "",
        email: "",
        password: "",
        confirmPassword: ""
      },
      isloading: false,
      errorMessage: "" //error message from server
    };
  }

  handleChange = event => {
    event.preventDefault();
    let { name, value } = event.target;
    let errors = this.state.errors;
    console.log("handle change called");
    switch (name) {
      case "firstname":
        errors.firstname =
          value.length < 5 ? "firstname should have atleast 5 characters" : "";
        break;
      case "lastname":
        errors.lastname =
          value.length < 5 ? "lastname should have atleast 5 characters" : "";
        break;
      case "email":
        errors.email = validEmailRegex.test(value) ? "" : "email is invalid";
        break;
      case "password":
        errors.password =
          value.length < 6 ? "password should be atleast 6 characters" : "";
        break;
      case "confirmpassword":
        errors.confirmPassword =
          value == this.state.password ? "" : "passwords do not match";
        break;
    }

    this.setState({ errors, [name]: value });
  };

  validateForm = errors => {
    let valid = true;
    Object.values(errors).forEach(
      // if we have an error string set valid to false
      val => val.length > 0 && (valid = false)
    );
    return valid;
  };

  handleSubmit = event => {
    event.preventDefault();
    if (this.validateForm(this.state.errors)) {
      let data = {
        firstname: this.state.firstname,
        lastname: this.state.lastname,
        email: this.state.email,
        password: this.state.password
      };
      this.setState({ isloading: true });
      Axios.post("http://localhost:54709/api/auth/register", data)
        .then(response => {
          debugger;
          if (response.data.success) {
            this.setState({ isloading: false });
            this.props.history.push("/");
          } else {
            this.setState({ isloading: false });
            this.setState({ errorMessage: response.data.message });
          }
        })
        .catch(err => {
          debugger;
          this.setState({ isloading: false });
          this.setState({ errorMessage: "something went wrong" });
        });
    } else {
      console.log("invalid form");
    }
  };

  render() {
    const { errors, isloading, errorMessage } = this.state;
    return (
      <Form loading={isloading} error={errorMessage.length > 0}>
        <Form.Input
          error={errors.firstname.length ? errors.firstname : null}
          fluid
          label="First name"
          name="firstname"
          placeholder="First name"
          onChange={this.handleChange}
        />
        <Form.Input
          error={errors.lastname.length ? errors.lastname : null}
          fluid
          label="Last Name"
          name="lastname"
          placeholder="Last Name"
          onChange={this.handleChange}
        />
        <Form.Input
          error={errors.email.length ? errors.email : null}
          fluid
          label="Email"
          name="email"
          placeholder="Email"
          onChange={this.handleChange}
        />
        <Form.Input
          error={errors.password.length ? errors.password : null}
          fluid
          type="password"
          label="Password"
          name="password"
          placeholder="Password"
          onChange={this.handleChange}
        />
        <Form.Input
          error={errors.confirmPassword.length ? errors.confirmPassword : null}
          fluid
          label="Confirm Password"
          type="password"
          name="confirmpassword"
          placeholder="Confirm Password"
          onChange={this.handleChange}
        />
        {errorMessage.length ? (
          <Message error header="Error" content={errorMessage} />
        ):''}
        <Button type="submit" onClick={this.handleSubmit}>
          Submit
        </Button>
      </Form>
    );
  }
}

export default withRouter(RegisterForm);