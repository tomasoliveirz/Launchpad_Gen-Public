import { ContractType } from "@/models/ContractType";
import { EntityApi, useEntity } from "./entityService";

const URL_SLUG = "ContractTypes";

const featureInTypeApi = EntityApi.injectEndpoints({
  endpoints: (builder) => ({
    getFeaturesByTypeUuid: builder.query<any[], { contractTypeUuid: string }>({
      query: ({ contractTypeUuid }) => `${URL_SLUG}/${contractTypeUuid}/features`,
    }),
  }),
  overrideExisting: false,
});

export function usefeatureInTypeApi() {
  const entityApi = useEntity<ContractType>(URL_SLUG);

  const useGetFeaturesByTypeUuid = (contractTypeUuid: string) => {
    const result = featureInTypeApi.useGetFeaturesByTypeUuidQuery({ contractTypeUuid });
    return [() => result.refetch(), result] as const; 
  };

  return { ...entityApi, useGetFeaturesByTypeUuid };
}

export { featureInTypeApi };