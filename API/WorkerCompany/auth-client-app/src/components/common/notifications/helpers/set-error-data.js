const SetErrorData = error => {
    const errorData = {
        message: "",
        errors: []
    };
    if (error.isAxiosError) {
        if (error.response) {
            const errorResponseData = error.response.data;
            const status = error.response.status;
            if (status === 500) {
                errorData.message = "Server error occured";
                errorData.errors.push("Internal server error");
            }
            else if (status === 401) {
                errorData.message = "No access";
                errorData.errors.push("Unauthorised");
            }
            else if (status === 403) {
                errorData.message = "No access";
                errorData.errors.push("Forbidden")
            }
            else if (status === 404) {
                errorData.message = errorResponseData.message ? errorResponseData.message : "Not found";
                if (errorResponseData.errors) {
                    errorData.errors = errorResponseData.errors;
                }
                else {
                    errorData.errors.push("Resourse was not found")
                }

            }
            else {
                errorData.message = errorResponseData.message;
                errorData.errors = errorResponseData.errors;
            }

        }
        else {
            errorData.message = "A network error occured";
            errorData.errors.push("Connection refused. API server is offline");
            errorData.errors.push("Or check your internet connection");
        }
    }
    else{
        errorData.message = "Script error";
        errorData.errors.push("Contact the administrator if needed");
    }

    return errorData;
};

export default SetErrorData;