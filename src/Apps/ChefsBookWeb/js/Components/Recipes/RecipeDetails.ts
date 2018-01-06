
import * as React from "react";
import * as Router from "react-router";
import { connect, Dispatch } from "react-redux";

import { fetchRecipe } from "../../Actions/Recipes";
import { RecipeDetailsComponent } from "./RecipeDetailsComponent";

export interface RecipeDetailsStateProps {
    readonly recipe: Models.RecipeDetailsDTO;
}

export interface RecipeDetailsDispatchProps {
    readonly fetchRecipe: () => void;
    readonly goBack: () => void;
}

interface RecipeDetailsProps extends Router.RouteComponentProps<{ recipeId: string }> {
}

const mapStateToProps = (state: RecipesState, props: RecipeDetailsProps): RecipeDetailsStateProps => {
    let recipeId = props.match.params.recipeId;

    return {
        recipe: state.recipesDetails[recipeId]
    };
};

const mapDispatchToProps = (dispatch: Dispatch<any>, props: RecipeDetailsProps): RecipeDetailsDispatchProps => {
    let recipeId = props.match.params.recipeId;

    return {
        fetchRecipe: () => dispatch(fetchRecipe(recipeId)),
        goBack: props.history.goBack
    };
};

export const RecipeDetails = connect(
    mapStateToProps,
    mapDispatchToProps as any as RecipeDetailsDispatchProps
)(RecipeDetailsComponent);
