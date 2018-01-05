import config from "../Configuration/Config";
import { HttpClient } from "./HttpClient";

class ApiClient extends HttpClient {
}

export const apiClient = new ApiClient(config.apiEndpoint);
