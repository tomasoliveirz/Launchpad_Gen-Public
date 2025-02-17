import { Entity } from "@/models/Entity"
import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";


const url = import.meta.env.VITE_APP_API_URL

export const launchpadApi = createApi({
    reducerPath: 'launchpadApi',
    baseQuery: fetchBaseQuery({ baseUrl: url }),
    tagTypes: ['Entity'],
    endpoints: (builder) => ({
        getAllEntity : builder.query<Entity[], { entityType: string }>({
            query: ({ entityType }) => `${entityType}`,
        }),
        getEntity: builder.query<Entity, { entityType: string, id: string }>({
            query: ({ entityType, id }) => `${entityType}/${id}`
        }),
        createEntity: builder.mutation<Entity, {entityType:string; newEntity: Omit<Entity, 'id'>} >({
            query: ({entityType, newEntity}) => ({
                url: `${entityType}/new`,
                method: 'POST',
                body: newEntity
            }),
        }),
        updateEntity: builder.mutation<Entity, { entityType: string, id: string, updatedEntity: Partial<Entity> }>({
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