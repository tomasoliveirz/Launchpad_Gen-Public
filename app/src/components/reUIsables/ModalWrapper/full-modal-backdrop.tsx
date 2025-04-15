import { DialogBackdrop, DialogBackdropProps, Portal } from "@chakra-ui/react";

export function FullModalBackdrop(props:DialogBackdropProps)
{
    return <Portal>
            <DialogBackdrop {...props} />
            </Portal>
}