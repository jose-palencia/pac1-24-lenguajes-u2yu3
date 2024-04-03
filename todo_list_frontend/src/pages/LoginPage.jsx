import { useNavigate } from 'react-router-dom';

export const LoginPage = () => {
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();

    navigate('/'); 

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

                    <div className="flex flex-col space-y-1 border-b-2 border-teal-500">
                        <input
                            type="text"
                            name="username"
                            id="username"
                            className="appearance-none bg-transparent border-none w-full
                            text-gray-700 mr-3 py-1 px-2 leading-tight focus:outline-none"
                            placeholder="Usuario"
                        />
                    </div>
                    <div className="flex flex-col space-y-1 border-b-2 border-teal-500">
                        <input
                            type="password"
                            name="password"
                            id="password"
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