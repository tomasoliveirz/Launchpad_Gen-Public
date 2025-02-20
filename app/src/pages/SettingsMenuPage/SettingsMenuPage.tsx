import { LauchpadHomeCard } from "@/components/launchpad/cards/home-card";
import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { HStack, VStack} from "@chakra-ui/react";
import { FaNetworkWired, FaPalette } from "react-icons/fa";
import { IoIosSettings } from "react-icons/io";
import { IoGitBranchOutline } from "react-icons/io5";
import { RiFilePaper2Fill } from "react-icons/ri";

export default function(){
    return <PageWrapper title="Settings" icon={IoIosSettings} description="Set up data for the generation service">
        <VStack mt="5em">
            <HStack gap="4em" flexWrap="wrap">
                <LauchpadHomeCard to="/settings/contract/types" icon={RiFilePaper2Fill} title="Contract Type" description="Manage your contract types"/>
                <LauchpadHomeCard to="/settings/contract/characteristics" icon={FaPalette} title="Contract Characteristics" description="Manage your contract characteristics"/>
                <LauchpadHomeCard to="/settings/contract/variants" icon={IoGitBranchOutline} title="Contract Variants" description="Manage your contract variants"/>
                <LauchpadHomeCard to="/settings/blockchain/networks" icon={FaNetworkWired} title="Blockchain Networks" description="Manage your blockchain networks"/>
            </HStack>
        </VStack>
        </PageWrapper>
}

