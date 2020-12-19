import React from "react";
import { Header, Loader, Segment,Icon } from "semantic-ui-react";
import { Box,Button } from "grommet";

const QuestionDetail = ({
  isLoading,
  answers,
  question,
  isUserAuthenticated
}) => {
  if (isLoading) return <Loader active/>;

  return (
    <div>
      <Header as="h1">{question.questionText}</Header>
      
      {answers.length ? (
        answers.map(ans => (
          <Box
            direction="column"
            pad="medium"            
            onClick={() => console.log("clicked")}
            elevation="small"
            key={question.id}
            alignContent={"start"}
            gap={"small"}
          >
            <p style={{ color: "grey" }}>
              {" "}
              {ans.user.firstName} {ans.user.lastName} {" . "}
              {new Date(ans.dateTime).toLocaleDateString()}
            </p>
            <div dangerouslySetInnerHTML={{ __html: ans.answerMarkup }}></div>
          </Box>
        ))
      ) : (
        <Segment placeholder>
          <Header icon>
            <Icon name="pencil alternate" />
            Question not answered yet.
          </Header>
          <Segment.Inline>
      {isUserAuthenticated && <Button label = "Write Answer" href = {`/write/${question.id}`}   primary color="accent-3" /> }
          </Segment.Inline>
        </Segment>
      )}
    </div>
  );
};

export default QuestionDetail;
