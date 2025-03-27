import { Box } from "@chakra-ui/react"
import { Editor } from "@monaco-editor/react"
export function CodePreview({...props}) {
    return (
        <Box {...props}>
            <Editor
                width="80vh"
                height="60vh"
                defaultLanguage="solidity"
                theme="vs-dark"
                options={{
                    readOnly: true
                }}
            />
        </Box>
    );
}