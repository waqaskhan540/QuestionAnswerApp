import axios from "axios";
import config from "../config";
import {getHeaders} from "../helpers/authHelper";

class QuestionsService {
  static getLatestQuestions = () => {
    return axios.get(`${config.API_BASE_URL}/api/questions`);
  };

  static postQuestion = (que) => {
    return axios.post(`${config.API_BASE_URL}/api/questions`,que,{
      headers : getHeaders()
    })
  }
}

export default QuestionsService;
