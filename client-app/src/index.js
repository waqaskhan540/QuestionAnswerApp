import React from "react";
import ReactDOM from "react-dom";
import { Box, Button,Heading, Grommet } from "grommet";
import { createStore } from "redux";
import { Provider } from "react-redux";
import AppHeader from "./containers/appHeaderContainer";
import { BrowserRouter as Router, Route } from "react-router-dom";
import HomeScreen from "./screens/homeScreen";
import LoginScreen from "./screens/loginScreen";
import RegisterScreen from "./screens/registerScreen";
import QuestionDetailScreen from "./screens/questionDetailScreen";
import WriteAnswerScreen from "./screens/writeAnswerScreen";
import MyQuestionsScreen from "./screens/myQuestionsScreen";
import rootReducer from "./reducers";
import { loadState, saveState } from "./helpers/localStorage";

const store = createStore(rootReducer, loadState());
store.subscribe(() => {
  saveState(store.getState());
});

const AppBar = props => (
  <Box
    tag="header"
    direction="row"
    align="start"
    justify="start"
    pad={{ vertical: "small", horizontal: "medium" }}
    {...props}
  />
);
ReactDOM.render(
  <Grommet plain>
    <Provider store={store}>
      <Router>
        <AppBar>
          <Heading level={3}>QnA</Heading>
          <Button  label="Home" onClick={() => {}} style= {{marginLeft:'20px'}} />
          <Button  label="Questions" onClick={() => {}} style= {{marginLeft:'20px'}} />
          <Button  label="My Questions" onClick={() => {}} style= {{marginLeft:'20px'}} />
          
        </AppBar>

        <Route exact path="/" render={() => <HomeScreen />} />
        <Route exact path="/home" render={() => <HomeScreen />} />
        <Route path="/login" render={props => <LoginScreen {...props} />} />
        <Route path="/register" render={() => <RegisterScreen />} />
        <Route
          path="/question/:id"
          render={props => <QuestionDetailScreen {...props} />}
        />
        <Route path="/myquestions" render={() => <MyQuestionsScreen />} />
        <Route
          path="/write/:id"
          render={props => <WriteAnswerScreen {...props} />}
        />
      </Router>
    </Provider>
  </Grommet>,
  document.getElementById("root")
);
