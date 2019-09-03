import React from "react";
import LoginForm from "../components/loginForm";
import { Grid, Header } from "semantic-ui-react";


export default class LoginScreen extends React.Component {
  render() {
    return (
      <div>
        <Grid container columns={3} padded>
          <Grid.Column></Grid.Column>
          <Grid.Column>
            <Header as="h3">Login</Header>
            <hr />
            <LoginForm />
          </Grid.Column>
          <Grid.Column></Grid.Column>
        </Grid>
      </div>
    );
  }
}
