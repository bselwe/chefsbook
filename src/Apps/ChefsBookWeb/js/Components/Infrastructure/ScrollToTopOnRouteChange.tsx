import * as React from "react";
import * as Router from "react-router";

export const ScrollToTopOnRouteChange = Router.withRouter(props => {
    props.history.listen(() => window.scrollTo(0, 0));
    return null;
});
