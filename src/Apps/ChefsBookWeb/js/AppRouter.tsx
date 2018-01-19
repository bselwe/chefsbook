import * as React from "react";
import * as Router from "react-router";
import * as routes from "./Routes";

import { WelcomePage } from "./Pages/WelcomePage";
import { NotFoundPage } from "./Pages/NotFoundPage";
import { Authentication } from "./Components/Infrastructure/Authentication";
import { Menu } from "./Components/Infrastructure/Menu";
import { MyRecipes } from "./Components/Recipes/MyRecipes";
import { RecipeDetails } from "./Components/Recipes/RecipeDetails";
import { CreateRecipe } from "./Components/Recipes/CreateRecipe";
import { EditRecipe } from "./Components/Recipes/EditRecipe";
import { Cart } from "./Components/Cart/Cart";
import { PublicRecipes } from "./Components/Recipes/PublicRecipes";

export class AppRouter extends React.Component {
    render() {
        return <Authentication>
            <Menu>
                <Router.Switch>
                    <Router.Route path={routes.WelcomePage} exact component={WelcomePage} />
                    <Router.Route path={routes.MyRecipes} exact render={props => <MyRecipes {...props} />} />
                    <Router.Route path={routes.PublicRecipes} exact render={props => <PublicRecipes {...props} />} />
                    <Router.Route path={routes.CreateRecipe} exact render={props => <CreateRecipe {...props} />} />
                    <Router.Route path={routes.EditRecipe} render={props => <EditRecipe {...props} />} />
                    <Router.Route path={routes.RecipeDetails} render={props => <RecipeDetails {...props} />} />
                    <Router.Route path={routes.Cart} render={props => <Cart {...props} />} />
                    <Router.Route component={NotFoundPage} />
                </Router.Switch>
            </Menu>
        </Authentication>;
    }
}
