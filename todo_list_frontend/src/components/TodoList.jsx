import { FaRegEdit } from 'react-icons/fa';
import { TiDelete } from 'react-icons/ti';

export const TodoList = ({todoListItems, setStatus, setTask, setMode, deleteTask}) => {

  const handleEdit = (item) => {
    setTask(item);
    setMode('U')
  }
  const handleDelete = (item) => {
    const yesno = confirm('¿Desea eliminar la tareas?');
    if(yesno === true) {
        deleteTask(item);
    }
  }

  return (
    <ul className="divide-y divide-gray-200 px-4">
        {
            todoListItems.map((item) => (
                <li key={item.id} className="py-4 flex items-center justify-between">
                    <div 
                        className="flex items-center">
                        <input 
                            type="checkbox" 
                            name={item.id} 
                            id={item.id}
                            defaultChecked={item.done}
                            onClick={() => setStatus(item)}
                            className="h-4 w-4 text-teal-600 focus:ring-teal-900
                             border-gray-300 rounded "
                        />
                        <label htmlFor={item.id} className="ml-3 block text-gray-700">
                        <span className="text-lg font-medium">{item.description}</span>
                        </label>
                    </div>
                    <div className="flex">
                        <button 
                            onClick={() => handleEdit(item)}
                            className="flex-shrink-0 bg-teal-500 hover:bg-teal-700 border-teal-500 hover:border-teal-700 text-sm border-4 text-white py-1 px-2 rounded mr-2">
                            <FaRegEdit />
                        </button>
                        <button 
                            onClick={() => handleDelete(item)}
                            className="flex-shrink-0 bg-red-500 hover:bg-red-700 border-red-500 hover:border-red-700 text-sm border-4 text-white py-1 px-2 rounded">
                            <TiDelete />
                        </button>
                    </div>
                </li>
            ))
        }
        
    </ul>
  )
}
