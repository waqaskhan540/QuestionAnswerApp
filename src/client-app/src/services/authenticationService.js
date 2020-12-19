import axios from "axios"
import config from "../config"

class AuthenticationService {
    static login = (email,password) => {
        return axios.post(`${config.API_BASE_URL}/api/auth/login`,{email,password} )
    }

    static register = (data) => {
        return axios.post(`${config.API_BASE_URL}/api/auth/register`,{...data} )
    }

    static loginExternal = (provider, accessToken) => {
        return axios.post(`${config.API_BASE_URL}/api/auth/external-login`,{provider,accessToken})
    }
}

export default AuthenticationService;