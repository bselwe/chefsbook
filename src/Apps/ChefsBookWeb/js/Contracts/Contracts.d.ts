declare module Models {
    interface UserInfoDTO {
        FirstName: string;
        LastName: string;
        Email: string;
        Photo: string;
    }

    interface RecipeDTO {
        Id: string;
        Title: string;
        Description: string;
        Image: string;
        Duration?: string;
        Servings?: string;
        Notes: string;
        Tags: TagDTO[];
    }

    interface RecipeDetailsDTO extends RecipeDTO {
        Ingredients: IngredientDTO[];
        Steps: StepDTO[];
    }

    interface TagDTO {
        Name: string;
    }

    interface IngredientDTO {
        Name: string;
        Quantity: string;
    }

    interface StepDTO {
        Duration?: string;
        Description: string;
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