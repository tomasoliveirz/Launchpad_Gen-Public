import { ContractVariant } from "@/models/ContractVariant";

export const contractVariantsData: ContractVariant[] = [
  {
    uuid: "a",
    name: "Standard Employment",
    description: "A full-time employment contract with standard benefits.",
    contractType: {
      uuid: "1a2b3c",
    }
  },
  {
    uuid: "b",
    name: "Freelance Agreement",
    description: "A short-term contract for freelance work.",
    contractType: {
      uuid: "4d5e6f",
    }
  },
  {
    uuid: "c",
    name: "Internship Contract",
    description: "An agreement for interns with limited working hours.",
    contractType: {
      uuid: "1a2b3c",
    }
  },
  {
    uuid: "d",
    name: "Consulting Agreement",
    description: "A contract for external consultants providing specialized services.",
    contractType: {
      uuid: "4d5e6f",
    }
  },
  {
    uuid: "e",
    name: "Fixed-Term Contract",
    description: "A temporary employment contract with a set end date.",
    contractType: {
      uuid: "3m4n5o",
    }
  },
];