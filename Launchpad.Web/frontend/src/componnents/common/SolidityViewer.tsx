import React from "react";

function highlightSolidity(code) {
    if (!code) return "";

    let escaped = code
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;");


    escaped = escaped.replace(
        /(".*?"|'.*?')/g,
        (match) => `<span class="string">${match}</span>`
    );

    escaped = escaped.replace(
        /\/\*[\s\S]*?\*\//g,
        (match) => `<span class="comment">${match}</span>`
    );

    escaped = escaped.replace(
        /(\/\/.*$)/gm,
        (match) => `<span class="comment">${match}</span>`
    );

    escaped = escaped.replace(
        /\b(\d+(\.\d+)?|\.\d+)\b/g,
        (match) => `<span class="number">${match}</span>`
    );

    const keywords = [
        "pragma", "solidity", "contract", "function", "return", "require",
        "if", "else", "for", "while", "do", "break", "continue", "emit",
        "event", "mapping", "public", "private", "internal", "external",
        "view", "pure", "payable", "memory", "storage", "constructor",
        "modifier", "assembly", "try", "catch", "revert", "throw", "interface",
        "error",
    ];

    keywords.forEach(kw => {
        const re = new RegExp(`\\b${kw}\\b`, "g");
        escaped = escaped.replace(re, `<span class="keyword">${kw}</span>`);
    });

    const types = [
        "uint", "uint8", "uint16", "uint32", "uint64", "uint128", "uint256",
        "int", "int8", "int16", "int32", "int64", "int128", "int256",
        "bool", "address", "string", "byte", "bytes", "bytes1", "bytes32",
    ];

    types.forEach(type => {
        const re = new RegExp(`\\b${type}\\b`, "g");
        escaped = escaped.replace(re, `<span class="type">${type}</span>`);
    });

    escaped = escaped.replace(
        /(<span class="keyword">contract<\/span>\s+)([a-zA-Z_$][a-zA-Z0-9_$]*)/g,
        (match, keywordSpan, contractName) =>
            `${keywordSpan}<span class="contract-name">${contractName}</span>`
    );

    escaped = escaped.replace(
        /\b([a-zA-Z_$][a-zA-Z0-9_$]*)\s*\(/g,
        (match, funcName) => `<span class="function-call">${funcName}</span>(`
    );

    return escaped;
}

export default function SolidityViewer({ generatedCode }) {
    const lines = generatedCode ? generatedCode.split("\n") : [];

    return (
        <div className="code-container">
            <div className="line-numbers">
                {lines.map((_, i) => (
                    <div key={i} className="line-number">
                        {i + 1}
                    </div>
                ))}
            </div>
            <pre
                className="code-block"
                dangerouslySetInnerHTML={{ __html: highlightSolidity(generatedCode) }}
            />
        </div>
    );
}
