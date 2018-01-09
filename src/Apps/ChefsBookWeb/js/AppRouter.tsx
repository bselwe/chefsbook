import * as React from "react";
import * as Router from "react-router";
import * as routes from "./Routes";

import { WelcomePage } from "./Pages/WelcomePage";
import { NotFoundPage } from "./Pages/NotFoundPage";
import { Authentication } from "./Components/Infrastructure/Authentication";
import { Menu } from "./Components/Infrastructure/Menu";
import { Recipes } from "./Components/Recipes/Recipes";
import { RecipeDetails } from "./Components/Recipes/RecipeDetails";
import { CreateRecipe } from "./Components/Recipes/CreateRecipe";

export class AppRouter extends React.Component {
    render() {
        return <Authentication>
            <Menu>
                <Router.Switch>
                    <Router.Route path={routes.WelcomePage} exact component={WelcomePage} />
                    <Router.Route path={routes.RecipesList} exact render={props => <Recipes {...props} />} />
                    <Router.Route path={routes.CreateRecipe} render={props => <CreateRecipe {...props} />} />
                    <Router.Route path={routes.RecipeDetails} render={props => <RecipeDetails {...props} />} />
                    <Router.Route component={NotFoundPage} />
                </Router.Switch>
            </Menu>
        </Authentication>;
    }
}
