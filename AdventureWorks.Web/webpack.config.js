module.exports = {
    entry:
    {
        shared: './src/shared.js',
        home: './src/home/home.js',
        customer: './src/customer/customer.js'
    },
    output: {
        filename: '../wwwroot/js/[name].js'
    },
    optimization: {
        splitChunks: {
            cacheGroups: {
                vendor: {
                    test: /[\\/]node_modules[\\/](jquery)[\\/]/,
                    name: 'vendor',
                    chunks: 'all'
                }
            }
        }
    }
};
