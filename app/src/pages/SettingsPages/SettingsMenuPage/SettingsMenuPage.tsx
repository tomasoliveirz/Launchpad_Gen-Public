import { LauchpadHomeCard } from "@/components/launchpad/cards/home-card";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { pages } from "@/constants/pages";
import { HStack, VStack} from "@chakra-ui/react";
import { FaNetworkWired, FaPalette, FaQuestion } from "react-icons/fa";
import { IoIosSettings } from "react-icons/io";
import { IoGitBranchOutline } from "react-icons/io5";
import { RiFilePaper2Fill } from "react-icons/ri";

export default function(){

const breadcrumbs = getBreadcrumbs(pages, location.pathname);

    return <PageWrapper title="Settings" icon={IoIosSettings} breadcrumbsProps={{items:breadcrumbs}}>
        <VStack mt="5em" mx="auto">
            <HStack mx="auto" gap="4em" flexWrap="wrap">
                <LauchpadHomeCard to="/settings/contract/types" icon={RiFilePaper2Fill} title="Contract Type" description="Manage your contract types"/>
                <LauchpadHomeCard to="/settings/contract/characteristics" icon={FaPalette} title="Contract Characteristics" description="Manage your contract characteristics"/>
                <LauchpadHomeCard to="/settings/contract/variants" icon={IoGitBranchOutline} title="Contract Variants" description="Manage your contract variants"/>
                <LauchpadHomeCard to="/settings/contract/features" icon={FaQuestion} title="Contract Features" description="Manage your contract features"/>
                <LauchpadHomeCard to="/settings/blockchain/networks" icon={FaNetworkWired} title="Blockchain Networks" description="Manage your blockchain networks"/>
            </HStack>
        </VStack>
        </PageWrapper>
}

