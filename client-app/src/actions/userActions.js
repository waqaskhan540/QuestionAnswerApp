import {USER_LOGGED_IN,USER_UPDATED_PROFILE_IMG} from "../actionTypes/userActionTypes"

export const userLoggedIn = (user) => {
    
    return {
        type : USER_LOGGED_IN,
        payload : user
    }
}

export const userUpdatedProfileImg = (img) => {
    return {
        type : USER_UPDATED_PROFILE_IMG,
        payload : img
    }
}