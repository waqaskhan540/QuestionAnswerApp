import React from "react";
import { Box, Anchor,Heading } from "grommet";

export const FeaturedTopics = () => (
    <Box gap="small">
        <Heading level={4}>Topics</Heading>
        <Anchor href='/box' label='Box' />
        <Anchor href='/grid' label='Grid' />
        <Anchor href='/layer' label='Layer' />
        <Anchor href='/stack' label='Stack' />
    </Box>
)