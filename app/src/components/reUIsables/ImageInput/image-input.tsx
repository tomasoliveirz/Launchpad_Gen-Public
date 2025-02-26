import { FileUploadList, FileUploadRoot, FileUploadTrigger } from "@/components/ui/file-upload";
import { Button } from "@chakra-ui/react";
import { useCallback } from "react";
import { UseFormSetValue } from "react-hook-form";
import { HiUpload } from "react-icons/hi";

export interface ImageUploadProps {
    setValue: UseFormSetValue<any>;
    formField: string;
}
export function ImageUpload({ setValue, formField }: ImageUploadProps) {
    const handleFileChange = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
      const file = event.target.files?.[0];
      if (file) {
        const reader = new FileReader();
        reader.onload = () => {
          const dataURL = reader.result as string;
          setValue(formField, dataURL);
        };
        reader.readAsDataURL(file); 
      }
    }, [setValue]);

    return (
        <FileUploadRoot onChange={handleFileChange}>
            <FileUploadTrigger asChild>
                <Button variant="outline" size="sm">
                    <HiUpload /> Upload Image
                </Button>
            </FileUploadTrigger>
            <FileUploadList />
        </FileUploadRoot>
    );
}