import {USER_LOGGED_IN} from "../actionTypes/userActionTypes"

export const userLoggedIn = (user) => {
    
    return {
        type : USER_LOGGED_IN,
        payload : user
    }
}