import { IoLogOutOutline } from 'react-icons/io5';
import { useNavigate } from 'react-router-dom';

export const LogoutButton = () => {
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.clear();
        navigate('/login')
    }

    return (
    <div className="relative">
        <button 
            type='button'
            onClick={handleLogout}
            className='z-20 text-white flex flex-col shrink-0 grow-0 justify-around
            fixed bottom-0 right-0 rounded-lg mr-5 mb-5 xl:mb-10 xl:mr-10'
        >
            <div className='p-3 rounded-full border-4 border-white
                bg-teal-500 hover:bg-teal-700 transition-colors'>
                <IoLogOutOutline
                    className='w-8 h-8 xl:w-12 xl:h-12'
                />
            </div>
        </button>
    </div>
  )
}