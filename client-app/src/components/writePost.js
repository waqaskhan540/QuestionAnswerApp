import React, { Component } from "react";
import { Box, TextInput, Text } from "grommet";
import { Spinning } from "grommet-controls";

class WritePost extends Component {
  render() {
    const { value, onChange, onKeyPress, isLoading } = this.props;
    return (
      <Box
        width="large"
        direction="row"            
        pad={{ horizontal: "small", vertical: "xsmall" }}
        //round="medium"
        border={{
          side: "bottom",
          color: "#7D4CDB",
          size: "small"
        }}
      >
        {isLoading ? (
          <Box
            align="center"
            alignContent="center"
            fill
            pad={{ horizontal: "small", vertical: "xsmall" }}
          >
            <Spinning color="accent-1" />
        </Box>
        ) : (
          <TextInput
            type="search"
            plain
            value={value}
            onChange={onChange}
            onKeyPress={onKeyPress}
            style = {{fontSize : "20px"}}
            //  onSelect={onSelect}
            //  suggestions={renderSuggestions()}
            placeholder="Write your question and press 'Enter'"
            // onSuggestionsOpen={() => setSuggestionOpen(true)}
            // onSuggestionsClose={() => setSuggestionOpen(false)}
          />
        )}
      </Box>
    );
  }
}

export default WritePost;
