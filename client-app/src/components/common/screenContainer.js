import React from "react";
import { Grid, Box } from "grommet";

const ScreenContainer = ({ left, right }) => (
  <Grid
    
    rows={["large"]}
    columns={["large", "medium"]}
    gap="small"
    areas={[
      { name: "left", start: [0, 0], end: [0, 0] },
      { name: "right", start: [1, 0], end: [1, 0] },
      
    ]}
  >
    <Box gridArea="left" margin={"small"} gap={"large"} >
      {left}
    </Box>
    <Box gridArea="middle" margin={"small"} pad={"small"} gap={"small"}>
      {right}
    </Box>
    
  </Grid>
);

export default ScreenContainer;
