import * as React from "react";
import * as Router from "react-router";

import { connect } from "react-redux";
import { Dispatch } from "redux";
import { Card, Toolbar, ToolbarGroup, ToolbarTitle, CardHeader, CardText, IconButton } from "material-ui";
import { createRecipe, fetchRecipes } from "../../Actions/Recipes";
import { NavigationArrowBack } from "material-ui/svg-icons";
import { fetchTags } from "../../Actions/Tags";
import { RecipeEditor } from "./RecipeEditor";

interface CreateRecipeProps extends Router.RouteComponentProps<{}> {
}

interface CreateRecipeStateProps {
    readonly tags: Models.TagDTO[];
}

interface CreateRecipeDispatchProps {
    readonly createRecipe: (recipe: Models.NewRecipeDTO) => void;
    readonly fetchTags: () => void;
    readonly goBack: () => void;
}

type CreateRecipeComponentProps = CreateRecipeStateProps & CreateRecipeDispatchProps;

class CreateRecipeComponent extends React.Component<CreateRecipeComponentProps> {
    componentDidMount() {
        this.props.fetchTags();
    }

    private saveRecipe = (recipe: Models.NewRecipeDTO) => {
        this.props.createRecipe(recipe);
    }

    render() {
        let recipe: Models.NewRecipeDTO = {
            Title: "",
            Description: "",
            Image: "",
            Notes: "",
            Ingredients: [],
            Steps: [],
            Tags: []
        };

        return <Card style={{ maxWidth: 960, margin: "0 auto" }}>
            <Toolbar>
                <ToolbarGroup>
                    <ToolbarTitle text={"Create recipe"} />
                </ToolbarGroup>
                <ToolbarGroup>
                    <IconButton tooltip={"Back"} onClick={this.props.goBack}>
                        <NavigationArrowBack />
                    </IconButton>
                </ToolbarGroup>
            </Toolbar>

            <CardText>
                <RecipeEditor recipe={recipe} tags={this.props.tags} saveRecipe={this.saveRecipe} />
            </CardText>
        </Card>;
    }
}

const mapStateToProps = (state: TagsState, props: CreateRecipeProps): CreateRecipeStateProps => {
    return {
        tags: state.tags
    };
};

const mapDispatchToProps = (dispatch: Dispatch<any>, props: CreateRecipeProps): CreateRecipeDispatchProps => {
    return {
        createRecipe: async (recipe: Models.NewRecipeDTO) => {
            await dispatch(createRecipe(recipe));
            await dispatch(fetchRecipes());
            props.history.replace(`/recipes`);
        },
        fetchTags: async () => await dispatch(fetchTags()),
        goBack: props.history.goBack
    };
};

export const CreateRecipe = connect(
    mapStateToProps,
    mapDispatchToProps as any as CreateRecipeDispatchProps
)(CreateRecipeComponent);
