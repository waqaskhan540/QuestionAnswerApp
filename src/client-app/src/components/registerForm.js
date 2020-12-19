import React from "react";
import { Formik } from "formik";
import { Form, Message } from "semantic-ui-react";
import {Button} from "grommet";

const RegisterForm = ({ error, validateForm, submitHandler }) => (
  <div>
    <Formik
      initialValues={{
        firstname: "",
        lastname: "",
        email: "",
        password: "",
        confirmPassword: ""
      }}
      validate={validateForm}
      onSubmit={submitHandler}
    >
      {({ errors, touched, handleChange, handleSubmit, isSubmitting }) => (
        <Form
          onSubmit={handleSubmit}
          loading={isSubmitting}
          error={error.length > 0}
        >
          <Form.Input
            error={errors.firstname && touched.firstname && errors.firstname}
            fluid
            label="First Name"
            name="firstname"
            placeholder="First Name"
            onChange={handleChange}
          />
          <Form.Input
            error={errors.lastname && touched.lastname && errors.lastname}
            fluid
            label="Last Name"
            name="lastname"
            placeholder="Last Name"
            onChange={handleChange}
          />
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

          <Form.Input
            error={
              errors.confirmPassword &&
              touched.confirmPassword &&
              errors.confirmPassword
            }
            fluid
            label="Confirm Password"
            name="confirmPassword"
            placeholder="Confirm Password"
            type="password"
            onChange={handleChange}
          />

          {error.length ? <Message error header="Error" content={error} /> : ""}

          {/* <Button type="submit" disabled={isSubmitting}>
            Submit
          </Button> */}
          <Button
            label="Register"
            type="submit"
            disabled={isSubmitting}
            fill="horizontal"
          />
        </Form>
      )}
    </Formik>
  </div>
);

export default RegisterForm;

