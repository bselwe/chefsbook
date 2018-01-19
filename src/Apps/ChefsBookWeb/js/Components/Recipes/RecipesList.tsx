import * as React from "react";
import { GridList, GridTile } from "material-ui";
import { IconButton } from "material-ui/IconButton";
import { ActionLock } from "material-ui/svg-icons";

interface RecipesListProps {
    readonly recipes: Models.RecipeDTO[];
    readonly navigateToRecipeDetails: (recipeId: string) => void;
}

export class RecipesList extends React.Component<RecipesListProps> {
    private getRecipeTitle(recipe: Models.RecipeDTO) {
        let name = recipe.Title;

        if (recipe.Tags && recipe.Tags.length > 0) {
            name += " (" + recipe.Tags.map(tag => tag.Name).join(", ") + ")";
        }

        return name;
    }

    render() {
        let recipes = this.props.recipes;

        return <GridList cols={2} cellHeight={240}>
            {
                recipes.map(recipe => (
                    <GridTile
                        key={recipe.Id}
                        title={this.getRecipeTitle(recipe)}
                        subtitle={recipe.Description}
                        style={{ cursor: "pointer" }}
                        actionIcon={recipe.IsPrivate ? <ActionLock color="white" style={{ marginRight: "1rem", paddingLeft: "1rem" }} /> : <div></div>}
                        onClick={(_) => this.props.navigateToRecipeDetails(recipe.Id)}>
                        <img src={recipe.Image} />
                    </GridTile>
                ))
            }
        </GridList>;
    }
}
