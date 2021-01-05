import React, { useState } from 'react';
import '../../styles/common.css';
import SignUpForm from './index';
import { withRouter } from 'react-router';

const SignUpComponent = (props) => {

    const [signUp, setSignUp] = useState({});
    const [ready, setReady] = useState(false);

    const submit = values => {
        setSignUp(values);
        setReady(true);
    }

    if (ready) {
        console.log(signUp);
    }

    return (
        <SignUpForm submit={submit} />
    )
};

export default withRouter(SignUpComponent);
