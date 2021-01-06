import React from 'react';
import 'antd/dist/antd.css';
import { Form, Input, Button, DatePicker, Select } from 'antd';
import { v4 as uuidv4 } from 'uuid';

const { Option } = Select;

const SignUpForm = ({ submit, workers, ...props }) => {
    const [form] = Form.useForm();

    const layout = {
        labelCol: {
            span: 8,
        },
        wrapperCol: {
            span: 16,
        },
    };

    const labelAlign = 'left';

    const workersItems =
        <Select>
            {workers.map((w) => {
                return <Option key={w.id} value={w.id}>{w.name}</Option>
            })}
        </Select>;

    const beforeSubmit = values => {
        submit(values);
    };

    return (
        <div className="form-container center-container">
            <Form
                // layout="horizontal"
                {...layout}
                form={form}
                name="register"
                onFinish={beforeSubmit}
                size="default"
                scrollToFirstError
            >

                <Form.Item
                    label="First name"
                    labelAlign={labelAlign}
                    name='firstName'
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
                    labelAlign={labelAlign}
                    name='lastName'
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
                    label="Display name"
                    labelAlign={labelAlign}
                    name='displayName'
                    className="ant-input-my-sign-up"
                    rules={
                        [
                            {
                                required: true,
                                message: "Display name is required"
                            }
                        ]
                    }>
                    <Input placeholder="Display name"
                        className="ant-input-my-sign-up"
                    />
                </Form.Item>

                <Form.Item
                    label="Email"
                    labelAlign={labelAlign}
                    name="username"
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
                    label="Password"
                    labelAlign={labelAlign}
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
                    labelAlign={labelAlign}
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
                    labelAlign={labelAlign}
                    name="dateOfBirth"
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
                    label="Select worker"
                    labelAlign={labelAlign}
                    name="workerId"
                    rules={
                        [
                            {
                                required: true,
                                message: "Worker is required"
                            }
                        ]
                    }>
                    {workersItems}
                </Form.Item>

                <Form.Item
                    label="Marriage status"
                    labelAlign={labelAlign}
                    name="marriageStatus">
                    <Select key={uuidv4()}>
                        <Option key={uuidv4()} value="Married">Married</Option>
                        <Option key={uuidv4()} value="Not married">Not married</Option>
                    </Select>
                </Form.Item>

                <Form.Item
                    label="Gender"
                    labelAlign={labelAlign}
                    name="gender"
                    rules={
                        [
                            {
                                required: true,
                                message: "Gender is required"
                            }
                        ]
                    }>
                    <Select key={uuidv4()}>
                        <Option key={uuidv4()} value="Male">Male</Option>
                        <Option key={uuidv4()} value="Female">Female</Option>
                        <Option key={uuidv4()} value="Unspecified">Unspecified</Option>
                    </Select>
                </Form.Item>

                <Form.Item
                    wrapperCol={{
                        offset: 0,
                        span: 24,
                    }}
                >
                    <Button
                        type="primary"
                        htmlType="submit"
                        size="large"
                        className="ant-btn-primary-my"
                        block={true}
                    // style={{ width: '100%' }}
                    >
                        Sign up
                    </Button>
                </Form.Item>
            </Form>
        </div>
    );
};

export default SignUpForm;