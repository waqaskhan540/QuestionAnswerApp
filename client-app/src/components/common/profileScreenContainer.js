import React from "react";
import { Box ,Stack} from "grommet";


const ProfileScreenContainer = ({ children }) => (
    <Stack fill anchor="bottom">
    <Box background="brand" fill pad={"xlarge"}>
    { children }
    </Box>
  </Stack>
);

export default ProfileScreenContainer;
