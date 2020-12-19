import * as QuestionDetailActions from "./../actionTypes/questionDetailActionTypes"

let initialState = {
    isloading: true,
    question: null,
    answers: []
}

const QuestionDetail = (state = initialState, action) => {
    switch(action.type) {
        case QuestionDetailActions.IS_LOADING:
            return {
                ...state,
                isloading : action.payload
            }
        case QuestionDetailActions.QUESTION_LOADED:
            return {
                ...state,
                question : action.payload
            }
        case QuestionDetailActions.ANSWERS_LOADED:
            return {
                ...state,
                answers : action.payload
            }
        default:
            return state;
    }
}

export default QuestionDetail;