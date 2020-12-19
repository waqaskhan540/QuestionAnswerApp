import * as QuestionDetailActions from "./../actionTypes/questionDetailActionTypes";

export const isLoading = loadingStatus => {
  return {
    type: QuestionDetailActions.IS_LOADING,
    payload: loadingStatus
  };
};

export const questionLoaded = question => {
  return {
    type: QuestionDetailActions.QUESTION_LOADED,
    payload: question
  };
};

export const answersLoaded = answers => {
  return {
    type: QuestionDetailActions.ANSWERS_LOADED,
    payload: answers
  };
};
