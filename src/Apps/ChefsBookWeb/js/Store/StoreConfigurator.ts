import { createStore, applyMiddleware } from "redux";
import { Store } from "redux";
import promiseMiddleware from "redux-promise-middleware";
import { Provider } from "react-redux";
import logger from "redux-logger";

import { handleActions } from "redux-actions";
import { loginManager } from "../Infrastructure/LoginManager";
import { accountReducers } from "../Actions/Account";

const reducers = {
    ...accountReducers
};

const initialState: AppState = {
    tasksCount: 0,
    isSigned: loginManager.isSigned
};

const reducer = handleActions<AppState>(reducers, initialState);

let store = createStore(
    reducer,
    applyMiddleware(
        promiseMiddleware(),
        logger
    )
);

export default store;
