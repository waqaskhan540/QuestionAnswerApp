import {combineReducers} from "redux";
import user from "./userReducer";
import questionDetail from "./questionDetailReducer";
import writeAnswer from "./writeAnswerReducer";

export default combineReducers({
    user,
    questionDetail,
    writeAnswer
})