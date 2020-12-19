import axios from "axios";
import config from "../config";
import { getHeaders } from "../helpers/authHelper";

class StatsService {
    static GetUserStats = () => {
        return axios.all([
            axios.get(`${config.API_BASE_URL}/api/drafts/count`,{headers : getHeaders()}),
            axios.get(`${config.API_BASE_URL}/api/saved/count`,{headers : getHeaders()})
        ])
    }
}


export default StatsService;