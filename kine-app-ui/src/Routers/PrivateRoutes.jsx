import React from 'react'
import { Route, Routes } from 'react-router-dom'

export const PrivateRoutes = () => {
    return (
        <>
            <div>
                <h1>NAVIGATION BAR</h1>
            </div>
            <div className='container'>
                <Routes>

                    {/* home */}
                    <Route path='/' element={<>home</>} />

                </Routes>
            </div>
        </>
    )
}
