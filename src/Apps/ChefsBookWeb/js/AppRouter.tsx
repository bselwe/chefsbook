import * as React from "react";
import * as Router from "react-router";
import * as routes from "./Routes";

import { HomePage } from "./Pages/HomePage";
import { NotFoundPage } from "./Pages/NotFoundPage";
import { Authentication } from "./Components/Infrastructure/Authentication";

export class AppRouter extends React.Component {
    render() {
        return <Authentication>
            <Router.Switch>
                <Router.Route path={routes.HomePage} component={HomePage} />
                <Router.Route component={NotFoundPage} />
            </Router.Switch>
        </Authentication>;
    }
}
