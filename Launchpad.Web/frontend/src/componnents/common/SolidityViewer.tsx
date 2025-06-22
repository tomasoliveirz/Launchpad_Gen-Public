import React from "react";

function highlightSolidity(code) {
    if (!code) return "";

    let escaped = code
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;");

    const createSafePattern = (pattern) => {
        return `(<span[^>]*>.*?<\\/span>)|(${pattern})`;
    };

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
        const re = new RegExp(createSafePattern(`\\b${kw}\\b`), "g");
        escaped = escaped.replace(re, (match, spanGroup, keywordGroup) => {
            if (spanGroup) {
                return spanGroup; 
            }
            if (keywordGroup) {
                return `<span class="keyword">${keywordGroup}</span>`; 
            }
            return match;
        });
    });

    const types = [
        "uint", "uint8", "uint16", "uint32", "uint64", "uint128", "uint256",
        "int", "int8", "int16", "int32", "int64", "int128", "int256",
        "bool", "address", "string", "byte", "bytes", "bytes1", "bytes32",
    ];

    types.forEach(type => {
        const re = new RegExp(createSafePattern(`\\b${type}\\b`), "g");
        escaped = escaped.replace(re, (match, spanGroup, typeGroup) => {
            if (spanGroup) {
                return spanGroup;
            }
            if (typeGroup) {
                return `<span class="type">${typeGroup}</span>`;
            }
            return match;
        });
    });

    escaped = escaped.replace(
        /(<span class="keyword">contract<\/span>\s+)([a-zA-Z_$][a-zA-Z0-9_$]*)/g,
        (match, keywordSpan, contractName) =>
            `${keywordSpan}<span class="contract-name">${contractName}</span>`
    );

    const funcCallPattern = `([a-zA-Z_$][a-zA-Z0-9_$]*)\\s*\\(`; 
    const funcCallRegex = new RegExp(createSafePattern(funcCallPattern), "g");

    escaped = escaped.replace(funcCallRegex, (match, spanGroup, funcCallText) => {
        if (spanGroup) {
            return spanGroup; 
        }
        if (funcCallText) {
            const funcNameMatch = funcCallText.match(/\b([a-zA-Z_$][a-zA-Z0-9_$]*)\b/);
            if (funcNameMatch && funcNameMatch[1]) {
                const funcName = funcNameMatch[1];
                const restOfMatch = funcCallText.substring(funcName.length); 
                return `<span class="function-call">${funcName}</span>${restOfMatch}`;
            }
        }
        return match;
    });

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
