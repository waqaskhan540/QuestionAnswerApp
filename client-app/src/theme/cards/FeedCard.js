import React from "react";
import { Box, ThemeContext, Image, Heading, Text, Anchor, Button } from "grommet"
import { Avatar, Card } from "grommet-controls";
import { Rss, Edit, Save } from "grommet-icons";


export const FeedCard = ({
    avatar,
    username,
    userbio,
    userId,
    questionId,
    questionText,
    onClickFollow,
    onClickAnswer,
    onClickSave,
    footer
}) => (
        <Box gap="small">
            <Box direction="column"  pad="small" width="large" border={{side:"all",size:"xsmall"}}>
                <Box direction="row">
                    <Box direction="row">
                        <Avatar
                            image={avatar}
                            title={<Anchor label={username} href="#" />}
                            subTitle={<Text size="xsmall" color="#777176">{userbio}</Text>}
                        />
                    </Box>
                </Box>

                <Box direction="row" margin={{ top: "small" }}>
                    <Text size="medium">
                        <Anchor href={`/question/${questionId}`} label={questionText} style={{
                            color : "black",
                            fontWeight :"100"
                        }}/>
                    </Text>
                </Box>
                {footer &&
                    <Box direction="row" gap="small" margin={{ top: "small" }}>
                        <Anchor size="xsmall"  label="FOLLOW" href="#" icon={<Rss />} onClick={onClickFollow} />
                        <Anchor size="xsmall" label="ANSWER" href="#" icon={<Edit />} onClick={onClickAnswer} />
                        <Anchor size="xsmall" label="SAVE" href="#" icon={<Save />} onClick={onClickSave} />
                    </Box>
                }
            </Box>
           
        </Box>
    )

