import { Entity } from "@/models/Entity"
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";


const url = import.meta.env.VITE_APP_API_URL

export const launchpadApi = createApi({
    reducerPath: 'launchpadApi',
    baseQuery: fetchBaseQuery({ baseUrl: url }),
    tagTypes: ['Entity'],
    endpoints: <T> (builder) => ({
        getAllEntity : builder.query<T[], { entityType: string }>({
            query: ({ entityType }) => `${entityType}`,
        }),
        getEntity: builder.query<T, { entityType: string, id: string }>({
            query: ({ entityType, id }) => `${entityType}/${id}`
        }),
        createEntity: builder.mutation<T, {entityType:string; newEntity: Omit<T, 'id'>} >({
            query: ({entityType, newEntity}) => ({
                url: `${entityType}/new`,
                method: 'POST',
                body: newEntity
            }),
        }),
        updateEntity: builder.mutation<None, { entityType: string, id: string, updatedEntity: Partial<T> }>({
            query: ({ entityType, id, updatedEntity }) => ({
                url: `${entityType}/${id}`,
                method: 'PATCH',
                body: updatedEntity
            }),
        }),
        deleteEntity: builder.mutation<void, { entityType: string; id: string | number }>({
            query: ({ entityType, id }) => ({
                url: `/${entityType}/${id}`,
                method: 'DELETE'
            })
        })
    })
})

export const {
    useGetAllEntityQuery,
    useGetEntityQuery,
    useCreateEntityMutation,
    useUpdateEntityMutation,
    useDeleteEntityMutation
} = launchpadApi;