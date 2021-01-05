import axios from 'axios';

const MakeRequestAsyncBody = async (urlTail, body, method, cancelToken) => {
    const requestUrl = "https://localhost:5021/".concat(urlTail);
    axios.defaults.withCredentials = true;
    const axiosConfig = {
        method: method,
        data: body,
        cancelToken: cancelToken,
        url: requestUrl,
        headers: {
            // "Authorization": localStorage.getItem("bearer_header"),
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*"
        }
    };
    try {
        const response = await axios(axiosConfig);
        return response;
    } catch (error) {
        throw error;
    }
};

export default MakeRequestAsyncBody;