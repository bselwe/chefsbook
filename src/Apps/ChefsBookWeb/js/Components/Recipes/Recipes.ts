
import * as React from "react";
import * as Router from "react-router";
import { connect, Dispatch } from "react-redux";

import { fetchRecipes } from "../../Actions/Recipes";
import { RecipesComponent } from "./RecipesComponent";

export interface RecipesStateProps {
    readonly recipes: Models.RecipeDTO[];
}

export interface RecipesDispatchProps {
    readonly fetchRecipes: () => void;
    readonly navigateToRecipe: (recipeId: string) => void;
}

interface RecipesProps extends Router.RouteComponentProps<{}> {
}

const mapStateToProps = (state: AppState, props: RecipesProps): RecipesStateProps => {
    return {
        recipes: state.recipes,
    };
};

const mapDispatchToProps = (dispatch: Dispatch<any>, props: RecipesProps): RecipesDispatchProps => {
    return {
        fetchRecipes: () => dispatch(fetchRecipes()),
        navigateToRecipe: (recipeId: string) => props.history.push(`/recipe/${recipeId}`)
    };
};

export const Recipes = connect(
    mapStateToProps,
    mapDispatchToProps as any as RecipesDispatchProps
)(RecipesComponent);
