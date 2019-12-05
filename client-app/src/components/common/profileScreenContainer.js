import React from "react";
import { Grid, Box, List ,Stack} from "grommet";
import SideBar from "./sideBar";

const ProfileScreenContainer = ({ children }) => (
    <Stack fill anchor="bottom">
    <Box background="brand" fill pad={"xlarge"}>
    { children }
    </Box>
  </Stack>
);

export default ProfileScreenContainer;
