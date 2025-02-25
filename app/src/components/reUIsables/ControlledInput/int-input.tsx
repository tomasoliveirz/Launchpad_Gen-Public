import { GenericInput, GenericInputProps } from "./generic-input";

export function IntegerInput({ setError, onChange, value, ...props }: GenericInputProps<number|undefined>) {
  return (
    <GenericInput
      value={value}
      type="number"
      step="1"
      {...props}
      onChange={(s) => updateValue(s, onChange, setError)}
    />
  );
}
function updateValue(
    s: any,
    onChange: (n: number) => void,
    setError: ((v: boolean | string) => void) | undefined
  ) {
    const strValue = String(s);
  
    if (strValue === "") {
      if (setError) setError("Input cannot be empty");
      return;
    }
  
    if (!isInteger(strValue)) {
      if (setError) setError("Invalid integer");
      return;
    }
  
    if (setError) setError(false);
    const num = parseInt(strValue, 10);
    onChange(num);
  }
  
  export function isInteger(value: string): boolean {
    return /^-?\d+$/.test(value);
  }