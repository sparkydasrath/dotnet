import React, { useEffect, useState } from 'react';
import './App.css';
import ProductList from './ProductList';
import axios from 'axios';

function App() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    axios.get('/products')
      .then(response => setProducts(response.data))
      .catch(error => console.error('Error fetching products:', error));
  }, []);

  return (
    <>
      <h1>Products</h1>
      <ProductList products={products} />
    </>
  );
}

export default App;
