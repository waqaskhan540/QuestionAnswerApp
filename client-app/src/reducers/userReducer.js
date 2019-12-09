import * as UserActions from "../actionTypes/userActionTypes";

let initialState = {
  firstname: "",
  lastname: "",
  userId: "",
  email: "",
  image: "",
  accessToken: "",
  isAuthenticated: false,
  questions: [],
  myQuestions: [],
  loadingMyQuestions: true,
  loading: true,
  statsUpdating: false,
  savedCount: 0,
  draftCount: 0
};

const user = (state = initialState, action) => {
  switch (action.type) {
    case UserActions.USER_LOGGED_IN:
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
    case UserActions.USER_UPDATED_PROFILE_IMG:
      return {
        ...state,
        image: action.payload
      };
    case UserActions.USER_QUESTIONS_LOADING:
      return {
        ...state,
        loading: action.payload
      };
    case UserActions.USER_QUESTIONS_LOADED:
      return {
        ...state,
        loading: false,
        questions: action.payload
      };
    case UserActions.USER_STATS_UPDATING:
      return {
        ...state,
        statsUpdating: action.payload
      };
    case UserActions.USER_STATS_UPADTED:
      return {
        ...state,
        savedCount: action.payload.savedCount,
        draftCount: action.payload.draftCount
      };
    case UserActions.USER_LOADING_MY_QUESTIONS:
      return {
        ...state,
        loadingMyQuestions: action.payload
      };
    case UserActions.USER_MY_QUESTIONS_LOADED:
      return {
        ...state,
        myQuestions: action.payload
      };
    default:
      return state;
  }
};

export default user;
