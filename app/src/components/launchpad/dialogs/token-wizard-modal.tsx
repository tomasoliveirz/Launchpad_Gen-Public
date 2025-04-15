import { Card, HStack, Spacer, VStack, Text } from "@chakra-ui/react";
import { CancelButton, LaunchpadButton } from "../buttons/button";
import { ModalWrapper } from "@/components/reUIsables/ModalWrapper/modal-wrapper";
import { FullModalBackdrop } from "@/components/reUIsables/ModalWrapper/full-modal-backdrop";
import { useState } from "react";
import { TokenWizardResponse } from "@/models/TokenWizardResponse";

export interface TokenWizardProps {
    isOpen?: boolean;
    onOpenChange?: (open: boolean) => void;
    tokenWizardResponse?: TokenWizardResponse
}

export function TokenWizardModal({ isOpen, onOpenChange, tokenWizardResponse }: TokenWizardProps) {
    const [isStarted, setIsStarted] = useState(false);
    const handleClose = () => {
        onOpenChange?.(false);
        setIsStarted(false);
    }

    const trigger = <LaunchpadButton text="Token Wizard" color="text.primary" />
    const body = <Card.Root bg="bg.primary">
        <Card.Header m="0" fontSize="xl" p="1em" fontWeight="bold" color="text.primary">
            <HStack>
                Token Wizard
                <Spacer />
                <CancelButton onClick={() => handleClose()} />
            </HStack>
        </Card.Header>
        <Card.Body pt="0">
            {!isStarted ? (
                <LaunchpadButton
                    text="Start"
                    fontSize="2xl"
                    color="text.primary"
                    w="fit-content"
                    alignSelf="center"
                    onClick={() => setIsStarted(true)}
                />
            ) : (
                <>
                    <VStack gap="2em">
                        <Text color="text.primary" fontSize="xl">{tokenWizardResponse?.question}</Text>
                        <VStack>
                            {tokenWizardResponse?.possibleAnswers.map((answer) => (
                                <LaunchpadButton color="text.primary" key={answer} text={answer} w="fit-content" />
                            ))}
                        </VStack>
                    </VStack>
                </>
            )}
        </Card.Body>
    </Card.Root>

    return <ModalWrapper body={body}
        trigger={trigger}
        backdrop={<FullModalBackdrop bg="#000000AA" />}
        open={isOpen}
        onOpenChange={({ open }) => onOpenChange?.(open)}
        placement="center" />
}