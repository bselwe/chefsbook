import config from "../Configuration/Config";
import { HttpClient } from "./HttpClient";

class ApiClient extends HttpClient {
    public fetchRecipes = () => {
        return this.get<Models.RecipeDTO[]>(
            `/api/recipes`
        );
    }

    public fetchRecipe = (recipeId: string) => {
        return this.get<Models.RecipeDetailsDTO>(
            `/api/recipes/${recipeId}`
        );
    }
}

export const apiClient = new ApiClient(config.apiEndpoint);
