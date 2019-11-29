import axios from "axios";
import config from "../config";
import { getHeaders } from "../helpers/authHelper";

class UserProfileService {
  static uploadImage = data => {
    return axios.post(
      `${config.API_BASE_URL}/api/profile/image`,
      { data },
      { headers: getHeaders() }
    );
  };
}

export default UserProfileService;
