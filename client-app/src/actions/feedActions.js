import * as FeedActions from "../actionTypes/feedActionTypes";

export const updateQuestions = questions => {
  return {
    type: FeedActions.UPDATE_QUESTIONS,
    payload: questions
  };
};

export const resetPage = () => ({ type: FeedActions.RESET_PAGE });

export const isFeedLoading = isLoading => ({
  type: FeedActions.IS_FEED_LOADING,
  payload: isLoading
});

export const postingToFeed = () => {
  return {
    type: FeedActions.POSTING_QUESTION_TO_FEED
  };
};

export const postedToFeed = question => {
  return {
    type: FeedActions.QUESTION_POSTED_TO_FEED,
    payload: question
  };
};

export const loadQuestionsFirstTime = questions => {
  return {
    type: FeedActions.LOAD_QUESTIONS_FIRST_TIME,
    payload: questions
  };
};
