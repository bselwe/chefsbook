import * as React from "react";
import { connect } from "react-redux";
import { Card, CardTitle, CardText, TextField, CardActions, RaisedButton } from "material-ui";
import { AsyncDispatch } from "../../Store/AsyncActions";
import { trySignInWithGoogle } from "../../Actions/Account";
import { GoogleLogin, GoogleSuccessResponse } from "react-google-login";
import config from "../../Configuration/Config";

interface AuthenticationProps {
    children?: React.ReactNode;
}

interface AuthenticationStateProps extends AuthenticationProps {
    isSigned: boolean;
    signInError?: string;
}

interface AuthenticationDispatchProps {
    signIn: (accessToken: string) => void;
}

type LoginProps = AuthenticationStateProps & AuthenticationDispatchProps;

export class Login extends React.Component<LoginProps> {
    constructor(props: LoginProps) {
        super(props);
    }

    private signInWithGoogle = (response: GoogleSuccessResponse) => {
        this.props.signIn(response.accessToken);
    }

    render() {
        return !this.props.isSigned ?
            <div style={{ maxWidth: 600, margin: "5rem auto" }}>
                <form>
                    <Card containerStyle={{ textAlign: "center", padding: "2rem 0" }}>
                        <CardTitle
                            title="Chefsbook Manager"
                            subtitle={this.props.signInError}
                            subtitleStyle={{ marginTop: "12px" }} />

                        <CardText>
                            <GoogleLogin
                                clientId={config.googleClientId}
                                buttonText="Sign in with Google"
                                onSuccess={this.signInWithGoogle}
                                onFailure={console.warn} />
                        </CardText>
                    </Card>
                </form>
            </div> : { ...this.props.children as any};
    }
}

const mapStateToProps = (state: AccountState, props: AuthenticationProps): AuthenticationStateProps => {
    return {
        ...props,
        isSigned: state.isSigned,
        signInError: state.signInError
    };
};

const mapDispatchToProps = (dispatch: any): AuthenticationDispatchProps => {
    return {
        signIn: (accessToken: string) =>
            dispatch(trySignInWithGoogle(accessToken))
    };
};

export const Authentication = connect(
    mapStateToProps,
    mapDispatchToProps
)(Login);
