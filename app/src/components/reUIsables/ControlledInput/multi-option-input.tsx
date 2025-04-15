import { useState, useEffect } from "react";
import CheckBox from "./checkbox";
import { HStack, Stack } from "@chakra-ui/react";
import { CiCircleQuestion } from "react-icons/ci";

export interface MultiSelectionOptionProps {
  options: { value: string; label: string, description?: string }[];
  setValue: (value: string) => void;
  value: string;
  direction?: "row" | "column";
  customKey?: string;
  [x: string]: any;
}

export function MultiSelectOption({
  options,
  setValue,
  value,
  direction = "column",
  customKey,
  ...props
}: MultiSelectionOptionProps) {
  const [checks, setChecks] = useState<boolean[]>([]);

  useEffect(() => {
    const selectedValues = value ? value.split(",") : [];
    const newChecks = options.map((option) => selectedValues.includes(option.value));
    setChecks(newChecks);
  }, [value, options]);

  const setCheck = (pos: number, isChecked: boolean) => {
    const updatedChecks = [...checks];
    updatedChecks[pos] = isChecked;
    setChecks(updatedChecks);

    const selected = options
      .filter((_, idx) => updatedChecks[idx])
      .map((option) => option.value)
      .join(",");
    setValue(selected);
  };

  return (
    <Stack direction={direction} {...props}>
      {options.map((option, idx) => (
        <CheckBox
          key={`${customKey || "multiselect"}-select-${idx}`}
          checked={checks[idx] || false}
          setCheck={(checked: boolean) => setCheck(idx, checked)}
        >
          <HStack>
            {option.label}
            {option.description && (
              <CiCircleQuestion title={option.description} />
            )}
          </HStack>
        </CheckBox>
      ))}
    </Stack>
  );
}