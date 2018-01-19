declare module Models {
    interface UserInfoDTO {
        Id: string;
        FirstName: string;
        LastName: string;
        Email: string;
        Photo: string;
    }

    interface RecipeDTO {
        IsPrivate: boolean;
        Id: string;
        UserId: string;
        Title: string;
        Description: string;
        Image: string;
        Duration?: string;
        Servings?: number;
        Notes: string;
        Tags: TagDTO[];
    }

    interface RecipeDetailsDTO extends RecipeDTO {
        Ingredients: IngredientDTO[];
        Steps: StepDTO[];
    }

    interface IngredientDTO {
        Name: string;
        Quantity: string;
    }

    interface StepDTO {
        Duration?: string;
        Description: string;
    }

    interface TagDTO {
        Name: string;
    }

    interface NewRecipeDTO {
        IsPrivate: boolean;
        Title: string;
        Description: string;
        Image: string;
        Duration?: string;
        Servings?: number;
        Notes: string;
        Ingredients: NewRecipeIngredientDTO[];
        Steps: NewRecipeStepDTO[];
        Tags: NewRecipeTagDTO[];
    }

    interface NewRecipeIngredientDTO {
        Name: string;
        Quantity: string;
    }

    interface NewRecipeStepDTO {
        Duration?: string;
        Description: string;
    }

    interface NewRecipeTagDTO {
        Name: string;
    }

    interface UpdateRecipeDTO {
        IsPrivate: boolean;
        Title: string;
        Description: string;
        Image: string;
        Duration?: string;
        Servings?: number;
        Notes: string;
        Ingredients: UpdateRecipeIngredientDTO[];
        Steps: UpdateRecipeStepDTO[];
        Tags: UpdateRecipeTagDTO[];
    }

    interface UpdateRecipeIngredientDTO {
        Name: string;
        Quantity: string;
    }

    interface UpdateRecipeStepDTO {
        Duration?: string;
        Description: string;
    }

    interface UpdateRecipeTagDTO {
        Name: string;
    }

    interface FilterRecipeDTO {
        Text: string;
        Tags: string[];
    }

    interface ResponseSuccess<TResponse> {
        IsSuccess: true;
        StatusCode: number;
        Response: TResponse;
    }

    interface ResponseError<TError> {
        IsSuccess: false;
        Error: TError;
        StatusCode: number;
    }

    type ApiResponse<TResponse extends {}, TError extends {}> = ResponseSuccess<TResponse> | ResponseError<TError>;
}