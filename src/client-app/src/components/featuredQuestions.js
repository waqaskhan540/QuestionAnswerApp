import React, { Component } from "react";
import { Box, Heading, Text, Anchor } from "grommet";
import  {Avatar}  from  "./common/avatar"
import {Spinning} from "grommet-controls"

const FeaturedQuestions = ({loading, featuredQuestions }) => (
  <Box direction="column"  gap="medium" width="medium" margin={{top:"60px"}}>
    <Heading size="3">Featured</Heading>
    {loading && <Spinning/>}
    {!loading && featuredQuestions.map(question => (
        <Box direction="row" key={question.id}>
        <Box align="center" width="small">
          <Avatar image={question.user.image}/>
        </Box>
        <Box>
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
