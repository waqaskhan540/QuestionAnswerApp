import React from "react";
import { Button, Icon } from "semantic-ui-react";


const SmallButton = ({ label, icon , onClick}) => (
  <Button size="mini" onClick = {onClick} >
    <Icon name={icon} />
    {label}
  </Button>
);

export default SmallButton;
