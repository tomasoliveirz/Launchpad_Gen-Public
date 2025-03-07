import { ContractVariant } from "@/models/ContractVariant";
import { EntityApi, useEntity } from "./entityService";


const URL_SLUG = "ContractVariants";

const characteristicInVariantApi = EntityApi.injectEndpoints({
  endpoints: (builder) => ({
    getCharacteristicsByVariantUuid: builder.query<any[], { contractVariantUuid: string }>({
      query: ({ contractVariantUuid }) => `${URL_SLUG}/${contractVariantUuid}/characteristics`,
    }),
  }),
  overrideExisting: false,
});

export function useCharacteristicInVariantApi() {
  const entityApi = useEntity<ContractVariant>(URL_SLUG);

  const useGetCharacteristicsByVariantUuid = (contractVariantUuid: string) => {
    const result = characteristicInVariantApi.useGetCharacteristicsByVariantUuidQuery({ contractVariantUuid });
    return [() => result.refetch(), result] as const; 
  };

  return { ...entityApi, useGetCharacteristicsByVariantUuid };
}

export { characteristicInVariantApi };