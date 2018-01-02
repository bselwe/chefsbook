const webpack = require("webpack");
const path = require("path");

const Merge = require('webpack-merge');
const CommonConfig = require('./webpack.common.js');

const HtmlWebpackPlugin = require("html-webpack-plugin");

module.exports = Merge(CommonConfig, {
    entry: [
        "react-hot-loader/patch",
        "webpack-dev-server/client?http://localhost:8080",
        "webpack/hot/only-dev-server",
    ],

    devtool: "eval-source-map",

    plugins: [
        new webpack.HotModuleReplacementPlugin(),
        new webpack.NamedModulesPlugin(),
        new webpack.NoEmitOnErrorsPlugin(),
    ],

    module: {
        rules: [
            {
                test: /\.css$/,
                use: "css-loader"
            },
            {
                test: /\.sass$/,
                use: [{
                    loader: "style-loader" // creates style nodes from JS strings
                }, {
                    loader: "css-loader" // translates CSS into CommonJS
                }, {
                    loader: "sass-loader" // compiles Sass to CSS
                }]
            }
        ]
    },

    devServer: {
        hot: true,
        contentBase: path.join(__dirname, "dist"),
        publicPath: "/",

        host: "localhost",
        port: 8080,
        historyApiFallback: true
    }
});
