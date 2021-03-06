import React from "react";
import { Grid, Box, Main } from "grommet";
import AppBar from "../../containers/appBarContainer";

const ScreenContainer = ({ left, middle, right }) => (
  <>
    <AppBar />
    <Grid
      align="stretch"
      rows={["large"]}     
      columns={["25%", "50%", "25%"]}
      gap="small"
      areas={[
        { name: "left", start: [0, 0], end: [0, 0] },
        { name: "middle", start: [1, 0], end: [1, 0] },
        { name: "right", start: [2, 0], end: [2, 0] }
      ]}
    >
      <Box gridArea="left">
        {left}
      </Box>
      <Box gridArea="middle"  gap={"small"}>
        {middle}
      </Box>
      <Box gridArea="right">
        {right}
      </Box>
    </Grid>
  </>
);

export default ScreenContainer;
