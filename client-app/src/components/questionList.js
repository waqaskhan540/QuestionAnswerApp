import React, { Component } from "react";
import { Item, Label ,Button} from "semantic-ui-react";
import { Link } from "react-router-dom";
import { Box, Heading } from "grommet";

class QuestionsList extends Component {
  render() {
    const { questions, isUserAuthenticated } = this.props;
    console.log(questions);
    return (
      <div>
        {questions.map(question => (
          <Box
            direction="column"
            pad="medium"
            border={"small"}
            margin="medium"
            hoverIndicator={true}
            onClick={() => console.log("clicked")}
            elevation="small"
            key={question.id}            
          >
            <Box align="stretch">
              {question.user.firstName} {question.user.lastName} {" . "}
              {new Date(question.dateTime).toLocaleDateString()}
            </Box>
            <Link to ={`question/${question.id}`}>
              {/* <Heading margin="none" level="5"> */}
              <h3> {question.questionText}</h3>
              {/* </Heading> */}
            </Link>
           
          </Box>
        ))}
      </div>
    );
  }
}

export default QuestionsList;
