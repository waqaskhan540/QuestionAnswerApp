import React from "react";
import { Grid, Box, List } from "grommet";
import SideBar from "./sideBar";



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
    <Box gridArea="left" margin={"small"} gap={"large"}>
        {left}
    </Box>
    <Box gridArea="middle" margin={"large"} pad={"small"}>
      {middle}
    </Box>
    <Box gridArea="right">{right}</Box>
  </Grid>
);

export default ScreenContainer;
