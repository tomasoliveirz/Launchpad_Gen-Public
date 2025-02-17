import { ContractType } from '@/models/ContractType'
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const url = import.meta.env.VITE_APP_API_URL


export const launchpadApi = createApi({
  reducerPath: 'launchpadApi',
  baseQuery: fetchBaseQuery({ baseUrl: url }),
  tagTypes: ['ContractType'],
  endpoints: (builder) => ({
    getContractTypes: builder.query<ContractType[], void>({
      query: () => `contractTypes`,
    }),
    getContractType: builder.query<ContractType, string>({
      query: (id) => `contractTypes/${id}`,
    }),
    createContractType: builder.mutation({
      query: (newContractType) => ({
        url: 'ContractTypes/new',
        method: 'POST',
        body: newContractType
      }),
      transformResponse: (response: { data: ContractType }, meta, arg) => response.data,

      transformErrorResponse: (
        response: { status: string | number },
        meta,
        arg,
      ) => response.status,
      invalidatesTags: ['ContractType'],
    }),

    updateContractType: builder.mutation<ContractType, string>({
      query: (uuid, ...contractType) => ({
        url: `ContractTypes/${uuid}`,
        method: 'PATCH',
        body: contractType
      }),
      transformResponse: (response: { data: ContractType }, meta, arg) => response.data,

      transformErrorResponse: (
        response: { status: string | number },
        meta,
        arg,
      ) => response.status,
      invalidatesTags: ['ContractType'],
    })
  }),
})

export const { 
  useGetContractTypeQuery,
  useGetContractTypesQuery,
  useCreateContractTypeMutation,
  useUpdateContractTypeMutation,
 } = launchpadApi