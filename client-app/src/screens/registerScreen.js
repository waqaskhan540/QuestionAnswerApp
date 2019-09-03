import React from "react";
import RegisterForm from "../components/registerForm";
import { Grid, Header } from "semantic-ui-react";

export default class RegisterScreen extends React.Component {
  render() {
    return (
      <div>
        <Grid container columns={3} padded>
          <Grid.Column></Grid.Column>
          <Grid.Column>
            <Header as="h3">Register</Header>
            <hr />
            <RegisterForm />
          </Grid.Column>
          <Grid.Column></Grid.Column>
        </Grid>
      </div>
    );
  }
}
