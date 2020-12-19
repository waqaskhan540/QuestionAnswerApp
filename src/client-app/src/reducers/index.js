import {combineReducers} from "redux";
import user from "./userReducer";
import questionDetail from "./questionDetailReducer";
import writeAnswer from "./writeAnswerReducer";
import feed from "./feedReducer";

export default combineReducers({
    user,
    questionDetail,
    writeAnswer,
    feed
})