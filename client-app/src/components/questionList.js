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
import { FeedCard } from "../theme/cards/FeedCard";
import { LoadingButton } from "../theme/buttons/LoadingButton";

class QuestionsList extends Component {
  render() {
    const {
      questions,
      isUserAuthenticated,
      questionsFollowing,
      questionsSaved,
      onFollow,
      onUnFollow,
      onSave,
      onUnSave,
      toggleLoadMore,
      onloadMore,
      loadingMore
    } = this.props;

    return (
      <div>
        {questions.map(question => (
          <FeedCard
            key={`feed-${question.id}`}
            avatar={question.user.image}
            username={`${question.user.firstName} ${question.user.lastName}`}
            userbio={"Software Engineer at Nova"}
            questionId={question.id}
            questionText={question.questionText}
            footer={isUserAuthenticated}
            isFollowing={
              isUserAuthenticated &&
              questionsFollowing.length > 0 &&
              questionsFollowing.includes(question.id)
            }
            isSaved={
              isUserAuthenticated &&
              questionsSaved.length > 0 &&
              questionsSaved.includes(question.id)
            }
            onClickFollow={() => onFollow(question.id)}
            onClickUnFollow={() => onUnFollow(question.id)}
            onClickSave={() => onSave(question.id)}
            onClickUnSave={() => onUnSave(question.id)}
          />
        ))}

        {questions.length && toggleLoadMore ? (
          <Box fill>
            <LoadingButton
              label={"Load more"}
              icon={<Download size="small" />}
              onClick={onloadMore}
              loading={loadingMore}
            />
          </Box>
        ) : (
          ""
        )}
      </div>
    );
  }
}

export default withRouter(QuestionsList);
