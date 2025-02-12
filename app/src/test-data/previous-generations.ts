import { PreviousGeneration } from "@/models/PreviousGeneration";

export  const previousGenerationsData: PreviousGeneration[] = [
    { uuid: "a", name: "test 1", createdAt: new Date("2024-01-01T10:00:00Z"), description: "First test item", contractVariantUuid:"a" },
    { uuid: "b", name: "test 2", createdAt: new Date("2024-01-02T11:30:00Z"), description: "Second test item", contractVariantUuid:"b" },
    { uuid: "c", name: "test 3", createdAt: new Date("2024-01-03T14:15:00Z"), description: "Third test item with more details", contractVariantUuid:"c" },
    { uuid: "d", name: "test 4", createdAt: new Date("2024-01-04T09:45:00Z"), description: "Another test record", contractVariantUuid:"a" },
    { uuid: "e", name: "test 5", createdAt: new Date("2024-01-05T16:20:00Z"), description: "Some extra description for test 5" , contractVariantUuid:"a"},
    { uuid: "f", name: "test 6", createdAt: new Date("2024-01-06T18:05:00Z"), description: "" , contractVariantUuid:"d"},
    { uuid: "g", name: "test 7", createdAt: new Date("2024-01-07T07:50:00Z"), description: "This one has an early timestamp", contractVariantUuid:"c" },
    { uuid: "h", name: "test 8", createdAt: new Date("2024-01-08T22:10:00Z"), description: "Latest item for testing" , contractVariantUuid:"e"}
];

