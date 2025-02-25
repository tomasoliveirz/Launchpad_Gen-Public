import { ContractVariant } from "@/models/ContractVariant";
import { EntityApi } from "./testService";


const URL_SLUG = "/ContractVariants/withTypes";

const variantsApi = EntityApi.injectEndpoints({
    endpoints: (builder) => ({
     
    }),
    overrideExisting: false,
  });


  export { variantsApi };