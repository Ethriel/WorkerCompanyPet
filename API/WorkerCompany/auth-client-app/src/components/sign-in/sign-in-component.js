import React, { useState } from 'react';
import '../../styles/common.css';
import LoginForm from './index.js';
import { withRouter } from 'react-router';

const SignInComponent = (props) => {
    const [signIn, setSignIn] = useState({});
    const [ready, setReady] = useState(false);

    const submit = values => {
        setSignIn(values);
        setReady(true);
    };

    if (ready) {
        console.log(signIn);
    }

    return (
        <LoginForm submit={submit} />
    )
};

export default withRouter(SignInComponent);