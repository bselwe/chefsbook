declare interface AsyncState {
    readonly tasksCount: number;
}

declare interface AccountState {
    readonly isSigned: boolean;
    readonly signInError?: string;

    readonly user?: Models.UserInfoDTO;
}

declare interface RecipesState {
    readonly recipes: Models.RecipeDTO[];
    readonly recipesDetails: { [recipeId: string]: Models.RecipeDetailsDTO };
}

declare interface AppState extends
    AsyncState, AccountState, RecipesState {
}
