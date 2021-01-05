import React from 'react';
import 'antd/dist/antd.css';
import { Form, Input, Button, DatePicker } from 'antd';
import PhoneInputMask from '../common/phone-input-mask';

const SignUpForm = ({ submit, ...props }) => {
    const [form] = Form.useForm();

    const onPhoneChange = phone => {
        return phone.replace(/\(|\)|\s|-/g, "");
    };

    const beforeSubmit = values => {
        values.phone = onPhoneChange(values.phone);
        submit(values);
    };

    return (
        <div className="form-container center-container">
            <Form
                layout="vertical"
                form={form}
                name="register"
                onFinish={beforeSubmit}
                size="default"
                scrollToFirstError
            >
                
                <Form.Item
                    label="First name"
                    name={['user', 'name']}
                    className="ant-input-my-sign-up"
                    rules={
                        [
                            {
                                required: true,
                                message: "First name is required"
                            }
                        ]
                    }>
                    <Input placeholder="First name"
                        className="ant-input-my-sign-up"
                    />
                </Form.Item>

                <Form.Item
                    label="Last name"
                    name={['user', 'lastname']}
                    className="ant-input-my-sign-up"
                    rules={
                        [
                            {
                                required: true,
                                message: "Last name is required"
                            }
                        ]
                    }>
                    <Input placeholder="Last name"
                        className="ant-input-my-sign-up"
                    />
                </Form.Item>

                <Form.Item
                    label="Email"
                    name="email"
                    className="ant-input-my-sign-up"
                    rules={[
                        {
                            type: 'email',
                            message: 'The input is not valid E-mail!',
                        },
                        {
                            required: true,
                            message: 'Please input your E-mail!',
                        },
                    ]}
                >
                    <Input placeholder="Email"
                        className="ant-input-my-sign-up"
                    />
                </Form.Item>

                <Form.Item
                    label="Phone"
                    name="phone"
                    className="ant-input-my-sign-up"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your phone!'
                        }
                    ]}
                    >
                    <PhoneInputMask className="ant-input ant-input-my-sign-up" />
                </Form.Item>

                <Form.Item
                    label="Password"
                    name="password"
                    className="ant-input-my-sign-up"
                    rules={[
                        {
                            required: true,
                            message: 'Please input your password!',
                        },
                    ]}
                    hasFeedback
                >
                    <Input.Password placeholder="Password"
                        className="ant-input-my-sign-up"
                    />
                </Form.Item>

                <Form.Item
                    label="Confirm"
                    name="confirm"
                    className="ant-input-my-sign-up"
                    dependencies={['password']}
                    hasFeedback
                    rules={[
                        {
                            required: true,
                            message: 'Please confirm your password!',
                        },
                        ({ getFieldValue }) => ({
                            validator(rule, value) {
                                if (!value || getFieldValue('password') === value) {
                                    return Promise.resolve();
                                }
                                return Promise.reject('The two passwords that you entered do not match!');
                            },
                        }),
                    ]}
                >
                    <Input.Password placeholder="Confirm password"
                        className="ant-input-my-sign-up"
                    />
                </Form.Item>
                <Form.Item
                    label="Birthdate"
                    name="birthdate"
                    rules={
                        [
                            {
                                type: 'object',
                                required: true,
                                message: "Select date of birth, please"
                            }
                        ]
                    }>
                    <DatePicker
                        format='DD/MM/YYYY'
                        className="ant-input-my-sign-up"
                        placeholder="Select birth date"
                    />
                </Form.Item>

                <Form.Item
                >
                    <Button
                        type="primary"
                        htmlType="submit"
                        size="large"
                        className="ant-btn-primary-my"
                        style={{ width: '100%' }}
                    >
                        Sign up
        </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default SignUpForm;