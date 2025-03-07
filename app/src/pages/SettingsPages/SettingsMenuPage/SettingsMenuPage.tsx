import { LauchpadHomeCard } from "@/components/launchpad/cards/home-card";
import { getBreadcrumbs } from "@/components/reUIsables/Breadcrumbs/breadcrumbs";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { pages } from "@/constants/pages";
import { SimpleGrid, VStack} from "@chakra-ui/react";
import { BsStars } from "react-icons/bs";
import { FaNetworkWired, FaPalette, FaScroll } from "react-icons/fa";
import { IoIosSettings } from "react-icons/io";
import { IoGitBranchOutline } from "react-icons/io5";

export default function(){

const breadcrumbs = getBreadcrumbs(pages, location.pathname);

    return <PageWrapper title="Settings" icon={IoIosSettings} breadcrumbsProps={{items:breadcrumbs}}>
                <VStack h="100%" pt="4em">
                    <SimpleGrid columns={{base:1, md:2, lg:3, xl:4}} gap="4em">
                            <LauchpadHomeCard to="/settings/contract/types" icon={FaScroll} title="Contract Type" description="Manage your contract types"/>
                            <LauchpadHomeCard to="/settings/contract/characteristics" icon={FaPalette} title="Contract Characteristics" description="Manage your contract characteristics"/>
                            <LauchpadHomeCard to="/settings/contract/variants" icon={IoGitBranchOutline} title="Contract Variants" description="Manage your contract variants"/>
                            <LauchpadHomeCard to="/settings/contract/features" icon={BsStars} title="Contract Features" description="Manage your contract features"/>
                            <LauchpadHomeCard to="/settings/blockchain/networks" icon={FaNetworkWired} title="Blockchain Networks" description="Manage your blockchain networks"/>
                    </SimpleGrid>
                </VStack>
            </PageWrapper>
}

