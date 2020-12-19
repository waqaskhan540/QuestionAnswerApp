import React from "react";
import "./NormalButtonStyles.css";
import { Button, Box } from "grommet";
import {Spinning} from "grommet-controls";

export const LoadingButton = ({ loading, label, onClick, icon }) => {
  if (loading) {
    return (
      <Box border align="center" pad="xxsmall">
        <Spinning kind="wave" />
      </Box>
    );
  }

  return (
    <Button
      label={label}
      className={"normal-button"}
      icon={icon}
      onClick={onClick}
    />
  );
};
