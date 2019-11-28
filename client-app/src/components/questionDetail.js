import React from "react";
import { Header, Divider, Container, Segment, Button,Icon } from "semantic-ui-react";
import { Link } from "react-router-dom";
import { Box } from "grommet";

const QuestionDetail = ({
  isLoading,
  answers,
  question,
  isUserAuthenticated
}) => {
  if (isLoading) return <div>Loading ...</div>;

  return (
    <Container>
      <Header as="h1">{question.questionText}</Header>
      {/* <span>{question.user.firstName}&nbsp;</span>
      <span>{question.user.lastName}</span> - &nbsp;
      <span>{new Date(question.dateTime).toLocaleDateString()}</span> */}
      {/* <div>
        {isUserAuthenticated ? (
          <Link to={`/write/${question.id}`}>
            <Button content="Write an Answer" basic />
          </Link>
        ) : (
          ""
        )}
      </div> */}
      {/* <Divider /> */}
      {answers.length ? (
        answers.map(ans => (
          <Box
            direction="column"
            pad="medium"
            hoverIndicator={true}
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
            <Button primary>Write Answer</Button>            
          </Segment.Inline>
        </Segment>
      )}
    </Container>
  );
};

export default QuestionDetail;
