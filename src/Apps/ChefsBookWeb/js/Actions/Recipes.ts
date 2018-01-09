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
    }
);

const FilterRecipes = createAsyncAction("RECIPES/FILTER", apiClient.filterRecipes,
    (state: AppState, action): AppState => {
        if (action.payload && action.payload.IsSuccess) {
            return {
                ...state,
                ...EndTask(state),
                recipes: action.payload.Response
            };
        }
        return state;
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
    }
);

const CreateRecipe = createAsyncAction<AppState, Models.ApiResponse<{}, {}>, Models.NewRecipeDTO>("RECIPE/CREATE", apiClient.createRecipe);

const UpdateRecipe = createAsyncAction<AppState, Models.ApiResponse<{}, {}>, string, Models.UpdateRecipeDTO>("RECIPE/UPDATE", apiClient.updateRecipe);

const DeleteRecipe = createAsyncAction<AppState, Models.ApiResponse<{}, {}>, string>("RECIPE/DELETE", apiClient.deleteRecipe);

export const fetchRecipes = FetchRecipes.action;
export const filterRecipes = FilterRecipes.action;
export const fetchRecipe = FetchRecipe.action;
export const createRecipe = CreateRecipe.action;
export const updateRecipe = UpdateRecipe.action;
export const deleteRecipe = DeleteRecipe.action;

export const recipesReducers = {
    ...FetchRecipes.reducers,
    ...FilterRecipes.reducers,
    ...FetchRecipe.reducers,
    ...CreateRecipe.reducers,
    ...UpdateRecipe.reducers,
    ...DeleteRecipe.reducers
};
