// CheckBoxWithDescription.tsx
import CheckBox from "@/components/reUIsables/ControlledInput/checkbox";
import { HStack, Tooltip } from "@chakra-ui/react";
import { CiCircleQuestion } from "react-icons/ci";

interface CheckboxWithDescriptionProps {
  checked: boolean;
  setCheck: (x: boolean) => void;
  label: React.ReactNode;
  description?: string;
}

export default function CheckboxWithDescription({
  checked,
  setCheck,
  label,
  description,
}: CheckboxWithDescriptionProps) {
  return (
    <CheckBox checked={checked} setCheck={setCheck}>
      <HStack>
        {label}
        {description && (
            <CiCircleQuestion title={description}/>
        )}
      </HStack>
    </CheckBox>
  );
}
