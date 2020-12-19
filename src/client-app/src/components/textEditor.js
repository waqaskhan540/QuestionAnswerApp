import React, { Component } from "react";
import RichTextEditor from "react-rte";
import { Header, Button, Icon } from "semantic-ui-react";

export default class TextEditor extends Component {
  state = {
    value: RichTextEditor.createEmptyValue(),
    publishBtnDisabled: true
  };

  onChange = value => {
    this.setState({ value });
    if (value.toString("html") !== "<p><br></p>")
      this.setState({ publishBtnDisabled: false });
    else this.setState({ publishBtnDisabled: true });

    if (this.props.onChange) {
      this.props.onChange(value.toString("html"));
    }
  };

  saveDraft = e => {
    const data = {
      content: this.state.value.toString("html"),
      questionId: this.props.question.id
    };
    this.props.onSaveDraft(data);
  };
  postAnswer = e => {
    this.props.onPostAnswer(this.state.value.toString("html"));
  };
  render() {
    const { question } = this.props;
    return (
      <div>
        <Header as="h1">{question.questionText}</Header>
        <RichTextEditor
          value={this.state.value}
          onChange={this.onChange}
          placeholder={"Write you answer here!"}
          editorStyle={{ height: "250px" }}
        />
        <br />
        <Button
          icon
          labelPosition="left"
          disabled={this.state.publishBtnDisabled}
          primary
          loading={this.props.publishingAnswer}
          onClick={this.postAnswer}
        >
          <Icon name="send" />
          Publish Answer
        </Button>
        <Button
          icon
          labelPosition="left"
          disabled={this.props.savingDraft}
          loading={this.props.savingDraft}
          onClick={this.saveDraft}
        >
          Save as Draft
          <Icon name="firstdraft" />
        </Button>
      </div>
    );
  }
}
