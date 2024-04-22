import { useNavigate } from 'react-router-dom';
import { InputEmailValidation } from '../validations/input-email';
import { useContext, useState } from 'react';
// import useLocalStorage from '../hooks/useLocalStorage';
import { InputRequiredValidation } from '../validations/input-required';
import { Errors } from '../components'
import { constants } from '../helpers/constants';
import { AuthContext } from '../context/AuthContext';

export const LoginPage = () => {
  const [loginForm, setLogin] = useState({
    email: '',
    password: ''
  });
  const [errors, setErrors] = useState([]);
//   const [token, setToken] = useLocalStorage('token', '');
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const { API_URL } = constants();

  const handleSubmit = async (e) => {
    e.preventDefault();

    let newErrors = [];

    const errorEmail = InputEmailValidation('Correo ELectrónico', loginForm.email);
    if(!errorEmail.validation) {
        newErrors.push(errorEmail.message);
    }

    const errorEmailRequired = InputRequiredValidation('Correo ELectrónico', loginForm.email);
    if(!errorEmailRequired.validation) {
        newErrors.push(errorEmailRequired.message);
    }

    const errorPasswordRequired = InputRequiredValidation('Contraseña', loginForm.password);
    if(!errorPasswordRequired.validation) {
        newErrors.push(errorPasswordRequired.message);
    }

    setErrors(newErrors);

    //console.log(errors);
    if(errors.length === 0) {
        //console.log('no hay errores')
        try {
            const response = await fetch(`${API_URL}/auth/login`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(loginForm)
            });

            if(!response.ok) {
                throw new Error('Error en el inicio de sesión');
            } 

            const result = await response.json();
            login(result.data);

        } catch (error) {
          console.log(error);  
        }

    }


  }

  return (
    <div className="h-full antialiased bg-gray-800 pb-5 ">
        <div className="flex flex-col justify-center sm:w-96 sm:m-auto mx-5 mb-5 space-y-8">
            <h1 className="font-bold text-center text-4xl text-teal-500">
                Lista {''}
                <span className="text-white">de Tareas</span>
            </h1>
            <form onSubmit={handleSubmit} >
                <div className="flex flex-col bg-white p-10 rounded-lg shadow space-y-6">
                    <h2 className="font-bold text-gray-600 text-xl text-center">
                        Ingrese a su Cuenta
                    </h2>

                    {errors.length > 0 ? <Errors errorList={errors} /> : null}

                    <div className="flex flex-col space-y-1 border-b-2 border-teal-500">
                        <input
                            type="email"
                            name="username"
                            autoComplete='off'
                            value={loginForm.email}
                            onChange={(e) => setLogin( {...loginForm, email: e.target.value})}
                            required
                            id="username"
                            className="appearance-none bg-transparent border-none w-full
                            text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none"
                            placeholder="Usuario"
                        />
                    </div>
                    <div className="flex flex-col space-y-1 border-b-2 border-teal-500">
                        <input
                            value={loginForm.password}
                            onChange={(e) => setLogin({ ...loginForm, password: e.target.value })}
                            type="password"
                            name="password"
                            id="password"
                            required
                            className="appearance-none bg-transparent border-none w-full
                            text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none"
                            placeholder="Contraseña"
                        />
                    </div>

                    <div className="flex flex-row justify-between content-center    
                        items-center">
                        <button 
                            type="submit"
                            className="bg-teal-500 w-full text-white font-bold rounded 
                        focus:outline-none shadow hover:bg-teal-700 transition-colors
                        px-5 py-2">
                            Iniciar Sesión
                        </button>
                    </div>
                </div>
                
            </form>
        </div>
    </div>
  )
}