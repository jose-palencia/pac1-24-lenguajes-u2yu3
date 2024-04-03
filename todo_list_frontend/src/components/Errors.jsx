import { IoAlert } from "react-icons/io5";

export const Errors = ({ errorList }) => {
    return (
        <div className="text-white">
            <ul>
                <li className="bg-red-500 p-3 rounded-md my-2 font-semibold flex flex-row gap-2 items-center">
                    <IoAlert /> La Descripción es requerida.
                </li>
                <li className="bg-red-500 p-3 rounded-md my-2 font-semibold flex flex-row gap-2 items-center">
                    <IoAlert />La Descripción debe tener entre 10 y 250 letras
                </li>
            </ul>
        </div>
    )
}
