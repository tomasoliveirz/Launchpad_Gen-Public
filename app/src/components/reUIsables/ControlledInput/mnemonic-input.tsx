import { Button, HStack,  InputProps,  Spacer, VStack } from "@chakra-ui/react";
import {
    MenuContent,
    MenuItem,
    MenuRoot,
    MenuTrigger,
  } from "@/components/ui/menu"
import { useEffect, useState } from "react";

export interface MnemonicInputProps extends Omit<InputProps, "onChange"|"value">
{
    itemsPerRow:number
    value:string|undefined
    setError?:(v:boolean|string)=>void
    onChange:(s:string|undefined)=>void
}


export function MnemonicInput({itemsPerRow, ...props}:MnemonicInputProps) {
    const [words, setWords] = useState<number>(12);
    const rowCount = (12%itemsPerRow > 0) ? Math.ceil(12/itemsPerRow): 12/itemsPerRow;
    const [rows, setRows] = useState<number>(rowCount);

    useEffect(()=>{
        setRows((12%itemsPerRow > 0) ? 
                 Math.ceil(12/itemsPerRow): 
                 12/itemsPerRow);
    }, [words])

    return (
      <VStack w="100%">
        <HStack w="100%">
            <Spacer/>
            <MenuRoot onSelect={x => setWords(parseInt(x.value))}>
                <MenuTrigger asChild>
                    <Button bg={props.bg} color={props.color}>Words</Button>
                </MenuTrigger>
                <MenuContent>
                    <MenuItem value="12">12</MenuItem>
                    <MenuItem value="15">15</MenuItem>
                    <MenuItem value="18">18</MenuItem>
                    <MenuItem value="21">21</MenuItem>
                    <MenuItem value="24">24</MenuItem>
                </MenuContent>
            </MenuRoot>
        </HStack>
        {/* {rows.map((x, idx)=><MnemonicWordRow startAt={}/>)} */}
      </VStack>
    );
  }

// interface MnemonicWordRowProps
// {
//     startAt:number
//     limit:number
// }

// function MnemonicWordRow({startAt, limit}:MnemonicWordRowProps)
// {
//     return <HStack>
//                 {}
//             </HStack>
// }


function updateMnemonicValue(
    s: any,
    onChange: (s: string) => void,
    setError: ((v: boolean | string) => void) | undefined
  ) {
    const strValue = String(s).trim();
    onChange(strValue);
    if (setError) {
      setError(strValue !== "" && !isValidMnemonic(strValue));
    }
  }

export function isValidMnemonic(mnemonic: string): boolean {
    const words = mnemonic.trim().split(/\s+/);
    return [12, 15, 18, 21, 24].includes(words.length);
  }