import { GenericInput, GenericInputProps } from "./generic-input";

export function PrivateKeyInput({
    setError,
    onChange,
    value,
    ...props
  }: GenericInputProps<string>) {
    return (
      <GenericInput
        value={value}
        type="text"
        {...props}
        onChange={(s) => updatePrivateKeyValue(s, onChange, setError)}
      />
    );
  }
  
  function updatePrivateKeyValue(
    s: any,
    onChange: (s: string) => void,
    setError: ((v: boolean | string) => void) | undefined
  ) {
    const strValue = String(s).trim();
    onChange(strValue);
    if (setError) {
      setError(strValue !== "" && !isValidPrivateKey(strValue));
    }
  }
  
  export function isValidPrivateKey(key: string): boolean {
    return /^(0x)?[a-fA-F0-9]{64}$/.test(key);
  }