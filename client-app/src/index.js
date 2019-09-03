import React from "react";
import ReactDOM from "react-dom";
import AppHeader from "./components/appHeader";
import { BrowserRouter as Router, Route } from "react-router-dom";
import HomeScreen from "./screens/homeScreen";
import LoginScreen from "./screens/loginScreen";
import QuestionScreen from "./screens/questionsScreen";
import RegisterScreen from "./screens/registerScreen";

ReactDOM.render(
    <Router>
        <AppHeader/>
        <Route exact path="/" render={() => <HomeScreen/>} />
        <Route exact path="/home" render={() => <HomeScreen/>} />
        <Route path="/questions" render={() => <QuestionScreen/>} />
        <Route path="/login" render={() => <LoginScreen/>} />
        <Route path="/register" render={() => <RegisterScreen/>} />
      </Router>,
  document.getElementById("root")
);
