import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { TypeField } from "@/components/reUIsables/ControlledInput/controlled-input";
import { DecimalInput } from "@/components/reUIsables/ControlledInput/decimal-input";
import { MnemonicInput } from "@/components/reUIsables/ControlledInput/mnemonic-input";
import { FieldErrorText, FieldLabel, FieldRoot } from "@chakra-ui/react";
import { useState } from "react";
import { FaCode } from "react-icons/fa";

export default function()
{
    const [error, setError] = useState<boolean|string>(false);
    const [val, setVal] = useState<string>()
    console.log(error);
    const defaultError = "Email is not valid";
    return <PageWrapper title="Contract Generator" icon={FaCode}>
                <FieldRoot invalid={(error && (typeof(error) === "string" && error !== "") || (typeof(error) === "boolean" && error === true))}>
                    <FieldLabel>
                    </FieldLabel>
                    <MnemonicInput bg="transparent" value="val" setError={setError} onChange={(setVal)} color="white"/>
                    {/* <EmailInput value="val" setError={setError} onChange={(setVal)}/> */}
                    {/* <DecimalInput value={val} setError={setError} onChange={setVal}/> */}
                    {error && <FieldErrorText>{typeof(error)==="string" ? error : defaultError }</FieldErrorText>}
                </FieldRoot>

                <TypeField label="Security Contact" type="email" description="Where people can contact you to report security issues. Will only be visible if contract metadata is verified."/>
            
            </PageWrapper>
}


