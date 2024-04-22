import { IoAlert } from "react-icons/io5";

export const Errors = ({ errorList }) => {
    
    return (
        <div className="text-white">
            <ul>
                {errorList.map((error, i) => (
                    <li key={i} className="bg-red-500 p-3 rounded-md my-2 font-semibold flex flex-row gap-2 items-center">
                        <IoAlert /> {error}
                    </li>
                ))}
            </ul>
        </div>
    )
}
