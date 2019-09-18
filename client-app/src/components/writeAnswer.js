import React, {Component} from 'react'
import TextEditor from '../components/textEditor'

class WriteAnswer extends Component {
    render() {
        const {questionId}=this.props;
        return(
            <div>
                <TextEditor/>
            </div>
        )
    }
}


export default WriteAnswer;