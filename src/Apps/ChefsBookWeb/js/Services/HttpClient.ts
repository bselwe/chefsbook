import { loginManager, CannotRefreshToken } from "./LoginManager";

export class UnauthorizedRequest extends Error { }

enum RequestMethod { Get, Post, Put, Delete }

export class HttpClient {
    private readonly baseUrl: string;

    constructor(baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    protected get<TResult, TError = {}>(url: string): Promise<Models.ApiResponse<TResult, TError>> {
        return this.executeRequest<TResult, TError>(url, RequestMethod.Get);
    }

    protected post<TResult, TError = {}>(url: string, data?: any): Promise<Models.ApiResponse<TResult, TError>> {
        return this.executeRequest<TResult, TError>(url, RequestMethod.Post, data);
    }

    protected put<TResult, TError = {}>(url: string, data?: any): Promise<Models.ApiResponse<TResult, TError>> {
        return this.executeRequest<TResult, TError>(url, RequestMethod.Put, data);
    }

    protected delete<TResult, TError = {}>(url: string, data?: any): Promise<Models.ApiResponse<TResult, TError>> {
        return this.executeRequest<TResult, TError>(url, RequestMethod.Delete, data);
    }

    private async executeRequest<TResult, TError = {}>(url: string, method: RequestMethod, data?: any): Promise<Models.ApiResponse<TResult, TError>> {
        const makeRequest = async () => {
            const headers = new Headers();

            if (method !== RequestMethod.Get) {
                headers.append("Content-Type", "application/json; charset=UTF-8");
            }

            if (loginManager.isSigned) {
                let token = await loginManager.getToken();
                headers.append("Authorization", `Bearer ${token}`);
            }

            const requestInit: RequestInit = {
                method: this.getRequestMethod(method),
                headers,
                body: data ? JSON.stringify(data) : undefined,
            };

            return await fetch(this.baseUrl + url, requestInit);
        };

        let response = await makeRequest();
        return await this.handleResponse<TResult, TError>(response, makeRequest);
    }

    private async handleResponse<TResult, TError>(response: Response, makeRequest?: () => Promise<Response>): Promise<Models.ApiResponse<TResult, TError>> {
        if (response.status === 401) {
            if (loginManager) {
                if (makeRequest) {
                    if (!await loginManager.tryRefreshToken()) {
                        throw new CannotRefreshToken("Cannot refresh access token after the server returned 401 Unauthorized");
                    }
                    return this.handleResponse<TResult, TError>(await makeRequest());
                } else {
                    throw new UnauthorizedRequest("The request has not been authorized and token refresh did not help");
                }
            }
            else {
                throw new UnauthorizedRequest("User need to be authenticated to execute the command/query");
            }
        }

        let body = {};
        try {
            body = await response.json();
        }
        catch { }

        if (response.status >= 200 && response.status < 300) {
            return {
                IsSuccess: true,
                StatusCode: response.status,
                Response: body as TResult
            };
        }
        else {
            return {
                IsSuccess: false,
                StatusCode: response.status,
                Error: body as TError
            };
        }
    }

    private getRequestMethod(method: RequestMethod): string {
        switch (method) {
            case RequestMethod.Get:
                return "GET";
            case RequestMethod.Post:
                return "POST";
            case RequestMethod.Put:
                return "PUT";
            case RequestMethod.Delete:
                return "DELETE";
        }
    }
}
