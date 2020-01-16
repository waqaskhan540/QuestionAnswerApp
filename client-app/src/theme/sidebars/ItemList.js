import React from "react";
import { Box, Anchor,Heading } from "grommet";

export const ItemList = ({align}) => (
    <Box gap="small" align={align}>
        <Heading level={4}>Topics</Heading>
        <Anchor href='#' label='Box' />
        <Anchor href='#' label='Grid' />
        <Anchor href='#' label='Layer' />
        <Anchor href='#' label='Stack' />
    </Box>
)