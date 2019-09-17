import React from "react";
import ReactDOM from "react-dom";
import { createStore } from "redux";
import { Provider } from "react-redux";
import AppHeader from "./components/appHeader";
import { BrowserRouter as Router, Route } from "react-router-dom";
import HomeScreen from "./screens/homeScreen";
import LoginScreen from "./screens/loginScreen";
import QuestionScreen from "./screens/questionsScreen";
import RegisterScreen from "./screens/registerScreen";
import QuestionDetailScreen from "./screens/questionDetailScreen";
import WriteAnswerScreen from "./screens/writeAnswerScreen";
import MyQuestionsScreen from './screens/myQuestionsScreen';
import rootReducer from "./reducers";
import {loadState,saveState} from "./helpers/localStorage";

const store = createStore(rootReducer,loadState());
store.subscribe(() => {
  saveState(store.getState())
})

ReactDOM.render(
  <Provider store = {store}>
    <Router>
      <AppHeader />
      <Route exact path="/" render={() => <HomeScreen />} />
      <Route exact path="/home" render={() => <HomeScreen />} />
      <Route path="/questions" render={() => <QuestionScreen />} />
      <Route path="/login" render={(props) => <LoginScreen {...props} />} />
      <Route path="/register" render={() => <RegisterScreen />} />
      <Route path ="/question/:id" render={(props) => <QuestionDetailScreen {...props}/>}/>
      <Route path="/myquestions" render = {() => <MyQuestionsScreen/>} />
      <Route path="/write/:id" render={(props) => <WriteAnswerScreen {...props}/>}/>
    </Router>
  </Provider>,
  document.getElementById("root")
);
