import React, { Component } from "react";
import TextEditor from "../components/textEditor";
import AnswerService from "../services/answerService";

class WriteAnswer extends Component {

  render() {
    return (
      <div>
        <TextEditor   />
      </div>
    );
  }
}

export default WriteAnswer;
