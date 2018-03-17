
import * as React from "react";
import * as Router from "react-router";
import { connect, Dispatch } from "react-redux";

import { fetchUserRecipes } from "../../Actions/Recipes";
import { MyRecipesComponent } from "./MyRecipesComponent";
import { fetchTags } from "../../Actions/Tags";

export interface MyRecipesStateProps {
    readonly recipes: Models.RecipeDTO[];
}

export interface MyRecipesDispatchProps {
    readonly fetchUserRecipes: () => void;
    readonly navigateToCreateRecipe: () => void;
    readonly navigateToRecipeDetails: (recipeId: string) => void;
}

interface MyRecipesProps extends Router.RouteComponentProps<{}> {
}

const mapStateToProps = (state: AppState, props: MyRecipesProps): MyRecipesStateProps => {
    return {
        recipes: state.recipes
    };
};

const mapDispatchToProps = (dispatch: Dispatch<any>, props: MyRecipesProps): MyRecipesDispatchProps => {
    return {
        fetchUserRecipes: () => dispatch(fetchUserRecipes()),
        navigateToCreateRecipe: () => props.history.push(`/recipes/new`),
        navigateToRecipeDetails: (recipeId: string) => props.history.push(`/recipe/${recipeId}`)
    };
};

export const MyRecipes = connect(
    mapStateToProps,
    mapDispatchToProps as any as MyRecipesDispatchProps
)(MyRecipesComponent);
