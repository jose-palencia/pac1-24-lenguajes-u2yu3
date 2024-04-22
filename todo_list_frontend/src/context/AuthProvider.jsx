import { useNavigate } from "react-router-dom";
import { AuthContext } from "./AuthContext"

const init = () => {
    const user = JSON.parse(localStorage.getItem('user'));

    return {
        logged: !!user,
        user: user
    }
}

export const AuthProvider = ({ children }) => {
    const navigate = useNavigate();
    const userPre = init();

    const login = (user) => {
        localStorage.setItem('user', JSON.stringify(user));
        navigate('/');
    }

    const logout = () => {
        localStorage.removeItem('user');
        // localStorage.clear();
    }

    return (
        <AuthContext.Provider value={{ ...userPre, login, logout }}>
            {children}
        </AuthContext.Provider>
    )
}
