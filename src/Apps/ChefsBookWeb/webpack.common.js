const webpack = require("webpack");
const path = require("path");

const HtmlWebpackPlugin = require("html-webpack-plugin");

const deployDir = process.env.DEPLOY_DIR ? process.env.DEPLOY_DIR : __dirname;

module.exports = {
    entry: [
        "./js/index.tsx",
        "./styles/app.sass"
    ],

    output: {
        path: path.join(deployDir, "dist"),
        filename: "bundle.js",
        publicPath: "/"
    },

    resolve: {
        extensions: [".ts", ".tsx", ".js"]
    },

    plugins: [
        new HtmlWebpackPlugin({
            template: path.join(__dirname, "index.html")
        }),
    ],

    module: {
        rules: [
            {
                test: /\.(ts|tsx)?$/,
                enforce: "pre",
                loader: "tslint-loader",
                options: {
                    failOnHint: true,
                    configFile: "tslint.json"
                }
            },
            {
                test: /\.(ts|tsx)?$/,
                loader: "awesome-typescript-loader",
                exclude: path.resolve(__dirname, "node_modules"),
                include: path.resolve(__dirname, "js"),
            },
            {
                test: /\.(jpg|png|svg)$/,
                loader: "url-loader?limit=256&name=images/[name].[ext]",
                exclude: path.resolve(__dirname, "node_modules"),
                include: path.resolve(__dirname, "images")
            }
        ]
    }
};
