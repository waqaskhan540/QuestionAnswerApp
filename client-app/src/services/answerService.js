import axios from "axios";
import config from "../config";
import { getHeaders } from "../helpers/authHelper";

class AnswerService {
  static getAnswersByQuestionId(questionId) {
    return axios.get(`${config.API_BASE_URL}/api/answers/${questionId}`);
  }

  static postAnswer(questionId,answer) {
    return axios.post(`${config.API_BASE_URL}/api/answer`,{questionId,answer},{
      headers : getHeaders()
    })
  }
}

export default AnswerService;
