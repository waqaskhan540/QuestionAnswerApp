import {USER_LOGGED_IN} from "../actionTypes/userActionTypes"

let initialState = {
    firstname:'',
    lastname:'',
    userId:'',
    email:'',
    accessToken :'',
    isAuthenticated : false
}

const user = (state = initialState, action) => {
    
    switch(action.type) {
        case USER_LOGGED_IN: 
       
            return {
                ...state,
                firstname:action.payload.firstname,
                lastname:action.payload.lastname,
                accessToken:action.payload.accessToken,
                email : action.payload.email,
                userId : action.payload.userId,
                isAuthenticated : true
            }
        default:
            return state;
    }
}

export default user;