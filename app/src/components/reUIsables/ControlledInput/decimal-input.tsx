import { GenericInput, GenericInputProps } from "./generic-input";

export function DecimalInput({ setError, onChange, value, ...props }: GenericInputProps<number|undefined>) {
  return (
    <GenericInput
      value={value}
      type="number"
      step="any"
      {...props}
      onChange={(s) => updateValue(s, onChange, setError)}
    />
  );
}

function updateValue(
  s: any,
  onChange: (n: number|undefined) => void,
  setError: ((v: boolean | string) => void) | undefined
) {
  const strValue = String(s).trim();

  if (strValue === "") 
    {
      onChange(undefined);
      return;
    }

  // Validate the string to see if it represents a valid decimal number.
  if (!isValidDecimal(strValue)) {
    if (setError) setError("Invalid decimal number");
    return;
  }

  const num = parseFloat(strValue);
  if (isNaN(num)) {
    if (setError) setError("Invalid number");
    return;
  }

  if (setError) setError(false);
  onChange(num);
}

export function isValidDecimal(value: string): boolean {
  // This regex matches decimal numbers with an optional negative sign,
  // allowing for an optional decimal point. Examples: 123, -123, 123.456, -123.456, .456, -.456
  return /^-?(?:\d+\.?\d*|\.\d+)$/.test(value);
}