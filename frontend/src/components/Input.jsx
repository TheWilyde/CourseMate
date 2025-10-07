const Input = ({
  label,
  type = 'text',
  placeholder,
  onChange,
  required = false,
}) => {
  return (
    <div className="flex flex-col gap-2">
      <label
        htmlFor={label}
        className="appearance-none text-[0.85rem] pl-4 text-gray-700">
        {label}
      </label>
      <input
        type={type}
        placeholder={placeholder}
        required={required}
        id={label}
        name={label}
        onChange={onChange}
        className="appearance-none h-12 px-4 bg-[#f2f2f2f2] rounded focus:outline-none ring-1 ring-inset ring-[#333333]/40"
      />
    </div>
  );
};

export default Input;
