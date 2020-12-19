import React from "react";
import { Button } from "grommet";
import { Edit } from "grommet-icons";

const SmallButton = ({ label, icon, onClick }) => (
  
    <Button icon={icon} onClick={onClick}  plain />
   
);

export default SmallButton;
