import React from 'react'
import { Route, Routes } from 'react-router-dom'
import { AppointmentsScreen } from '../Components/Appointments/AppointmentsScreen'
import { NavBar } from '../Components/NavBar'

export const PrivateRoutes = () => {
    return (
        <>
            <NavBar />
            <div className='container'>
                <Routes>

                    {/* home */}
                    <Route path='/' element={<AppointmentsScreen />} />

                </Routes>
            </div>
        </>
    )
}
