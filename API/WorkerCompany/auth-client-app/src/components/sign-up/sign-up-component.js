import React, { useState, useEffect, useCallback } from 'react';
import '../../styles/common.css';
import SignUpForm from './index';
import { withRouter } from 'react-router';
import axios from 'axios';
import MakeRequestAsyncBody from '../common/make-request-async-body';
import NotificationError from '../common/notifications/notification-error';
import NotificationOk from '../common/notifications/notification-ok';
import getFormattedDate from './helpers/get-formatted-date';
import { SIGN_UP } from '../../constants/auth';
import { WORKERS_DATA } from '../../constants/workers';
import { GET, POST } from '../../constants/request-methods';

const SignUpComponent = (props) => {
    const [workers, setWorkers] = useState([]);
    const [ready, setReady] = useState(false);

    const fetchWorkers = useCallback(async (token) => {
        try {
            setReady(false);
            const response = await MakeRequestAsyncBody(WORKERS_DATA, { msg: "hi" }, GET, token);
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
        return function cleanUp() {
            signal.cancel("CANCEL IF FETCH WORKERS");
        }
    }, [fetchWorkers]);

    const submit = async values => {
        setReady(false);
        const { confirm, ...model } = values;
        model.marriageStatus = model.marriageStatus === "Married";
        const formattedDate = getFormattedDate(model.dateOfBirth._d);
        model.dateOfBirth = formattedDate;

        try {
            const signal = axios.CancelToken.source();
            const response = await MakeRequestAsyncBody(SIGN_UP, model, POST, signal.token);
            NotificationOk(response.data.message);
            setReady(true);
        } catch (error) {
            NotificationError(error);
        }
    }

    return (
        <>
            {
                ready && <SignUpForm workers={workers} submit={submit} />
            }
        </>
    )
};

export default withRouter(SignUpComponent);
