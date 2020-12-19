import React from "react";
import {
  Box,
  ThemeContext,
  Image,
  Heading,
  Text,
  Anchor,
  Button
} from "grommet";
import { Avatar, Card } from "grommet-controls";
import { Rss, Edit, Save } from "grommet-icons";

export const FeedCard = ({
  avatar,
  username,
  userbio,
  userId,
  questionId,
  questionText,
  footer,
  isFollowing,
  isSaved,

  onClickFollow,
  onClickSave,
  onClickUnFollow,
  onClickUnSave,
  ...others
}) => (
  <Box gap="small" margin="small" {...others}>
    <Box
      direction="column"
      pad="small"      
      border={{ side: "all", size: "xsmall" }}
    >
      <Box direction="row">
        <Box direction="row">
          <Avatar
            image={avatar}
            title={<Anchor label={username} href="#" />}
            subTitle={
              <Text size="xsmall" color="#777176">
                {userbio}
              </Text>
            }
          />
        </Box>
      </Box>

      <Box direction="row" margin={{ top: "small" }}>
        <Text size="medium">
          <Anchor
            href={`/question/${questionId}`}
            label={questionText}
            style={{
              color: "black",
              fontWeight: "100"
            }}
          />
        </Text>
      </Box>
      {footer && (
        <Box direction="row" gap="small" margin={{ top: "small" }}>
          {!isFollowing && (
            <Anchor
              size="xsmall"
              label={"FOLLOW"}
              href="#"
              icon={<Rss />}
              onClick={onClickFollow}
            />
          )}
          {isFollowing && (
            <Anchor
              size="xsmall"
              label={"UNFOLLOW"}
              href="#"
              icon={<Rss />}
              onClick={onClickUnFollow}
            />
          )}

          {!isSaved && (
            <Anchor
              size="xsmall"
              label={"SAVE"}
              href="#"
              icon={<Save />}
              onClick={onClickSave}
            />
          )}
          {isSaved && (
            <Anchor
              size="xsmall"
              label={"UNSAVE"}
              href="#"
              icon={<Save />}
              onClick={onClickUnSave}
            />
          )}

          <Anchor
            size="xsmall"
            label="ANSWER"
            href={`write/${questionId}`}
            icon={<Edit />}
          />
        </Box>
      )}
    </Box>
  </Box>
);
