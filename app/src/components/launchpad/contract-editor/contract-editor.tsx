import { Editor } from "@monaco-editor/react";

interface CodeEditorProps {
    code: string;
    onCodeChange?: (newCode: string | undefined) => void;
    readOnly?: boolean;
}

export function CodeEditor({ code, onCodeChange, readOnly = false }: CodeEditorProps) {
    return (
        <Editor
            height="110vh"
            width="60vw"
            defaultLanguage="solidity"
            theme="vs-dark"
            value={code}
            onChange={onCodeChange}
            options={{
                readOnly,
                minimap: { enabled: false },
                scrollBeyondLastLine: false,
            }}
        />
    );
}