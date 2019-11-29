import {
  USER_LOGGED_IN,
  USER_UPDATED_PROFILE_IMG
} from "../actionTypes/userActionTypes";

let initialState = {
  firstname: "",
  lastname: "",
  userId: "",
  email: "",
  image: "",
  accessToken: "",
  isAuthenticated: false
};

const user = (state = initialState, action) => {
  switch (action.type) {
    case USER_LOGGED_IN:
      return {
        ...state,
        firstname: action.payload.firstname,
        lastname: action.payload.lastname,
        accessToken: action.payload.accessToken,
        email: action.payload.email,
        userId: action.payload.userId,
        image: action.payload.image,
        isAuthenticated: true
      };
    case USER_UPDATED_PROFILE_IMG:
      return {
        ...state,
        image: action.payload
      };
    default:
      return state;
  }
};

export default user;
