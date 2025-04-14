import { Flex, VStack } from "@chakra-ui/react";
import { Option } from "../../reUIsables/ControlledInput/radio-input";
import { Field } from "@/components/ui/field";
import { MultitypeInput } from "@/components/reUIsables/ControlledInput/multi-type-input";
import { AccordionItemWrapper } from "@/components/reUIsables/Accordion/accordion-item-wrapper";
import { GroupInputWrapper } from "../wrappers/group-input-wrapper";


export interface FungibleTokenContractProps {
    name: string;
    symbol: string;
    premint: string;
    features: string;
    vote: string;
    access: string;
    upgradeability: string;
    securityContact: string;
    license: string;
    featuresList: Option[];
    voteList: Option[];
    accessList: Option[];
    upgradeabilityList: Option[];

    voteChecked: boolean;
    accessChecked: boolean;
    upgradeabilityChecked: boolean;

    onNameChange: (value: string) => void;
    onSymbolChange: (value: string) => void;
    onPremintChange: (value: string) => void;
    onFeaturesChange: (value: string) => void;
    onVoteChange: (value: string) => void;
    onAccessChange: (value: string) => void;
    onUpgradeabilityChange: (value: string) => void;
    onSecurityContactChange: (value: string) => void;
    onLicenseChange: (value: string) => void;

    onVoteCheck: (checked: boolean) => void;
    onAccessCheck: (checked: boolean) => void;
    onUpgradeabilityCheck: (checked: boolean) => void;
}
export function FungibleTokenContract({
    name,
    symbol,
    premint,
    features,
    vote,
    access,
    upgradeability,
    securityContact,
    license,
    voteChecked,
    accessChecked,
    upgradeabilityChecked,
    onNameChange,
    onSymbolChange,
    onPremintChange,
    onFeaturesChange,
    onVoteChange,
    onAccessChange,
    onUpgradeabilityChange,
    onSecurityContactChange,
    onLicenseChange,
    onVoteCheck,
    onAccessCheck,
    onUpgradeabilityCheck,
    accessList,
    upgradeabilityList,
    voteList,
    featuresList
}: FungibleTokenContractProps) {

    return <>
        <VStack alignItems="start">
            <GroupInputWrapper label="Settings" w="12rem">
                <Flex justifyContent="space-between">
                    <Field label="Name" w="8rem">
                        <MultitypeInput
                            type="string"
                            value={name}
                            setValue={onNameChange}
                        />
                    </Field>
                    <Field label="Symbol" w="3rem">
                        <MultitypeInput
                            type="string"
                            value={symbol}
                            setValue={onSymbolChange}
                        />
                    </Field>
                </Flex>
                <Field label="Premint">
                    <MultitypeInput
                        type="integer"
                        value={premint}
                        setValue={onPremintChange}
                    />
                </Field>
            </GroupInputWrapper>
            <GroupInputWrapper label="Features" w="12rem">
                <MultitypeInput
                    multiSelect
                    type="option"
                    options={featuresList}
                    value={features}
                    setValue={onFeaturesChange}
                />
            </GroupInputWrapper>
            <AccordionItemWrapper
                onChecked={() => onVoteCheck(!voteChecked)}
                checked={voteChecked}
                label="Votes"
                w="12rem"
            >
                <MultitypeInput
                    type="option"
                    options={voteList}
                    value={vote}
                    setValue={onVoteChange}
                />
            </AccordionItemWrapper>
            <AccordionItemWrapper
                onChecked={() => onAccessCheck(!accessChecked)}
                checked={accessChecked}
                label="Access Control"
            >
                <MultitypeInput
                    type="option"
                    options={accessList}
                    value={access}
                    setValue={onAccessChange}
                />
            </AccordionItemWrapper>
            <AccordionItemWrapper
                onChecked={() => onUpgradeabilityCheck(!upgradeabilityChecked)}
                checked={upgradeabilityChecked}
                label="Upgradeability"
            >
                <MultitypeInput
                    type="option"
                    options={upgradeabilityList}
                    value={upgradeability}
                    setValue={onUpgradeabilityChange}
                />
            </AccordionItemWrapper>
            <GroupInputWrapper label="Info" w="12rem">
                <Field label="Security Contact">
                    <MultitypeInput
                        type="email"
                        value={securityContact}
                        setValue={onSecurityContactChange}
                    />
                </Field>
                <Field label="License">
                    <MultitypeInput
                        type="string"
                        value={license}
                        setValue={onLicenseChange}
                    />
                </Field>
            </GroupInputWrapper>
        </VStack>
    </>
}