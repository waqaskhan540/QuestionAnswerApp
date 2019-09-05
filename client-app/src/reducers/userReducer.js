import {USER_LOGGED_IN} from "../actionTypes/userActionTypes"

let initialState = {
    firstname:'',
    lastname:'',
    email:'',
    accessToken :'',
    isAuthenticated : false
}

const user = (state = initialState, action) => {
    switch(action) {
        case USER_LOGGED_IN: 
        debugger;
            return {
                ...state,
                firstname:action.payload.firstname,
                lastname:action.payload.lastname,
                accessToken:action.payload.accessToken,
                email : action.payload.email,
                isAuthenticated : true
            }
        default:
            return state;
    }
}

export default user;