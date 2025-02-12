import { ContractType } from '@/models/ContractType'
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const url = import.meta.env.VITE_APP_API_URL

export const launchpadApi = createApi({
  reducerPath: 'launchpadApi',
  baseQuery: fetchBaseQuery({ baseUrl: url }),
  endpoints: (builder) => ({
    getContractType: builder.query<ContractType, string>({
      query: (id) => `contractTypes/${id}`,
    }),
    // createContractType: builder.mutation
  }),
})

export const { useGetContractTypeQuery } = launchpadApi