import { createAsyncAction, StartTask, EndTask } from "./AsyncActions";
import { apiClient } from "../Services/ApiClient";

const FetchCart = createAsyncAction("CART/FETCH", apiClient.fetchCart,
    (state: AppState, action): AppState => {
        if (action.payload && action.payload.IsSuccess) {
            let cart = action.payload.Response.reduce((prev, cur) => {
                return [...prev, ...cur];
            }, []);

            return {
                ...state,
                ...EndTask(state),
                cart
            };
        }
        return state;
    }
);

const AddToCart = createAsyncAction<AppState, Models.ApiResponse<{}, {}>, string>("CART/ADD", apiClient.addToCart);
const DeleteCart = createAsyncAction<AppState, Models.ApiResponse<{}, {}>>("CART/DELETE", apiClient.deleteCart);

export const fetchCart = FetchCart.action;
export const addToCart = AddToCart.action;
export const deleteCart = DeleteCart.action;

export const cartReducers = {
    ...FetchCart.reducers,
    ...AddToCart.reducers,
    ...DeleteCart.reducers
};
