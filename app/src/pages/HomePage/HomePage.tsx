import { LauchpadHomeCard } from "@/components/launchpad/cards/home-card";
import { PageWrapper } from "@/components/reUIsables/PageWrapper/page-wrapper";
import { Heading, HStack, SimpleGrid, Spacer, VStack } from "@chakra-ui/react";
import { FaCode, FaFileCode } from "react-icons/fa";
import { FaCloudArrowUp, FaDroplet } from "react-icons/fa6";

export default function () {
    return <PageWrapper title={""}>
        <VStack h="100%">
            <Spacer />
            <HStack w="65%" mb="4em">
                <Heading as="h1">Launchpad</Heading>
            </HStack>
            <SimpleGrid columns={{ base: 1, md: 2, lg: 3, xl: 4 }} gap="4em">
                <LauchpadHomeCard to="/contract/generate" icon={FaCode} title="Generate" description="Generate a smart contract through a simple interface" />
                <LauchpadHomeCard to="/contract/publish" icon={FaCloudArrowUp} title="Publish" description="Publish a smart contract on your favorite network" />
                <LauchpadHomeCard to="/app/generate" icon={FaFileCode} title="Create an app" description="Take you contract to the next level by generating a web app" />
                <LauchpadHomeCard to="/contract/manage" icon={FaDroplet} title="Manage Liquidity" description="Make sure your token has enough liquidity" />
            </SimpleGrid>
            <Spacer />
        </VStack>
    </PageWrapper>

}