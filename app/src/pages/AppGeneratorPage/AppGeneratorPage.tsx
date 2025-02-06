import PreviewContractsInput from "@/components/launchpad/preview-contracts-input";
import { Heading } from "@chakra-ui/react";
import axios from "axios";
import { useEffect, useState } from "react";

export default function(){
    const apiUrl = process.env.REACT_APP_API_URL;
    const [contractsGenerated, setContractsGenerated] = useState([]);
    useEffect(() => {
        axios.get(`${apiUrl}/`)
        .then( (response) => {
            console.log(response.data)
            setContractsGenerated(response.data)})
        .catch((error) => console.error(error));
    }, []);

    return <>
    <Heading as="h1">Create an App</Heading>
    <PreviewContractsInput/>
    </>
}