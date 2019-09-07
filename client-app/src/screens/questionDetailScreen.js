import React, {Component} from 'react';
import QuestionDetail from '../components/questionDetail';


class QuestionDetailScreen extends Component {

    
    render() {
        const {id}  = this.props.match.params;
        return (
            <QuestionDetail questionId={id}/>            
        )
    }
}

export default QuestionDetailScreen;