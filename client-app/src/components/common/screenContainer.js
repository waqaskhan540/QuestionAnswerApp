import React from "react";
import { Grid, Box } from "grommet";

const ScreenContainer = ({ left, middle, right }) => (
  <Grid
    rows={["xlarge"]}
    columns={["small", "large", "small"]}
    gap="small"
    areas={[
      { name: "left", start: [0, 0], end: [0, 0] },
      { name: "middle", start: [1, 0], end: [1, 0] },
      { name: "right", start: [2, 0], end: [2, 0] }
    ]}
  >
    <Box gridArea="left">{left}</Box>
    <Box gridArea="middle" margin = {"small"}>{middle}</Box>
    <Box gridArea="right">{right}</Box>
  </Grid>
);

export default ScreenContainer;
