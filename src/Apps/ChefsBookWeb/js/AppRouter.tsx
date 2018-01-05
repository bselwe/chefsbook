import * as React from "react";
import * as Router from "react-router";
import * as routes from "./Routes";

import { WelcomePage } from "./Pages/WelcomePage";
import { NotFoundPage } from "./Pages/NotFoundPage";
import { Authentication } from "./Components/Infrastructure/Authentication";
import { Menu } from "./Components/Infrastructure/Menu";

export class AppRouter extends React.Component {
    render() {
        return <Authentication>
            <Menu>
                <Router.Switch>
                    <Router.Route path={routes.WelcomePage} component={WelcomePage} />
                    <Router.Route component={NotFoundPage} />
                </Router.Switch>
            </Menu>
        </Authentication>;
    }
}
