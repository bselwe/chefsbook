import * as React from "react";
import * as Router from "react-router";
import { Card, Toolbar, ToolbarGroup, ToolbarTitle, CardText, GridList, GridTile, FloatingActionButton } from "material-ui";
import { RecipesStateProps, RecipesDispatchProps } from "./Recipes";
import { ContentAdd } from "material-ui/svg-icons";
import { RecipesList } from "./RecipesList";
import { RecipesFilters } from "./RecipesFilters";

type RecipesComponentProps = RecipesStateProps & RecipesDispatchProps;

export class RecipesComponent extends React.Component<RecipesComponentProps> {
    componentDidMount() {
        this.props.fetchRecipes();
        this.props.fetchTags();
    }

    private handleFiltersChanged = (text: string, tags: string[]) => {
        this.props.filterRecipes(text, tags);
    }

    render() {
        return <Card style={{ maxWidth: 960, margin: "0 auto" }}>
            <Toolbar>
                <ToolbarGroup>
                    <ToolbarTitle text="Recipes" />
                </ToolbarGroup>
            </Toolbar>

            <CardText>
                <RecipesFilters
                    tags={this.props.tags}
                    onFiltersChanged={this.handleFiltersChanged} />
                <RecipesList
                    recipes={this.props.recipes}
                    navigateToRecipeDetails={this.props.navigateToRecipeDetails} />
            </CardText>

            <FloatingActionButton style={{ position: "fixed", right: "100px", bottom: "60px", zIndex: 1000 }} onClick={this.props.navigateToCreateRecipe}>
                <ContentAdd />
            </FloatingActionButton>
        </Card>;
    }
}
