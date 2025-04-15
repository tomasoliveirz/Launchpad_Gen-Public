import { Accordion, AccordionRootProps, HStack, Span } from "@chakra-ui/react";
import CheckBox from "../ControlledInput/checkbox";

export interface AccordionItemWrapperProps extends AccordionRootProps
{
  label:string,
  onChecked?:()=>void
  checked?:boolean
}


export function AccordionItemWrapper({children,label,onChecked,checked, ...props}:AccordionItemWrapperProps)
{
    return <Accordion.Root collapsible {...props}>
      <Accordion.Item border="0" value={(props.value? props.value[0] :"")} key={props.key}>
        <Accordion.ItemTrigger border="0" _focus={{outline:"0"}} bg="transparent" m={0} p={0} textAlign="left">
          <HStack>
            {onChecked && <CheckBox checked={checked??false} setCheck={onChecked}/>}          
            <Span flex="1">{label}</Span>
          </HStack>
          <Accordion.ItemIndicator />
        </Accordion.ItemTrigger>
        <Accordion.ItemContent pl="2em">
          <Accordion.ItemBody>{children}</Accordion.ItemBody>
        </Accordion.ItemContent>
      </Accordion.Item>
  </Accordion.Root>
}