import * as React from "react";
import * as Router from "react-router";
import * as routes from "../../Routes";
import { Card, Toolbar, ToolbarGroup, ToolbarTitle, CardText, GridList, GridTile } from "material-ui";
import { RecipesStateProps, RecipesDispatchProps } from "./Recipes";
import * as moment from "moment";

type RecipesComponentProps = RecipesStateProps & RecipesDispatchProps;

export class RecipesComponent extends React.Component<RecipesComponentProps> {
    componentDidMount() {
        this.props.fetchRecipes();
    }

    private getRecipeTitle(recipe: Models.RecipeDTO) {
        let name = recipe.Title;

        if (recipe.Tags && recipe.Tags.length > 0) {
            name += " (" + recipe.Tags.map(tag => tag.Name).join(", ") + ")";
        }

        return name;
    }

    render() {
        let recipes = this.props.recipes;

        return <Card style={{ maxWidth: 960, margin: "0 auto" }}>
            <Toolbar>
                <ToolbarGroup>
                    <ToolbarTitle text="Recipes" />
                </ToolbarGroup>
            </Toolbar>

            <CardText>
                <GridList cols={2} cellHeight={240}>
                    {
                        recipes.map(recipe => (
                            <GridTile
                                key={recipe.Id}
                                title={this.getRecipeTitle(recipe)}
                                subtitle={recipe.Description}
                                style={{ cursor: "pointer" }}
                                onClick={(_) => this.props.navigateToRecipe(recipe.Id)}>
                                <img src={recipe.Image} />
                            </GridTile>
                        ))
                    }
                </GridList>
            </CardText>
        </Card>;
    }
}
