import * as React from "react";
import * as Router from "react-router";

import { connect } from "react-redux";
import { Dispatch } from "redux";
import { Card, Toolbar, ToolbarGroup, ToolbarTitle, CardHeader, CardText, IconButton, RefreshIndicator } from "material-ui";
import { updateRecipe, fetchRecipes, fetchRecipe } from "../../Actions/Recipes";
import { NavigationArrowBack } from "material-ui/svg-icons";
import { fetchTags } from "../../Actions/Tags";
import { RecipeEditor } from "./RecipeEditor";

interface EditRecipeProps extends Router.RouteComponentProps<{ recipeId: string }> {
}

interface EditRecipeStateProps {
    readonly recipe: Models.RecipeDetailsDTO;
    readonly tags: Models.TagDTO[];
}

interface EditRecipeDispatchProps {
    readonly updateRecipe: (recipe: Models.UpdateRecipeDTO) => void;
    readonly fetchRecipe: () => void;
    readonly fetchTags: () => void;
    readonly goBack: () => void;
}

type EditRecipeComponentProps = EditRecipeStateProps & EditRecipeDispatchProps;

class EditRecipeComponent extends React.Component<EditRecipeComponentProps> {
    componentDidMount() {
        this.props.fetchRecipe();
        this.props.fetchTags();
    }

    private saveRecipe = (recipe: Models.UpdateRecipeDTO) => {
        this.props.updateRecipe(recipe);
    }

    private mapRecipe = (recipe: Models.RecipeDetailsDTO): Models.UpdateRecipeDTO => {
        return {
            Title: recipe.Title,
            Description: recipe.Description,
            Image: recipe.Image,
            Duration: recipe.Duration,
            Servings: recipe.Servings,
            Notes: recipe.Notes,
            Ingredients: recipe.Ingredients,
            Steps: recipe.Steps,
            Tags: recipe.Tags
        };
    }

    render() {
        return <Card style={{ maxWidth: 960, margin: "0 auto" }}>
            <Toolbar>
                <ToolbarGroup>
                    <ToolbarTitle text={"Edit"} />
                </ToolbarGroup>
                <ToolbarGroup>
                    <IconButton tooltip={"Back"} onClick={this.props.goBack}>
                        <NavigationArrowBack />
                    </IconButton>
                </ToolbarGroup>
            </Toolbar>

            <CardText>
                {this.props.recipe ?
                    <RecipeEditor recipe={this.mapRecipe(this.props.recipe)} tags={this.props.tags} saveRecipe={this.saveRecipe} /> :
                    <RefreshIndicator left={0} size={50} top={0} status="loading" style={{ position: "relative", display: "inline-block" }} />
                }
            </CardText>
        </Card>;
    }
}

const mapStateToProps = (state: RecipesState & TagsState, props: EditRecipeProps): EditRecipeStateProps => {
    let recipeId = props.match.params.recipeId;

    return {
        recipe: state.recipesDetails[recipeId],
        tags: state.tags
    };
};

const mapDispatchToProps = (dispatch: Dispatch<any>, props: EditRecipeProps): EditRecipeDispatchProps => {
    let recipeId = props.match.params.recipeId;

    return {
        updateRecipe: async (recipe: Models.NewRecipeDTO) => {
            await dispatch(updateRecipe(recipeId, recipe));
            await dispatch(fetchRecipes());
            props.history.goBack();
        },
        fetchRecipe: () => dispatch(fetchRecipe(recipeId)),
        fetchTags: () => dispatch(fetchTags()),
        goBack: props.history.goBack
    };
};

export const EditRecipe = connect(
    mapStateToProps,
    mapDispatchToProps as any as EditRecipeDispatchProps
)(EditRecipeComponent);
