import config from "../Configuration/Config";
import { HttpClient } from "./HttpClient";

class ApiClient extends HttpClient {
    public fetchRecipes = () => {
        return this.get<Models.RecipeDTO[]>(
            `/api/recipes`);
    }

    public filterRecipes = (dto: Models.FilterRecipeDTO) => {
        return this.post<Models.RecipeDTO[]>(
            `/api/recipes/filter`, dto);
    }

    public fetchRecipe = (recipeId: string) => {
        return this.get<Models.RecipeDetailsDTO>(
            `/api/recipes/${recipeId}`);
    }

    public createRecipe = (dto: Models.NewRecipeDTO) => {
        return this.post<{}>(
            `/api/recipes`, dto);
    }

    public updateRecipe = (recipeId: string, dto: Models.UpdateRecipeDTO) => {
        return this.put<{}>(
            `/api/recipes/${recipeId}`, dto);
    }

    public deleteRecipe = (recipeId: string) => {
        return this.delete<{}>(
            `/api/recipes/${recipeId}`);
    }

    public fetchTags = () => {
        return this.get<Models.TagDTO[]>(
            `/api/tags`);
    }

    public fetchCart = () => {
        return this.get<Models.IngredientDTO[][]>(
            `/api/cart`);
    }

    public addToCart = (recipeId: string) => {
        return this.post<{}>(
            `/api/cart/${recipeId}`);
    }

    public deleteCart = () => {
        return this.delete<{}>(
            `/api/cart`);
    }
}

export const apiClient = new ApiClient(config.apiEndpoint);
