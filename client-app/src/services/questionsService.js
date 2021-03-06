import axios from "axios";
import config from "../config";
import { getHeaders } from "../helpers/authHelper";

class QuestionsService {

  static getFeedData = (pageNum = 1) => {
    return axios.get(`${config.API_BASE_URL}/api/feed/${pageNum}`)
  }
  static getLatestQuestions = () => {
    return axios.get(`${config.API_BASE_URL}/api/questions`);
  };

  static postQuestion = que => {
    return axios.post(`${config.API_BASE_URL}/api/questions`, que, {
      headers: getHeaders()
    });
  };

  static getQuestionById = queId => {
    return axios.get(`${config.API_BASE_URL}/api/questions/${queId}`);
  };

  static getMyQuestions = (userId) => {
    return axios.get(`${config.API_BASE_URL}/api/questions/user/${userId}`, {
      headers: getHeaders()
    });
  };

  static saveQuestion = (questionId) => {
    return axios.post(`${config.API_BASE_URL}/api/save/${questionId}`,{}, {
      headers : getHeaders()
    })
  }

  static unSaveQuestion = (questionId) => {
    return axios.post(`${config.API_BASE_URL}/api/unsave/${questionId}`,{}, {
      headers : getHeaders()
    })
  }

  static followQuestion = (questionId) => {
    return axios.post(`${config.API_BASE_URL}/api/question/follow/${questionId}`,{}, {
      headers : getHeaders()
    })
  }

  static unFollowQuestion = (questionId) => {
    return axios.post(`${config.API_BASE_URL}/api/question/unfollow/${questionId}`,{}, {
      headers : getHeaders()
    })
  }

  static getFeaturedQuestions = () => {
    return axios.get(`${config.API_BASE_URL}/api/questions/featured`)
  }
}

export default QuestionsService;
