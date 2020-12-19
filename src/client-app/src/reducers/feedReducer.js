import * as FeedActions from "./../actionTypes/feedActionTypes"

let initialState = {
    questions : [],
    loading : false,
    page : 2,
    postingToFeed : false    
}

const feedReducer = (state = initialState , action) => {

    switch(action.type) {
        
        case FeedActions.RESET_PAGE:
            debugger;
            return {
                ...state,
                questions : [],
                page : 2
            }
        case FeedActions.LOAD_QUESTIONS_FIRST_TIME:
            return {
                ...state,
                page : 2,
                questions : action.payload
            }
        case FeedActions.UPDATE_QUESTIONS:
            debugger;
            return {
                ...state,
                page : state.page + 1,
                questions : [...state.questions,...action.payload]
            }
        case FeedActions.IS_FEED_LOADING:
            return {
                ...state,
                loading : action.payload
            }
        case FeedActions.POSTING_QUESTION_TO_FEED:
            return {
                ...state,
                postingToFeed : true
            }
        case FeedActions.QUESTION_POSTED_TO_FEED:
            return {
                ...state,
                questions: [action.payload,...state.questions],
                postingToFeed : false
            }
        default:
            return state;

    }
}

export default feedReducer;