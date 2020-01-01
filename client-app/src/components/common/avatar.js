import React from "react";
import {Box} from "grommet";

export const Avatar = ({ image }) => (
    <Box
      height="xxsmall"
      width="xxsmall"
      round="full"   
      alignSelf="center"
      background={`url(data:image/png;base64, ${image})`}
    />
  );