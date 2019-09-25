import React, { Component } from "react";
import RichTextEditor from "react-rte";
import { Header, Button, Icon } from "semantic-ui-react";

export default class TextEditor extends Component {
  //   static propTypes = {
  //     onChange: PropTypes.func
  //   };

  state = {
    value: RichTextEditor.createEmptyValue()
  };

  onChange = value => {
    this.setState({ value });
    if (this.props.onChange) {
      // Send the changes up to the parent component as an HTML string.
      // This is here to demonstrate using `.toString()` but in a real app it
      // would be better to avoid generating a string on each change.
      this.props.onChange(value.toString("html"));
    }
  };

  postAnswer = (e) => this.props.onPostAnswer(this.state.value.toString("html"));

  render() {
    
    const { question } = this.props;
    return (
      <div>
        <Header as="h1">{question.questionText}</Header>
        <span>{question.user.firstName}&nbsp;</span>
        <span>{question.user.lastName}</span> - &nbsp;
        <span>{new Date(question.dateTime).toLocaleDateString()}</span>
        <RichTextEditor         
          value={this.state.value}
          onChange={this.onChange}
          placeholder = {"Write you answer here!"}
          editorStyle = {{height:'250px'}}
        />
        <br />
        <Button icon labelPosition="left" primary onClick = {this.postAnswer}>
          <Icon name="send" />
          Publish Answer
        </Button>
        <Button icon labelPosition="left">
          Save as Draft
          <Icon name="firstdraft" />
        </Button>
      </div>
    );
  }
}
