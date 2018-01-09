
import * as React from "react";
import * as Router from "react-router";
import { connect, Dispatch } from "react-redux";

import { fetchRecipes, filterRecipes } from "../../Actions/Recipes";
import { RecipesComponent } from "./RecipesComponent";
import { fetchTags } from "../../Actions/Tags";

export interface RecipesStateProps {
    readonly recipes: Models.RecipeDTO[];
    readonly tags: Models.TagDTO[];
}

export interface RecipesDispatchProps {
    readonly fetchRecipes: () => void;
    readonly fetchTags: () => void;
    readonly filterRecipes: (text: string, tags: string[]) => void;
    readonly navigateToCreateRecipe: () => void;
    readonly navigateToRecipeDetails: (recipeId: string) => void;
}

interface RecipesProps extends Router.RouteComponentProps<{}> {
}

const mapStateToProps = (state: AppState, props: RecipesProps): RecipesStateProps => {
    return {
        recipes: state.recipes,
        tags: state.tags
    };
};

const mapDispatchToProps = (dispatch: Dispatch<any>, props: RecipesProps): RecipesDispatchProps => {
    return {
        fetchRecipes: () => dispatch(fetchRecipes()),
        fetchTags: () => dispatch(fetchTags()),
        filterRecipes: (text: string, tags: string[]) => dispatch(filterRecipes({ Text: text, Tags: tags })),
        navigateToCreateRecipe: () => props.history.push(`/recipes/new`),
        navigateToRecipeDetails: (recipeId: string) => props.history.push(`/recipe/${recipeId}`)
    };
};

export const Recipes = connect(
    mapStateToProps,
    mapDispatchToProps as any as RecipesDispatchProps
)(RecipesComponent);
