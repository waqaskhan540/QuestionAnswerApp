import * as Actions from "./../actionTypes/writeAnswerActionTypes";

let initialState = {
  loadingQuestion: true,
  question: null,
  publishingAnswer: false,
  savingDraft: false
};

const WriteReducer = (state = initialState, action) => {
  switch (action.type) {
    case Actions.LOADING_QUESTION:
      return {
        ...state,
        loadingQuestion: action.payload
      };
    case Actions.QUESTION_LOADED:
      return {
        ...state,
        loadingQuestion: false,      
        question: action.payload
      };
    case Actions.SAVING_DRAFT:
      return {
        ...state,
        savingDraft: action.payload
      };
    case Actions.POSTING_ANSWER:
        return {
            ...state,
            publishingAnswer : action.payload
        }
    default:
      return state;
  }
};

export default WriteReducer;
