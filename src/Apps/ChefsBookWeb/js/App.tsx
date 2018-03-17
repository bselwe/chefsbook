import * as React from "react";
import * as Router from "react-router";

import { AppRouter } from "./AppRouter";
import { LoadingIndicator } from "./Components/Infrastructure/LoadingIndicator";
import { ScrollToTopOnRouteChange } from "./Components/Infrastructure/ScrollToTopOnRouteChange";

export class App extends React.Component {
    render() {
        return <div>
            <AppRouter />
            <LoadingIndicator />
            <ScrollToTopOnRouteChange />
        </div>;
    }
}
