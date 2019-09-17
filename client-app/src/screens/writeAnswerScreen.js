import React,{Component} from 'react'
import WriteAnswer from '../components/writeAnswer'

class WriteAnswerScreen extends Component {
    render() {
        const {id}  = this.props.match.params;
        return <WriteAnswer questionId={id}/>

    }
}

export default WriteAnswerScreen;