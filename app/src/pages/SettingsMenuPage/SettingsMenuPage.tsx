import { PageWrapper } from "@/components/launchpad/wrappers/page-wrapper";
import { Box, BoxProps, HStack, Text, VStack, Wrap } from "@chakra-ui/react";
import { IconType } from "react-icons";
import { FaNetworkWired, FaPalette } from "react-icons/fa";
import { IoIosSettings } from "react-icons/io";
import { IoGitBranchOutline } from "react-icons/io5";
import { RiFilePaper2Fill } from "react-icons/ri";
import { Link } from "react-router-dom";


export default function(){
    return <PageWrapper title="Settings" icon={IoIosSettings} description="Set up data for the generation service">
        <VStack mt="5em">
            <HStack gap="4em" flexWrap="wrap">
                <SquareMenuItem to="/settings/contracts/types" icon={RiFilePaper2Fill} title="Contract Type" description="Manage your contract types"/>
                <SquareMenuItem to="/settings/contracts/characteristics" icon={FaPalette} title="Contract Characteristics" description="Manage your contract characteristics"/>
                <SquareMenuItem to="/settings" icon={IoGitBranchOutline} title="Contract Variants" description="Manage your contract variants"/>
                <SquareMenuItem to="/settings" icon={FaNetworkWired} title="Blockchain Networks" description="Manage your contract variants"/>
            </HStack>
        </VStack>
        </PageWrapper>
}


export interface SquareMenuItemProps extends BoxProps
{
    to:string
    icon: IconType
    title?:string
    description: string
}

export function SquareMenuItem({icon, to, title, description, ...props}:SquareMenuItemProps){
    const Icon = icon
    return <Box w="13em" h="13em" bgColor="gray.900" shadow="sm" {...props}>
        <Link to={to} style={{ width: "100%", height: "100%", display: "flex" }}>
        <Wrap 
            textAlign="center" 
            display="flex"
            flexDirection="column"
            alignItems="center"
            justifyContent="space-evenly"
            p="2em"
        >
            <Box fontSize="3em">
                <Icon color="white"/>
            </Box>
            <Box>
                <Text textStyle="l" color="white">{title}</Text>
                <Text textStyle="xs" color="gray.400">{description}</Text>
            </Box>
        </Wrap>
        </Link>
    </Box>
}