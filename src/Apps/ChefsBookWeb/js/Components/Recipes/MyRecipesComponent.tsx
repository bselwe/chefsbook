import * as React from "react";
import * as Router from "react-router";
import { Card, Toolbar, ToolbarGroup, ToolbarTitle, CardText, GridList, GridTile, FloatingActionButton } from "material-ui";
import { MyRecipesStateProps, MyRecipesDispatchProps } from "./MyRecipes";
import { ContentAdd } from "material-ui/svg-icons";
import { RecipesList } from "./RecipesList";
import { RecipesFilters } from "./RecipesFilters";

type MyRecipesComponentProps = MyRecipesStateProps & MyRecipesDispatchProps;

export class MyRecipesComponent extends React.Component<MyRecipesComponentProps> {
    componentDidMount() {
        this.props.fetchUserRecipes();
    }

    render() {
        return <Card style={{ maxWidth: 960, margin: "0 auto" }}>
            <Toolbar>
                <ToolbarGroup>
                    <ToolbarTitle text="My recipes" />
                </ToolbarGroup>
            </Toolbar>

            <CardText>
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
