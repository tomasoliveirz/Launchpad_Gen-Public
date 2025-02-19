import { LauchpadHomeCard } from "@/components/launchpad/cards/home-card";
import { Center, Heading, HStack, VStack} from "@chakra-ui/react";
import { FaCode, FaFileCode } from "react-icons/fa";
import { FaCloudArrowUp, FaDroplet } from "react-icons/fa6";

export default function(){
    return <>
    <Center height="100%">
        <VStack gap="4em">
            <Heading as="h1" textStyle="7xl" alignSelf="start">Launchpad</Heading>
            <HStack gap="6em" flexWrap="wrap">
                <LauchpadHomeCard to="/contract/generate" icon={FaCode} title="Generate" description="Generate a smart contract through a simple interface"/>
                <LauchpadHomeCard to="/contract/publish" icon={FaCloudArrowUp} title="Publish" description="Publish a smart contract on your favorite network"/>
                <LauchpadHomeCard to="/app/generate" icon={FaFileCode} title="Create an app" description="Take you contract to the next level by generating a web app"/>
                <LauchpadHomeCard to="/contract/manage" icon={FaDroplet} title="Manage Liquidity" description="Make sure your token has enough liquidity"/>
            </HStack>
        </VStack>
    </Center>
    </>
}