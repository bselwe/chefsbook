interface Configuration {
    authEndpoint: string;
    authSecret: string;
    apiEndpoint: string;
    googleClientId: string;
}

const config: Configuration = {
    authEndpoint: "https://localhost:5000",
    authSecret: "secret",
    apiEndpoint: "https://localhost:5001/api",
    googleClientId: "1070857886466-nsvmqiisg2kdvkko1sk2v9e8dnuefbtp.apps.googleusercontent.com"
};

export default config;
