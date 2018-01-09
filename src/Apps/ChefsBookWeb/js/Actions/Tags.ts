import { createAsyncAction, StartTask, EndTask } from "./AsyncActions";
import { apiClient } from "../Services/ApiClient";

const FetchTags = createAsyncAction("TAGS/FETCH", apiClient.fetchTags,
    (state: AppState, action): AppState => {
        if (action.payload && action.payload.IsSuccess) {
            return {
                ...state,
                ...EndTask(state),
                tags: action.payload.Response
            };
        }
        return state;
    }
);

export const fetchTags = FetchTags.action;

export const tagsReducers = {
    ...FetchTags.reducers
};
