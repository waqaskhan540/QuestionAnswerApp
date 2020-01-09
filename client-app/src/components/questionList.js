import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Box, Anchor } from "grommet";
import SmallButton from "./common/smallButton";
import { withRouter } from "react-router-dom";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import questionService from "./../services/questionsService";
import { Button } from "grommet";
import { Download, Save, Edit, Rss } from "grommet-icons";
import { Avatar } from "./common/avatar";
import "./styles/questionList.css";

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
    const {
      questions,
      isUserAuthenticated,
      questionsFollowing,
      onFollow,
      onUnFollow
    } = this.props;

    return (
      <div>
        {questions.map(question => (
          <Box
            direction="column"
            pad="small"
            margin="medium"
            elevation="xsmall"
            key={question.id}
            background="light-3"
            round="small"
            alignContent={"start"}
            gap={"small"}
          >
            <Box direction="row" gap="small">
              <Avatar image={question.user.image} />
              <Box>
                <p style={{ color: "grey" }}>
                  {" "}
                  {question.user.firstName} {question.user.lastName} {" . "}
                  {new Date(question.dateTime).toLocaleDateString()}
                </p>
                <Link to={`question/${question.id}`} style={{ color: "black" }}>
                  <h3> {question.questionText}</h3>
                </Link>
              </Box>
            </Box>

            {isUserAuthenticated && (
              <Box direction="row">
                <Box
                  align="start"
                  direction="row"
                  gap="small"
                  fill
                  margin="small"
                >
                  <Anchor
                    label="Answer"
                    icon={<Edit />}
                    href={`/write/${question.id}`}
                  />
                  <Anchor
                    label="Save"
                    icon={<Save />}
                    onClick={() => this.saveQuestion(question.id)}
                  />

                  {questionsFollowing.includes(question.id) ? (
                    <Anchor
                      label="UnFollow"
                      icon={<Rss />}
                      onClick={() => onUnFollow(question.id)}
                    />
                  ) : (
                    <Anchor
                      label="Follow"
                      icon={<Rss />}
                      onClick={() => onFollow(question.id)}
                    />
                  )}
                </Box>
              </Box>
            )}
          </Box>
        ))}

        {questions.length && (
          <Box fill>
            <Button
              icon={<Download />}
              label="Load More"
              onClick={this.props.onloadMore}
            />
          </Box>
        )}
      </div>
    );
  }
}

export default withRouter(QuestionsList);
