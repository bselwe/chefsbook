
import * as React from "react";
import * as Router from "react-router";
import * as routes from "../../Routes";

import { connect, Dispatch } from "react-redux";
import { fetchRecipe, deleteRecipe } from "../../Actions/Recipes";
import { RecipeDetailsComponent } from "./RecipeDetailsComponent";
import { addToCart } from "../../Actions/Cart";

export interface RecipeDetailsStateProps {
    readonly recipe: Models.RecipeDetailsDTO;
}

export interface RecipeDetailsDispatchProps {
    readonly fetchRecipe: () => void;
    readonly deleteRecipe: () => void;
    readonly addToCart: (recipeId: string) => void;
    readonly navigateToEditRecipe: () => void;
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
        deleteRecipe: async () => {
            await dispatch(deleteRecipe(recipeId));
            props.history.replace(routes.MyRecipes);
        },
        addToCart: (recipeId: string) => dispatch(addToCart(recipeId)),
        navigateToEditRecipe: () => props.history.push(`/recipes/edit/${recipeId}`),
        goBack: props.history.goBack
    };
};

export const RecipeDetails = connect(
    mapStateToProps,
    mapDispatchToProps as any as RecipeDetailsDispatchProps
)(RecipeDetailsComponent);
