import React, { Component } from "react";
import { Button, Checkbox, Form } from "semantic-ui-react";

class LoginForm extends Component {
  render() {
    return (
      <Form>
        <Form.Field>
          <label>Email</label>
          <input placeholder="Email" />
        </Form.Field>
        <Form.Field>
          <label>Password</label>
          <input type="password" placeholder="Password" />
        </Form.Field>

        <Button type="submit">Submit</Button>
      </Form>
    );
  }
}

export default LoginForm;
