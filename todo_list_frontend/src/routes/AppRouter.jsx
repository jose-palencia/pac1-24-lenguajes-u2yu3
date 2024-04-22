import { Route, Routes } from "react-router-dom"
import { LoginPage, TodoListPage } from "../pages"
import { PublicRoute } from "./PublicRoute"
import { PrivateRoute } from "./PrivateRoute"

export const AppRouter = () => {
  return (
    <>
        <Routes>
            <Route path='/login' element={
                <PublicRoute>
                    <LoginPage />
                </PublicRoute>
            } />
            <Route path="/" element={
                <PrivateRoute>
                    <TodoListPage />
                </PrivateRoute>
            } />
        </Routes>
    </>
  )
}
