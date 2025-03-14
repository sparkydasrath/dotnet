import React from 'react';
import PropTypes from 'prop-types';
import ProductListItem from './ProductListItem';

const ProductList = ({ products }) => {
    return (
        <div>
            {products.map(product => (
                <ProductListItem key={product.id} id={product.id} name={product.name} description={product.description} price={product.price} />
            ))}
        </div>
    );
};

ProductList.propTypes = {
    products: PropTypes.arrayOf(PropTypes.object).isRequired,
};

export default ProductList;