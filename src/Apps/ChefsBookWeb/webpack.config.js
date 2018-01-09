const webpack = require("webpack");

const Merge = require('webpack-merge');
const CommonConfig = require('./webpack.common.js');

const ExtractTextPlugin = require("extract-text-webpack-plugin");

module.exports = Merge(CommonConfig, {
    devtool: "eval-source-map",

    plugins: [
        new ExtractTextPlugin({ filename: "styles.css", allChunks: true }),
    ],

    module: {
        rules: [
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({
                    fallback: "style-loader", use: "css-loader",
                })
            },
            {
                test: /\.sass$/,
                use: ExtractTextPlugin.extract({
                    fallback: "style-loader", use: "css-loader!sass-loader",
                })
            }
        ]
    }
});
