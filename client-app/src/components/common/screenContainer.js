import React from "react";
import { Grid, Box, List } from "grommet";
import { Input, Label, Menu } from "semantic-ui-react";

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
      <Menu pointing secondary vertical>
        <Menu.Item name="inbox">
          <Label color="teal">1</Label>
          Inbox
        </Menu.Item>

        <Menu.Item name="spam">
          <Label>51</Label>
          Spam
        </Menu.Item>

        <Menu.Item name="updates">
          <Label>1</Label>
          Updates
        </Menu.Item>
        <Menu.Item>
          <Input icon="search" placeholder="Search mail..." />
        </Menu.Item>
      </Menu>
    </Box>
    <Box gridArea="middle" margin={"small"}>
      {middle}
    </Box>
    <Box gridArea="right">{right}</Box>
  </Grid>
);

export default ScreenContainer;
