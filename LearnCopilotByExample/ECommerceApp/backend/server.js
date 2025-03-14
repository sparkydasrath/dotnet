// filepath: app.js
const express = require('express');
const cookieParser = require('cookie-parser');
const products = require('./product-test-data.js');
const app = express();
const port = 3000;


// Middleware to parse JSON bodies
app.use(express.json());

// Middleware to parse cookies
app.use(cookieParser());

app.get('/', (req, res) => {
    res.send('Hello World!');
});

// Endpoint to load a list of all products in a user's cart
app.get('/cart', (req, res) => {
    // Logic to load products in the user's cart
    res.send('List of products in the cart');
});

// Endpoint to load all the site's products
app.get('/products', (req, res) => {
    res.json(products);
});

// Endpoint to add products to a user's cart
app.post('/cart', (req, res) => {
    // Logic to add a product to the user's cart
    res.send('Product added to the cart');
});

app.listen(port, () => {
    console.log(`Example app listening at http://localhost:${port}`);
});