import React from 'react';

const ProductListItem = ({ id, name, description, price, addToCart }) => {
    return (
        <div className="product-list-item">
            <h2>{name}</h2>
            <p>{description}</p>
            <p>Price: ${price.toFixed(2)}</p>
            <button onClick={() => addToCart(id)}>Add to Cart</button>
        </div>
    );
};

export default ProductListItem;