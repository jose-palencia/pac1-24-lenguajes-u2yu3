import { Navigate } from "react-router-dom";
import { Layout } from "../components";
import { LoginPage, TodoListPage } from "../pages";

const PrivateRoutes = () => {
    return {
        element: <Layout />,
        children: [
            {
                path: "/",
                element: <TodoListPage />
            },
            {
                path: "/login",
                element: <LoginPage />
            },
            {
                path: "*",
                element: <Navigate to={'/'} replace />
            }
        ]
    };
}
export default PrivateRoutes;