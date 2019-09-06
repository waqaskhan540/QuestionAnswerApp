export const getHeaders = () => {
    const {accessToken} = JSON.parse(localStorage.getItem("state")).user;
    return {
        "Authorization":`Bearer ${accessToken}`
    }
}