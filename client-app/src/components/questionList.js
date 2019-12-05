import React, { Component } from "react";
import { Item, Label, Button } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { Box, Heading } from "grommet";
import SmallButton from "./common/smallButton";
import { withRouter } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import questionService from "./../services/questionsService";

class QuestionsList extends Component {
  saveQuestion(questionId) {
    toast.info("Saving question");
    questionService
      .saveQuestion(questionId)
      .then(resp => {        
        toast.success(resp.data.message);
      })
      .catch(err => toast.error("Something went wrong!"));
  }
  render() {
    const { questions, isUserAuthenticated, history } = this.props;

    return (
      <div>
        {questions.map(question => (
          <Box
            direction="column"
            pad="medium"
            margin="medium"
            onClick={() => console.log("clicked")}
            elevation="small"
            key={question.id}
            alignContent={"start"}
            gap={"small"}
          >
            <Box align="stretch">
              <p style={{ color: "grey" }}>
                {" "}
                {question.user.firstName} {question.user.lastName} {" . "}
                {new Date(question.dateTime).toLocaleDateString()}
              </p>
            </Box>
            <Link to={`question/${question.id}`} style={{ color: "black" }}>
              {/* <Heading margin="none" level="5"> */}
              <h3> {question.questionText}</h3>
              {/* </Heading> */}
            </Link>
            <Box align="start" direction="row">
              <SmallButton
                label={"Answer"}
                onClick={() => history.push(`write/${question.id}`)}
                icon={"write"}
              />
              <SmallButton
                onClick={() => this.saveQuestion(question.id)}
                label={"Save"}
                icon={"save"}
              />
            </Box>
          </Box>
        ))}
      </div>
    );
  }
}

export default withRouter(QuestionsList);
