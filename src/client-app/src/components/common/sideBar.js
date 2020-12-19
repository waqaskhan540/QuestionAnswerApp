import React from "react";
import { Input, Label, Menu } from "semantic-ui-react";

const categories = [
  "Lifestyle",
  "Technology",
  "Software Engineering",
  "Politics"
];

const SideBar = ({ isUserAuthenticated, draftCount, savedCount }) => {
  if (isUserAuthenticated) {
    return (
      <Menu secondary vertical>
        <Menu.Item>
          {draftCount && draftCount > 0 ? (
            <Label color="teal">{draftCount}</Label>
          ) : (
            ""
          )}
          Drafts
        </Menu.Item>

        <Menu.Item>
          {savedCount && savedCount > 0 ? (
            <Label color="teal">{savedCount}</Label>
          ) : (
            ""
          )}
          Saved
        </Menu.Item>
      </Menu>
    );
  }

  return (
    <Menu secondary vertical>
      {categories.map(category => {
        return (
          <Menu.Item key= {category}>
            <Label color="teal">1</Label>
            {category}
          </Menu.Item>
        );
      })}
    </Menu>
  );
};

export default SideBar;
