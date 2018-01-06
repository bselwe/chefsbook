import { createAsyncAction, StartTask, EndTask } from "./AsyncActions";
import { apiClient } from "../Services/ApiClient";

const FetchRecipes = createAsyncAction("RECIPES/FETCH", apiClient.fetchRecipes,
    (state: AppState, action): AppState => {
        if (action.payload && action.payload.IsSuccess) {
            return {
                ...state,
                ...EndTask(state),
                recipes: action.payload.Response
            };
        }
        return state;
    },
    (state: AppState): AppState => {
        return {
            ...state,
            ...StartTask(state)
        };
    },
    (state: AppState): AppState => {
        return {
            ...state,
            ...EndTask(state)
        };
    }
);

const FetchRecipe = createAsyncAction("RECIPE/FETCH", apiClient.fetchRecipe,
    (state: AppState, action): AppState => {
        if (action.payload && action.payload.IsSuccess) {
            return {
                ...state,
                ...EndTask(state),
                recipesDetails: {
                    ...state.recipesDetails,
                    [action.payload.Response.Id]: action.payload.Response
                }
            };
        }
        return state;
    },
    (state: AppState): AppState => {
        return {
            ...state,
            ...StartTask(state)
        };
    },
    (state: AppState): AppState => {
        return {
            ...state,
            ...EndTask(state)
        };
    }
);

export const fetchRecipes = FetchRecipes.action;
export const fetchRecipe = FetchRecipe.action;

export const recipesReducers = {
    ...FetchRecipes.reducers,
    ...FetchRecipe.reducers
};
