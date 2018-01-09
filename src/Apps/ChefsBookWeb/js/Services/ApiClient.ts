import config from "../Configuration/Config";
import { HttpClient } from "./HttpClient";

class ApiClient extends HttpClient {
    public fetchRecipes = () => {
        return this.get<Models.RecipeDTO[]>(
            `/api/recipes`);
    }

    public fetchRecipe = (recipeId: string) => {
        return this.get<Models.RecipeDetailsDTO>(
            `/api/recipes/${recipeId}`);
    }

    public createRecipe = (dto: Models.NewRecipeDTO) => {
        return this.post<{}>(
            `/api/recipes`, dto);
    }

    public updateRecipe = (dto: Models.UpdateRecipeDTO) => {
        return this.put<{}>(
            `/api/recipes`, dto);
    }

    public fetchTags = () => {
        return this.get<Models.TagDTO[]>(
            `/api/tags`);
    }
}

export const apiClient = new ApiClient(config.apiEndpoint);
