import React from 'react';
import '../../styles/common.css';
import LoginForm from './index.js';
import { withRouter } from 'react-router';
import axios from 'axios';
import MakeRequestAsyncBody from '../common/make-request-async-body';
import NotificationError from '../common/notifications/notification-error';
import NotificationOk from '../common/notifications/notification-ok';
import { SIGN_IN } from '../../constants/auth';
import { POST } from '../../constants/request-methods';

const SignInComponent = (props) => {
    const submit = async values => {
        try {
            const signal = axios.CancelToken.source();
            const response = await MakeRequestAsyncBody(SIGN_IN, values, POST, signal.token);
            const authData = response.data.authData;
            NotificationOk(response.data.message);
        } catch (error) {
            NotificationError(error);
        }

    };

    return (
        <LoginForm submit={submit} />
    )
};

export default withRouter(SignInComponent);