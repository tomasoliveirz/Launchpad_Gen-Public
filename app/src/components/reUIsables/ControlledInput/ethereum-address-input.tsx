import { GenericInputProps, GenericInput } from "./generic-input";

export function EthereumAddressInput({
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
        onChange={(s) => updateEthereumAddressValue(s, onChange, setError)}
      />
    );
  }

  function updateEthereumAddressValue(
    s: any,
    onChange: (s: string) => void,
    setError: ((v: boolean | string) => void) | undefined
  ) {
    const strValue = String(s).trim();
    onChange(strValue);
    if (setError) {
      setError(strValue !== "" && !isValidEthereumAddress(strValue));
    }
  }

  export function isValidEthereumAddress(address: string): boolean {
    return /^0x[a-fA-F0-9]{40}$/.test(address);
  }