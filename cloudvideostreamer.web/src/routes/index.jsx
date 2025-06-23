import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Home from '../pages/Home';
import About from '../pages/About/Index';
import Layout from '../components/layout/layouts/Layout';
import NotFound from '../pages/Error/NotFound';
import Login from '../pages/public/Login/Index';
import PrivateRoute from '../pages/PrivateRoute';

const routes = [
    {
        path: '/',
        element: (
            <PrivateRoute>
                <Layout />
            </PrivateRoute>
        ),
        errorElement: (
            <PrivateRoute>
                <NotFound/>
            </PrivateRoute>
        ),
        children: [
            {
                index: true,
                element: (
                    <PrivateRoute>
                        <Home/>
                    </PrivateRoute>
                )
            },
            {
                path: '/about',
                element: (
                    <PrivateRoute>
                        <About/>
                    </PrivateRoute>
                )
            }]
    },
    {
        path: '/login',
        element: <Login/>
    }]

export const router = createBrowserRouter(routes);