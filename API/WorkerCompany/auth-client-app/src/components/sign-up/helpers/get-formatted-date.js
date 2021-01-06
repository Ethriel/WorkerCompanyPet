import moment from 'moment';

const getFormattedDate = date => {
    const dateObj = new Date(date);
    const momentObj = moment(dateObj);
    const formatted = momentObj.format("MM/DD/YYYY");
    return formatted;
}

export default getFormattedDate;