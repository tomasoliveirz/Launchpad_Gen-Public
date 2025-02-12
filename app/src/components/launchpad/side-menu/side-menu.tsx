import { Box, Spacer, Text, VStack } from "@chakra-ui/react";
import { FaCode, FaFileCode, FaHammer } from "react-icons/fa";
import { FaCloudArrowUp, FaDroplet } from "react-icons/fa6";
import { Link } from "react-router-dom";
import SideMenuItem from "./side-menu-item";
import { IoIosSettings } from "react-icons/io";

export default function()
{
    return <VStack h="100%" gap="3em" w="50px" bg="#ffffff11">
            <Spacer maxH="10%"/>
            <SideMenuItem to="/" isMain icon={FaHammer}/>
            <SideMenuItem title="Generate Contract" to="/contract/generate" icon={FaCode}/>
            <SideMenuItem title="Publish Contract" to="/contract/publish" icon={FaCloudArrowUp}/>
            <SideMenuItem title="Generate App" to="/app/generate" icon={FaFileCode}/>
            <SideMenuItem title="Manage Liquidity" to="/contract/manage" icon={FaDroplet}/>
            <Spacer/>
            <SideMenuItem mb="1em" title="Settings" to="/settings" icon={IoIosSettings}/>
        </VStack>
}