import React from "react";
import { Box, Image } from "grommet";

export const Avatar = ({ image }) => (
  <Box height="xxsmall" width="xxsmall" round="full" alignSelf="center">
    <Image src={image} height="40" width="40"/>
  </Box>
);
