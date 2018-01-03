import * as React from "react";
import * as ReactDOM from "react-dom";
import * as RouterDOM from "react-router-dom";

import MuiThemeProvider from "material-ui/styles/MuiThemeProvider";

import { App } from "./App";
import { getMuiTheme, MuiTheme } from "material-ui/styles";
import { AppContainer } from "react-hot-loader";

import store from "./Store/StoreConfigurator";
import { Provider } from "react-redux";

const render = () => ReactDOM.render(
    <AppContainer>
        <MuiThemeProvider>
            <RouterDOM.BrowserRouter>
                <Provider store={store}>
                    <App />
                </Provider>
            </RouterDOM.BrowserRouter>
        </MuiThemeProvider>
    </AppContainer>,
    document.getElementById("root")
);

let instance = render();
declare var module: { hot: any };

if (module.hot) {
    module.hot.accept("./App", () => instance = render());
}
