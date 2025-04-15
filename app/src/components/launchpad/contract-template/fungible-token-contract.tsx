import { Flex, VStack } from "@chakra-ui/react";
import { Option } from "../../reUIsables/ControlledInput/radio-input";
import { Field } from "@/components/ui/field";
import { MultitypeInput } from "@/components/reUIsables/ControlledInput/multi-type-input";
import { GroupInputWrapper } from "../wrappers/group-input-wrapper";
import { AccordionItemWithDescriptionWrapper } from "../wrappers/accordion-item-with-description";
import { FieldWithDescription } from "../fields/field-with-description";


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
        <VStack alignItems="start" w="13rem">
            <GroupInputWrapper label="Settings" w="100%">
                <Flex justifyContent="space-between">
                    <FieldWithDescription label="Name" w="8rem">
                        <MultitypeInput
                            type="string"
                            value={name}
                            setValue={onNameChange}
                        />
                    </FieldWithDescription>
                    <FieldWithDescription label="Symbol" w="3rem">
                        <MultitypeInput
                            type="string"
                            value={symbol}
                            setValue={onSymbolChange}
                        />
                    </FieldWithDescription>
                </Flex>
                <FieldWithDescription label="Premint" description="Number of tokens to pre-mint">
                    <MultitypeInput
                        type="integer"
                        value={premint}
                        setValue={onPremintChange}
                        
                    />
                </FieldWithDescription>
            </GroupInputWrapper>
            <GroupInputWrapper label="Features" w="100%">
                <MultitypeInput
                    multiSelect
                    type="option"
                    options={featuresList}
                    value={features}
                    setValue={onFeaturesChange}
                />
            </GroupInputWrapper>
            <AccordionItemWithDescriptionWrapper
                onChecked={() => onVoteCheck(!voteChecked)}
                checked={voteChecked}
                label="Votes"
                description="Keeps track of historical balances for voting in on-chain governance, with a way to delegate one's voting power to a trusted account."
            >
                <MultitypeInput
                    type="option"
                    options={voteList}
                    value={vote}
                    setValue={onVoteChange}
                />
            </AccordionItemWithDescriptionWrapper>
            <AccordionItemWithDescriptionWrapper
                onChecked={() => onAccessCheck(!accessChecked)}
                checked={accessChecked}
                label="Access Control"
                description="Allows the contract to be upgraded"
            >
                <MultitypeInput
                    type="option"
                    options={accessList}
                    value={access}
                    setValue={onAccessChange}
                />
            </AccordionItemWithDescriptionWrapper>
            <AccordionItemWithDescriptionWrapper
                onChecked={() => onUpgradeabilityCheck(!upgradeabilityChecked)}
                checked={upgradeabilityChecked}
                label="Upgradeability"
                description="Allows the contract to be upgraded"
            >
                <MultitypeInput
                    type="option"
                    options={upgradeabilityList}
                    value={upgradeability}
                    setValue={onUpgradeabilityChange}
                />
            </AccordionItemWithDescriptionWrapper>
            <GroupInputWrapper label="Info" w="12rem">
                <FieldWithDescription label="Security Contact" description="Where people can contact you to report security issues. Will only be visible if contract metadata is verified.">
                    <MultitypeInput
                        type="email"
                        value={securityContact}
                        setValue={onSecurityContactChange}
                    />
                </FieldWithDescription>
                <FieldWithDescription label="License">
                    <MultitypeInput
                        type="string"
                        value={license}
                        setValue={onLicenseChange}
                    />
                </FieldWithDescription>
            </GroupInputWrapper>
        </VStack>
    </>
}