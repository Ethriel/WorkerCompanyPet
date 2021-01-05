import React, { useState } from 'react';
import '../../styles/common.css';
import LoginForm from './index.js';
import { withRouter } from 'react-router';
import axios from 'axios';
import MakeRequestAsyncBody from '../common/make-request-async-body';

const SignInComponent = (props) => {
    const submit = async values => {
        const signal = axios.CancelToken.source();
        const response = await MakeRequestAsyncBody('sign-in', values, "POST", signal.token);
        const authData = response.data.authData;
        console.log(authData);
    };

    return (
        <LoginForm submit={submit} />
    )
};

export default withRouter(SignInComponent);