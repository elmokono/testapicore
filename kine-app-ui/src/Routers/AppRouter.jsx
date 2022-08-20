import React from 'react'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { PrivateRoutes } from './PrivateRoutes';

export const AppRouter = () => {
    return (
        <BrowserRouter>
            <Routes>

                {/* anonymous */}
                <Route path='/login' element={ <><h1>LOGIN SCREEN</h1></> } />

                {/* protected */}
                <Route path='/*' element={<PrivateRoutes />} />
                
            </Routes>
        </BrowserRouter>
    )
}
