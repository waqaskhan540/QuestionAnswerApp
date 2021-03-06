import * as UserActions from "../actionTypes/userActionTypes"

export const userLoggedIn = (user) => {
    
    return {
        type : UserActions.USER_LOGGED_IN,
        payload : user
    }
}

export const userUpdatedProfileImg = (img) => {
    return {
        type : UserActions.USER_UPDATED_PROFILE_IMG,
        payload : img
    }
}

export const userQuestionsLoading = (loadingStatus) => {
    return {
        type: UserActions.USER_QUESTIONS_LOADING,
        payload : loadingStatus
    }
}

export const userQuestionsLoaded = (questions) => {
    return {
        type: UserActions.USER_QUESTIONS_LOADED,
        payload : questions
    }
}

export const userStatsUpdating = (updateStatus) => {
    return {
        type : UserActions.USER_STATS_UPDATING,
        payload : updateStatus
    }
}

export const userStatsUpdated = (stats) => {
    return {
        type : UserActions.USER_STATS_UPADTED,
        payload : stats
    }
}

export const userMyQuestionsLoading = (loadingStatus) => {
    return {
        type : UserActions.USER_LOADING_MY_QUESTIONS,
        payload : loadingStatus
    }
}

export const userMyQuestionsLoaded = (myQuestions) => {
    return {
        type : UserActions.USER_MY_QUESTIONS_LOADED,
        payload : myQuestions
    }
}

export const userUpdateQuestions = (question) => {
    return {
        type : UserActions.USER_UPDATE_QUESTIONS,
        payload : question
    }
}

export const userResetPage = () => {
    return {type : UserActions.USER_RESET_PAGE}
}

export const userFollowQuestion = (questionId) => {
    return {type : UserActions.USER_FOLLOW_QUESTION, payload : questionId}
}

export const userUnFollowQuestion = (questionId) => {
    return {type : UserActions.USER_UNFOLLOW_QUESTION, payload : questionId}
}

export const userSavedQuestion = (questionId) => {
    return {type : UserActions.USER_SAVED_QUESTION, payload : questionId}
}

export const userUnSavedQuestion = (questionId) => {
    return {type : UserActions.USER_UNSAVED_QUESTION, payload : questionId};
}