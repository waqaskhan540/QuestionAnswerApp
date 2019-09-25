import React from "react";
import { Formik } from "formik";
import { Form, Button, Message } from "semantic-ui-react";

const LoginForm = ({ submitHandler, error, validateForm }) => (
  <div>
    <Formik
      initialValues={{ email: "", password: "" }}
      validate={validateForm}
      onSubmit={submitHandler}
    >
      {({ errors, touched, handleChange, handleSubmit, isSubmitting }) => (
        <Form
          loading={isSubmitting}
          onSubmit={handleSubmit}
          error={error.length > 0}
        >
          <Form.Input
            error={errors.email && touched.email && errors.email}
            fluid
            label="Email"
            name="email"
            placeholder="Email"
            onChange={handleChange}
          />

          <Form.Input
            error={errors.password && touched.password && errors.password}
            fluid
            label="Password"
            name="password"
            placeholder="Password"
            type="password"
            onChange={handleChange}
          />

          {error.length ? <Message error header="Error" content={error} /> : ""}

          <Button type="submit" disabled={isSubmitting}>
            Submit
          </Button>
        </Form>
      )}
    </Formik>
  </div>
);

export default LoginForm;
