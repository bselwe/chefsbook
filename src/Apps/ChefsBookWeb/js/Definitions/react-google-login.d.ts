declare module "react-google-login" {
    import * as React from "react";

    interface GoogleSuccessResponse {
        readonly accessToken: string;
    }

    interface GoogleFailureResponse {
        readonly error: string;
    }

    interface GoogleLoginProps {
        readonly clientId: string;
        readonly buttonText: string;
        readonly onSuccess: (response: GoogleSuccessResponse) => void;
        readonly onFailure: (response: GoogleFailureResponse) => void;
    }

    export class GoogleLogin extends React.Component<GoogleLoginProps> {
    }
}
