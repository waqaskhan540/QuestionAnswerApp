import * as Actions from "./../actionTypes/writeAnswerActionTypes"

export const loadingQuestion = (loadingStatus) => {
    return {
        type : Actions.LOADING_QUESTION,
        payload : loadingStatus
    }
}

export const questionLoaded = (question) => {
    return {
        type : Actions.QUESTION_LOADED,
        payload : question
    }
}

export const savingDraft = (status) => {
    return {
        type : Actions.SAVING_DRAFT,
        payload : status
    }
}

export const savingAnswer = (status) => {
    return {
        type : Actions.POSTING_ANSWER,
        payload : status
    }
}