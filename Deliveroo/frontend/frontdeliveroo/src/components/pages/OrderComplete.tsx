import React from 'react';
import {useParams} from "react-router-dom";

const OrderComplete = () => {
    let params=useParams();
    return (
        <div>
            Order number - {params.orderId}
        </div>
    );
};

export default OrderComplete;
