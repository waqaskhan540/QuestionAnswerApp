import React, { Component } from "react";
import { Box, Heading, Text, Anchor } from "grommet";
import  {Avatar}  from  "./common/avatar"
import {Spinning} from "grommet-controls"

const FeaturedQuestions = ({loading, featuredQuestions }) => (
  <Box direction="column" margin="medium" gap="medium">
    <Heading size="3">Featured</Heading>
    {loading && <Spinning/>}
    {!loading && featuredQuestions.map(question => (
        <Box direction="row" key={question.id}>
        <Box align="center">
          <Avatar image={question.user.image}/>
        </Box>
        <Box margin="small">
          <Anchor style={anchorStyle} href={`/question/${question.id}`}>
            {question.questionText}
          </Anchor>
        </Box>
      </Box>
    ))}
  </Box>
);

const anchorStyle = {
  fontSize: "15px",
  fontWeight: "bold"
};
export default FeaturedQuestions;
