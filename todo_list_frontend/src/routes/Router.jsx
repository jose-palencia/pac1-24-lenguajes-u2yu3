import { createBrowserRouter } from "react-router-dom";
import PrivateRoutes from "./PrivateRuotes";

const Router = () => {
    return createBrowserRouter([
        PrivateRoutes()
    ])
}

export default Router;