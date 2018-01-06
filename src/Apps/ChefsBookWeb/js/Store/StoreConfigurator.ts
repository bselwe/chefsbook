import { createStore, applyMiddleware } from "redux";
import { Store } from "redux";
import promiseMiddleware from "redux-promise-middleware";
import { Provider } from "react-redux";
import logger from "redux-logger";

import { handleActions } from "redux-actions";
import { loginManager } from "../Services/LoginManager";
import { accountReducers } from "../Actions/Account";
import { recipesReducers } from "../Actions/Recipes";

const reducers = {
    ...accountReducers,
    ...recipesReducers
};

const initialState: AppState = {
    tasksCount: 0,
    isSigned: loginManager.isSigned,
    recipes: [],
    recipesDetails: {}
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
