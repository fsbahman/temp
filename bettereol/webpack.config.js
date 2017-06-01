const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  entry: {
    javascript: './src/index.js'
  },

  plugins: [new HtmlWebpackPlugin({
    template: 'src/index.html'
  })],

  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'app.bundle.js'
  },

  resolve: {
    extensions: ['.js', '.jsx', '.json']
  },

  module: {
    loaders: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        loader: 'babel-loader',
        query:
        {
          presets: ['react', 'es2015']
        }
      }]
  },
};