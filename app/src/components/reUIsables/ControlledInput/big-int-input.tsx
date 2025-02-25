import { GenericInput, GenericInputProps } from "./generic-input";

export function BigIntegerInput({ setError, onChange, value, ...props }: GenericInputProps<bigint|undefined>) {
  return (
    <GenericInput
      value={value}
      type="text"
      {...props}
      onChange={(s) => updateValue(s, onChange, setError)}
    />
  );
}


function updateValue(
    s: any,
    onChange: (n: bigint|undefined) => void,
    setError: ((v: boolean | string) => void) | undefined
  ) {
    const strValue = String(s).trim();
  
    if (strValue === "") 
    {
      onChange(undefined);
      return;
    }
  
    if (!isValidBigInt(strValue)) {
      if (setError) setError("Invalid integer");
      return;
    }
  
    try {
      const num = BigInt(strValue);
      if (setError) setError(false);
      onChange(num);
    } catch (error) {
      if (setError) setError("Number is too large or invalid");
    }
  }
  
  export function isValidBigInt(value: string): boolean {
    return /^-?\d+$/.test(value);
  }