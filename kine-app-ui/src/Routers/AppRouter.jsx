import React from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { PrivateRoutes } from './PrivateRoutes';

export const AppRouter = () => {
    return (
        <BrowserRouter>
            <Routes>

                {/* anonymous */}
                {/* <Route path='/login' element={ } /> */}

                {/* protected */}
                <Route path='/*' element={<PrivateRoutes />} />
            </Routes>
        </BrowserRouter>
    )
}
