
import * as React from "react";
import * as Router from "react-router";
import { connect, Dispatch } from "react-redux";

import { fetchUserRecipes, filterRecipes } from "../../Actions/Recipes";
import { PublicRecipesComponent } from "./PublicRecipesComponent";
import { fetchTags } from "../../Actions/Tags";

export interface PublicRecipesStateProps {
    readonly recipes: Models.RecipeDTO[];
    readonly tags: Models.TagDTO[];
}

export interface PublicRecipesDispatchProps {
    readonly fetchTags: () => void;
    readonly filterRecipes: (text: string, tags: string[]) => void;
    readonly navigateToRecipeDetails: (recipeId: string) => void;
}

interface PublicRecipesProps extends Router.RouteComponentProps<{}> {
}

const mapStateToProps = (state: AppState, props: PublicRecipesProps): PublicRecipesStateProps => {
    return {
        recipes: state.recipes,
        tags: state.tags
    };
};

const mapDispatchToProps = (dispatch: Dispatch<any>, props: PublicRecipesProps): PublicRecipesDispatchProps => {
    return {
        fetchTags: () => dispatch(fetchTags()),
        filterRecipes: (text: string, tags: string[]) => dispatch(filterRecipes({ Text: text, Tags: tags })),
        navigateToRecipeDetails: (recipeId: string) => props.history.push(`/recipe/${recipeId}`)
    };
};

export const PublicRecipes = connect(
    mapStateToProps,
    mapDispatchToProps as any as PublicRecipesDispatchProps
)(PublicRecipesComponent);
