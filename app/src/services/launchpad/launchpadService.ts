import { ContractCharacteristic } from '@/models/ContractCharacteristic';
import { ContractType } from '@/models/ContractType'
import { ContractVariant } from '@/models/ContractVariant';
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const url = import.meta.env.VITE_APP_API_URL

export const launchpadApi = createApi({
  reducerPath: 'launchpadApi',
  baseQuery: fetchBaseQuery({ baseUrl: url }),
  tagTypes: ['ContractType', 'ContractCharacteristic', 'ContractVariant'],
  endpoints: (builder) => ({

  //Contract Types
    getContractTypes: builder.query<ContractType[], void>({
      query: () => `ContractTypes`,
    }),
    getContractType: builder.query<ContractType, string>({
      query: (uuid) => `ContractTypes/${uuid}`,
    }),
    createContractType: builder.mutation({
      query: (newContractType) => ({
        url: 'ContractTypes/new',
        method: 'POST',
        body: newContractType
      }),
      transformResponse: (response: { data: ContractType }) => response.data,

      transformErrorResponse: (
        response: { status: string | number },
      ) => response.status,
      invalidatesTags: ['ContractType'],
    }),

    updateContractType: builder.mutation<ContractType, { uuid: string; contractType: Partial<ContractType> }>({
      query: ({uuid, contractType}) => ({
        url: `ContractTypes/${uuid}`,
        method: 'PUT',
        body: contractType
      }),
      transformResponse: (response: { data: ContractType }) => {
        return response.data;
      },

      transformErrorResponse: (
        response: { status: string | number }) => response.status,
      invalidatesTags: ['ContractType'],
    }),

    removeContractType: builder.mutation<{ success: boolean }, string>({
      query: (uuid) => ({
        url: `ContractTypes/${uuid}`,
        method: 'DELETE',
      }),
      transformResponse: (response: { success: boolean }) => response,
      transformErrorResponse: (response: { status: string | number }) => response.status,
      invalidatesTags: ['ContractType'],
    }),


    //Contract Characteristics
    getContractCharacteristics: builder.query<ContractCharacteristic[], void>({
      query: () => `ContractCharacteristics`,
    }),
    getContractCharacteristic: builder.query<ContractCharacteristic, string>({
      query: (uuid) => `ContractCharacteristics/${uuid}`,
    }),
    createContractCharacteristic: builder.mutation({
      query: (newContractCharacteristic) => ({
        url: 'ContractCharacteristics/new',
        method: 'POST',
        body: newContractCharacteristic
      }),
      transformResponse: (response: { data: ContractCharacteristic }) => response.data,
      
      transformErrorResponse: (
        response: { status: string | number },
      ) => response.status,
      invalidatesTags: ['ContractCharacteristic'],
    }),

    updateContractCharacteristic: builder.mutation<ContractCharacteristic, { uuid: string; contractCharacteristic: Partial<ContractCharacteristic> }>({
      query: ({uuid, contractCharacteristic}) => ({
        url: `ContractCharacteristics/${uuid}`,
        method: 'PUT',
        body: contractCharacteristic
      }),
      transformResponse: (response: { data: ContractCharacteristic }) => {
        return response.data;
      },

      transformErrorResponse: (
        response: { status: string | number }) => response.status,
      invalidatesTags: ['ContractCharacteristic'],
    }),

    removeContractCharacteristic: builder.mutation<{ success: boolean }, string>({
      query: (uuid) => ({
        url: `ContractCharacteristics/${uuid}`,
        method: 'DELETE',
      }),
      transformResponse: (response: { success: boolean }) => response,
      transformErrorResponse: (response: { status: string | number }) => response.status,
      invalidatesTags: ['ContractCharacteristic'],
    }),


    //Contract Variants
    getContractVariants: builder.query<ContractVariant[], void>({
      query: () => `ContractVariants`,
    }),
    getContractVariant: builder.query<ContractVariant, string>({
      query: (uuid) => `ContractVariants/${uuid}`,
    }),
    createContractVariant: builder.mutation({
      query: (newContractVariant) => ({
        url: 'ContractVariants/new',
        method: 'POST',
        body: newContractVariant
      }),
      transformResponse: (response: { data: ContractVariant }) => response.data,

      transformErrorResponse: (
        response: { status: string | number },
      ) => response.status,
      invalidatesTags: ['ContractVariant'],
    }),

    updateContractVariant: builder.mutation<ContractVariant, { uuid: string; contractVariant: Partial<ContractVariant> }>({
      query: ({uuid, contractVariant}) => ({
        url: `ContractVariants/${uuid}`,
        method: 'PUT',
        body: contractVariant
      }),
      transformResponse: (response: { data: ContractVariant }) => {
        return response.data;
      },

      transformErrorResponse: (
        response: { status: string | number }) => response.status,
      invalidatesTags: ['ContractVariant'],
    }),

    removeContractVariant: builder.mutation<{ success: boolean }, string>({
      query: (uuid) => ({
        url: `ContractVariants/${uuid}`,
        method: 'DELETE',
      }),
      transformResponse: (response: { success: boolean }) => response,
      transformErrorResponse: (response: { status: string | number }) => response.status,
      invalidatesTags: ['ContractVariant'],
    }),
  }),
})

export const { 
  useGetContractTypeQuery,
  useGetContractTypesQuery,
  useCreateContractTypeMutation,
  useUpdateContractTypeMutation,
  useRemoveContractTypeMutation,

  useGetContractCharacteristicQuery,
  useGetContractCharacteristicsQuery,
  useCreateContractCharacteristicMutation,
  useUpdateContractCharacteristicMutation,
  useRemoveContractCharacteristicMutation,

  useGetContractVariantQuery,
  useGetContractVariantsQuery,
  useCreateContractVariantMutation,
  useUpdateContractVariantMutation,
  useRemoveContractVariantMutation
 } = launchpadApi