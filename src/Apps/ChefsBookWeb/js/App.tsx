import * as React from "react";
import * as Router from "react-router";

import { AppRouter } from "./AppRouter";
import { LoadingIndicator } from "./Components/Infrastructure/LoadingIndicator";

export class App extends React.Component {
    render() {
        return <div>
            <AppRouter />
            <LoadingIndicator />
        </div>;
    }
}
