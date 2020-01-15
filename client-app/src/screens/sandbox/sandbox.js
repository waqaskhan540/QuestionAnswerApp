import React from "react";
import {InputText} from "./../../theme/inputs/InputText";
import {FeedCard} from "./../../theme/cards/FeedCard";
import {FeaturedTopics} from "./../../theme/sidebars/featuredTopics";
import {Box} from "grommet";

class Sandbox extends React.Component {
    render() {
        return(
            <Box direction="column" margin="medium" pad="medium" gap="small">
                <InputText placeholder="placeholder text" label= "label" />
                <FeedCard
                    avatar= {"https://v2.grommet.io/assets/Wilderpeople_Ricky.jpg"}
                    username = {"Muhammad Waqas"}
                    userbio = {"Software Engineer at Nova"}
                    questionId = {1}
                    questionText = {"Velit occaecat laboris labore deserunt minim proident irure et officia sit amet minim elit esse."}
                    onClickFollow={() => alert("follow")}
                    onClickAnswer={() => alert("answer")}
                    onClickSave= {() => alert("save")}
                    footer                    
                />
                <FeaturedTopics/>
            </Box>
        )
    }
}

export default Sandbox;