import CheckBox from "@/components/reUIsables/ControlledInput/checkbox";
import { Accordion, AccordionRootProps, HStack, Span } from "@chakra-ui/react";
import { CiCircleQuestion } from "react-icons/ci";
import { FaChevronDown } from "react-icons/fa";

export interface AccordionItemWithDescriptionWrapperProps extends AccordionRootProps {
    label: string,
    onChecked?: () => void
    checked?: boolean
    description?: string
}


export function AccordionItemWithDescriptionWrapper({ children, label, onChecked, checked, description, ...props }: AccordionItemWithDescriptionWrapperProps) {
    return <Accordion.Root collapsible py="1em" borderBottom="2px solid white" {...props}>
        <Accordion.Item border="0" value={(props.value ? props.value[0] : "")} key={props.key}>
            <HStack>
                <Accordion.ItemTrigger border="0" _focus={{ outline: "0" }} bg="transparent" m={0} p={0} textAlign="left">
                    {onChecked && <CheckBox checked={checked ?? false} setCheck={onChecked} />}
                    <Span flex="1">{label}</Span>
                    <Accordion.ItemIndicator
                        as={FaChevronDown}
                        transition="transform 0.2s"
                    />
                </Accordion.ItemTrigger>
                {description && <CiCircleQuestion title={description} />}
            </HStack>
            <Accordion.ItemContent pl="2em">
                <Accordion.ItemBody>{children}</Accordion.ItemBody>
            </Accordion.ItemContent>
        </Accordion.Item>
    </Accordion.Root>
}