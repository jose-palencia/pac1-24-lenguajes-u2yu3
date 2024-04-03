export const TodoForm = ({ addTask, task, setTask, mode, editTask }) => {

  const handleSubmit = (e) => {
    e.preventDefault();
    
    if(mode === 'C') {
      addTask();
    }

    if(mode === 'U') {
      editTask();
    }

  }

  return (
    <form onSubmit={handleSubmit} className="w-full mx-auto px-4 py-2">
            <div className="flex 
              items-center
              border-b-2
              border-teal-500
              py-2"
            >
              <input
                value={task.description}
                onChange={(v) => setTask({ ...task, description: v.target.value})}
                className="appearance-none 
                  bg-transparent
                  border-none
                  w-full
                  text-gray-700
                  mr-3
                  py-1
                  px-2
                  leading-tight
                  focus:outline-none"
                type="text"
                name="task"
                placeholder="Agregar Tarea"
              />
              <button 
                type="submit"
                className="flex-shrink-0 
                  bg-teal-500
                  hover:bg-teal-700
                  border-teal-500
                  hover:border-teal-700
                  text-sm
                  border-4
                  text-white
                  py-1
                  px-2
                  rounded">
                {`${ mode === 'C' ? 'Agregar' : mode === 'U' ? 'Editar' : '' }`}
              </button>
            </div>
          </form>
  )
}