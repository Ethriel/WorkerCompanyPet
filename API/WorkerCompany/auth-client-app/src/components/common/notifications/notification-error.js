import { notification } from 'antd';
import GetNotificationDescription from './helpers/get-notification-description';
import SetErrorData from './helpers/set-error-data';

const NotificationError = error => {
    const errorData = SetErrorData(error);
    const description = GetNotificationDescription(errorData.errors);
    notification.error({
        message: errorData.message,
        description: description
    });
};

export default NotificationError;