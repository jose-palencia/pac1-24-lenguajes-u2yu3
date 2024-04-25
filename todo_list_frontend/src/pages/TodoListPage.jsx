import { useContext, useEffect, useState } from "react"
import { Errors, LogoutButton, TodoForm, TodoList } from "../components"
import { AuthContext } from '../context/AuthContext';

export const TodoListPage = () => {

  const [todoListItems, setTodoListItems] = useState([]);
  const [task, setTask] = useState({description: '', done: false});
  const [fetched, setFetched] = useState(true);
  const [mode, setMode] = useState('C');
  const [user, setUser] = useState(JSON.parse(localStorage.getItem('user')) || {});
  const { refreshToken } = useContext(AuthContext);

  useEffect(() => {
    if(fetched) {
      refreshToken();
      fetch('https://localhost:7125/api/tasks', {
        headers: {
          'Authorization' : `Bearer ${user.token}`,
          'Content-Type': 'application/json'
        }
      })
      .then((response) => response.json())
      .then((dataResponse) => {
        setTodoListItems(dataResponse.data);
      });
      setFetched(false);
    }
    
  }, [fetched]);

  const setStatus = async (item) => {
    item.done = !item.done;
    try {
      const response = await fetch(`https://localhost:7125/api/tasks/${item.id}`, {
        method: 'PUT',
        headers: {
          'Authorization' : `Bearer ${user.token}`,
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
      });

      if(!response.ok) {
        throw new Error('Error al cambiar el estado de la tarea');
      }
      const result = await response.json();
      
    } catch (error) {
      console.log(error);
    }
  } 

  const addTask = async () => {
    try {
      const response = await fetch('https://localhost:7125/api/tasks',
      {
        method: 'POST',
        headers: {
          'Authorization' : `Bearer ${user.token}`,
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(task)
      });

      if(!response.ok) {
        throw new Error('Error al crear la tarea.');
      }

      setFetched(true);
      setTask({...task, description: ''});
    } catch (error) {
      console.error(error);
    }
  } 

  const editTask = async () => {
    try {
      const response = await fetch(`https://localhost:7125/api/tasks/${task.id}`,
      {
        method: 'PUT',
        headers: {
          'Authorization' : `Bearer ${user.token}`,
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(task)
      });

      if(!response.ok) {
        throw new Error('Error al editar la tarea.');
      }

      setFetched(true);
      setTask({ description: '', done: false });
      setMode('C');
    } catch (error) {
      console.log(error);
    }
  }

  const deleteTask = async (task) => {
    try {
      const response = await fetch(`https://localhost:7125/api/tasks/${task.id}`, 
      {
        method: 'DELETE',
        headers: {
          'Authorization' : `Bearer ${user.token}`,
          'Content-Type' : 'application/json'
        }
      });

      if(!response.ok) {
        throw new Error('Error al borra la tarea.');
      }

      setFetched(true);

    } catch (error) {
      console.log(error);
    }
  } 

  return (
    <>
      <LogoutButton />
      <div className="mx-10 bg-white shadow-lg overflow-hidden">
        <div className="px-4 py-2">
          <h1 className="text-gray-800 font-bold text-2xl uppercase">
            Lista de Tareas
          </h1>
          <Errors errorList={[]}/>
          <TodoForm 
            addTask={addTask} 
            task={task} 
            setTask={setTask} 
            mode={mode}
            editTask={editTask}
          />

          <TodoList 
            todoListItems={todoListItems}
            setStatus={setStatus}
            setTask={setTask}
            setMode={setMode}
            deleteTask={deleteTask}
          />
          
        </div>
      </div>
    </>
    
  )
}
