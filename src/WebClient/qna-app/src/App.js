
import Login from './features/login';
import Home from './features/home';
import Register from './features/register';

import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

const App = () => {
  return (
    <Router>
      <Switch>
        <Route  path ="/home">
          <Home />
        </Route>
        <Route  path ="/">
          <Home />
        </Route>
        <Route path ="/login">
          <Login />
        </Route>
        <Route path ="/register">
          <Register />
        </Route>
      </Switch>


    </Router>
  )
}

export default App;
