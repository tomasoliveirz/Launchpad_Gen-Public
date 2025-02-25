import { ContractVariant } from "@/models/ContractVariant";
import { EntityApi } from "./testService";


const URL_SLUG = "/ContractVariants/withTypes";

const variantsApi = EntityApi.injectEndpoints({
    endpoints: (builder) => ({
      getVariantsWithTypeName: builder.query<ContractVariant[], void>({
        query: () => `${URL_SLUG}`,
      }),
    }),
    overrideExisting: false,
  });

  export function getVariantsWithTypeName() {  
    const useListQuery = variantsApi.useGetVariantsWithTypeNameQuery();
    return { ...variantsApi, useGetVariantsWithTypeNameQuery: useListQuery };
  }
  
  export { variantsApi };