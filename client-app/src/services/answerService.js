import axios from "axios";
import config from "../config";
import { getHeaders } from "../helpers/authHelper";

class AnswerService {
  static getAnswersByQuestionId(questionId) {
    return axios.get(`${config.API_BASE_URL}/api/answers/${questionId}`);
  }
}

export default AnswerService;
