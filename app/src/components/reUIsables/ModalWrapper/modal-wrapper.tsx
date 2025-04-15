import {
  DialogBody,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogRoot,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"
import { DialogContentProps, DialogHeaderProps, DialogRootProps } from "@chakra-ui/react"
import { JSX } from "react";
import { FullModalBackdrop } from "./full-modal-backdrop";

export interface ModalWrapperProps extends Omit<DialogRootProps, "children"> {
  trigger: JSX.Element
  backdrop?: JSX.Element
  title?: string,
  header?: JSX.Element,
  body: JSX.Element,
  footer?: JSX.Element
  contentProps?: Omit<DialogContentProps, "ref">
  contentRef?: React.Ref<HTMLDivElement>
  headerProps?: DialogHeaderProps
}
export function ModalWrapper({ trigger, contentProps, headerProps, footer, backdrop, body, header, title, contentRef, ...props }: ModalWrapperProps) {
  return <DialogRoot {...props}>
    {backdrop ?? <FullModalBackdrop />}
    <DialogTrigger m="0" p={0} bg="transparent" fontWeight="normal" focusRing="none" _focus={{ border: "none" }}>
      {trigger}
    </DialogTrigger>
    <DialogContent ref={contentRef} bg="transparent" {...contentProps}>
      {header ? header : title ?
        (<DialogHeader {...headerProps}>
          <DialogTitle>
            {title}
          </DialogTitle>
        </DialogHeader>) : <></>
      }
      <DialogBody>
        {body}
      </DialogBody>
      {footer && <DialogFooter>
        {footer}
      </DialogFooter>}
    </DialogContent>
  </DialogRoot>
}