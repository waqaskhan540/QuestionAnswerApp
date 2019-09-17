import React, {Component} from 'react'

class WriteAnswer extends Component {
    render() {
        const {questionId}=this.props;
        return(
            <div>
                Write Answer For questionId  {questionId}
            </div>
        )
    }
}


export default WriteAnswer;