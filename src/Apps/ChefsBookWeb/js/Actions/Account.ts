import { createAction, handleAction, Action } from "redux-actions";
import { createAsyncAction, StartTask, EndTask } from "./Infrastructure/AsyncActions";
import { loginManager } from "../Infrastructure/LoginManager";

export const TrySignInWithGoogle = createAsyncAction<AppState, boolean, string>(
    "ACCOUNT/SIGN_IN_WITH_GOOGLE",
    (accessToken: string) => {
        return loginManager.trySignInWithGoogle(accessToken);
    },
    (state, action) => {
        let isSigned = !!action.payload;

        return {
            ...state,
            ...EndTask(state),
            isSigned,
            signInError: !isSigned ? "Couldn't sign user in using Google. Try again." : undefined
        };
    },
    (state) => {
        return {
            ...state,
            ...StartTask(state),
            signInError: undefined
        };
    },
    (state, action) => {
        let signInError = action.payload ? action.payload.message : "Couldn't sign user in";

        return {
            ...state,
            ...EndTask(state),
            isSigned: false,
            signInError
        };
    }
);

const setSignedIn = createAction<boolean>("ACCOUNT/INTERNAL_SET_SIGNED_IN");

loginManager.onChange(() => {
    setSignedIn(loginManager.isSigned);
});

export const signOut = createAction("ACCOUNT/SIGN_OUT");

export const trySignInWithGoogle = TrySignInWithGoogle.action;

export const accountReducers: ReduxActions.ReducerMap<AppState, any> = {
    [signOut.toString()](state: AppState): AppState {
        loginManager.signOut();

        return {
            tasksCount: 0,
            isSigned: false,
        };
    },
    [setSignedIn.toString()](state: AppState, payload: Action<boolean>): AppState {
        return {
            ...state,
            isSigned: !!payload.payload
        };
    },
    ...TrySignInWithGoogle.reducers
};
