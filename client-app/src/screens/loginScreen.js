import React from "react";
import LoginForm from "../components/loginForm";
import { Grid, Header } from "semantic-ui-react";
import qs from 'qs';

export default class LoginScreen extends React.Component {
  render() {
    
    const {returnUrl} = qs.parse(this.props.location.search, { ignoreQueryPrefix: true });
    
    return (
      <div>
        <Grid container columns={3} padded>
          <Grid.Column></Grid.Column>
          <Grid.Column>
            <Header as="h3">Login</Header>
            <hr />
            <LoginForm returnUrl={returnUrl}/>
          </Grid.Column>
          <Grid.Column></Grid.Column>
        </Grid>
      </div>
    );
  }
}
