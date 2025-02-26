import { toaster } from "@/components/ui/toaster";

export interface ToasterProps {
    title?: string;
    description: string;
    type?: string;
}
export function LaunchpadToaster({ title, description, type }: ToasterProps) {
    toaster.create({
        title: title,
        description: description,
        type: type,
    });
}

export const LaunchpadSuccessToaster = (description: string) => {
    toaster.create({
        title: "Success",
        description: description,
        type: "success"
    });
};

export const LaunchpadErrorToaster = (description: string) => {
    toaster.create({
        title: "Error",
        description: description,
        type: "error"
    });
};