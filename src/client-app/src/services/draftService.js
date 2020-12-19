import axios from "axios";
import config from "../config";
import { getHeaders } from "../helpers/authHelper";

class DraftService {
  static saveDraft = data => {
    return axios.post(`${config.API_BASE_URL}/api/drafts`, data, {
      headers: getHeaders()
    });
  };

  static getDraftsCount = () => {
    return axios.get(`${config.API_BASE_URL}/api/drafts/count`, {
      headers : getHeaders()
    })
  }
}

export default DraftService;
