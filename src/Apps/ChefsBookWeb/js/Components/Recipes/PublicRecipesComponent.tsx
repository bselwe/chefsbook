import * as React from "react";
import * as Router from "react-router";
import { Card, Toolbar, ToolbarGroup, ToolbarTitle, CardText, GridList, GridTile } from "material-ui";
import { PublicRecipesStateProps, PublicRecipesDispatchProps } from "./PublicRecipes";
import { ContentAdd } from "material-ui/svg-icons";
import { RecipesList } from "./RecipesList";
import { RecipesFilters } from "./RecipesFilters";

type PublicRecipesComponentProps = PublicRecipesStateProps & PublicRecipesDispatchProps;

export class PublicRecipesComponent extends React.Component<PublicRecipesComponentProps> {
    componentDidMount() {
        this.props.filterRecipes("", []);
        this.props.fetchTags();
    }

    private handleFiltersChanged = (text: string, tags: string[]) => {
        this.props.filterRecipes(text, tags);
    }

    render() {
        return <Card style={{ maxWidth: 960, margin: "0 auto" }}>
            <Toolbar>
                <ToolbarGroup>
                    <ToolbarTitle text="Explore" />
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
        </Card>;
    }
}
