import React, {Component} from 'react'
import questionService from '../services/questionsService'

class QuestionDetail extends Component {

    constructor(props) {
        super(props)

        this.state = {
            isLoading : true,
            question : null
        }
    }

    componentDidMount() {
        const {questionId} = this.props;
        

        questionService.getQuestionById(questionId)
            .then(response => {
               
               
                const questionData = response.data.data;
                this.setState({question:questionData});
                this.setState({isLoading:false});
            })
            .catch(err => this.setState({isLoading:false}))
    }
    render() {
       
        const {isLoading,question} = this.state;
        if(isLoading)
            return <div>Loading question ...</div>

        return (<div>{question.questionText}</div>)
    }
}

export default QuestionDetail;