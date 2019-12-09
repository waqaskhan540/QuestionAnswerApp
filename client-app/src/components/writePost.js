import React, { Component } from "react";
import { Box, TextInput,Text } from "grommet";

class WritePost extends Component {
  render() {
    return (
       
        <Box
         // ref={boxRef}
          width="large"
          direction="row"
          align="center"
          pad={{ horizontal: "small", vertical: "xsmall" }}
          round="medium"   
               
          border={{
            side: "all",
            color: "#7D4CDB" ,
            size:"medium"            
          }}         
        >
         
          <TextInput
            type="search"
           // dropTarget={boxRef.current}
            plain
          //  value={value}
           // onChange={onChange}
          //  onSelect={onSelect}
          //  suggestions={renderSuggestions()}
            placeholder="Write your question and press 'Enter'"
           // onSuggestionsOpen={() => setSuggestionOpen(true)}
           // onSuggestionsClose={() => setSuggestionOpen(false)}
          />
        </Box>
      
    );
  }
}

export default WritePost;
