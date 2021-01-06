import React, { useState, useEffect, useCallback } from 'react';
import '../../styles/common.css';
import SignUpForm from './index';
import { withRouter } from 'react-router';
import axios from 'axios';
import MakeRequestAsyncBody from '../common/make-request-async-body';
import NotificationError from '../common/notifications/notification-error';

const SignUpComponent = (props) => {

    const [signUp, setSignUp] = useState({});
    const [workers, setWorkers] = useState([]);
    const [ready, setReady] = useState(false);

    const fetchWorkers = useCallback(async (token) =>{
        try {
            setReady(false);
            const response = await MakeRequestAsyncBody('workers/data', { msg: "hi" }, "GET", token);
            const data = response.data;
            setWorkers(data.responseData.workers);
            setReady(true);
        } catch (error) {
            NotificationError(error)
        }
    }, []);

    useEffect(() => {
        const signal = axios.CancelToken.source();
        fetchWorkers(signal.token);
        return function cleanUp(){
            signal.cancel("CANCEL IF FETCH IDS");
        }
    }, [fetchWorkers]);

    const submit = values => {
        console.log(values);
        setReady(false);
    }

    return (
        <>
            {
                ready && <SignUpForm workers={workers} submit={submit}/>
            }
        </>
    )
};

export default withRouter(SignUpComponent);
