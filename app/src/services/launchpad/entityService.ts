import { BaseQueryFn, createApi, EndpointBuilder, FetchArgs, fetchBaseQuery, FetchBaseQueryError, FetchBaseQueryMeta } from "@reduxjs/toolkit/query/react";

type ApiEndPointBuilder = EndpointBuilder<
  BaseQueryFn<string | FetchArgs, unknown, FetchBaseQueryError, {}, FetchBaseQueryMeta>, 
  never, 
  "api"
>;

const fetchQuery = {baseUrl: import.meta.env.VITE_API_URL}

const getQuery = <T>(builder:ApiEndPointBuilder) => 
    builder.query<T, { entityName: string, uuid: string }>({
        query: ({ entityName, uuid }) => `${entityName}/${uuid}`,
    });

const listQuery = <T>(builder:ApiEndPointBuilder) =>
    builder.query<T[], string>({
        query: (entityName) => `${entityName}`,
    });
    
const createMutation = <T>(builder:ApiEndPointBuilder) =>
    builder.mutation<string, { entityName: string, data: T }>({
        query: ({ entityName, data }) => ({
          url: `${entityName}/new`,
          method: 'POST',
          body: data,
          responseHandler: async (response) => {
            const text = await response.text();
            return text;
          },
        }),
        transformResponse: (response: string) => {
          return response.trim();
        },
    });

const updateMutation = <T>(builder:ApiEndPointBuilder) =>
    builder.mutation<void, { entityName: string, uuid: string, data: T }>({
        query: ({ entityName, uuid, data }) => ({
        url: `${entityName}/${uuid}`,
        method: 'PUT',
        body: data,
        }),
    });

const deleteMutation = (builder:ApiEndPointBuilder) =>
    builder.mutation<any, { entityName: string, uuid: string }>({
        query: ({ entityName, uuid }) => ({
        url: `${entityName}/${uuid}`,
        method: 'DELETE',
        }),
    });

const basePath = {
    reducerPath:"api",
    baseQuery: fetchBaseQuery(fetchQuery),
    endpoints: (builder:ApiEndPointBuilder) => ({
        get:    getQuery(builder),
        list:   listQuery(builder),
        create: createMutation(builder),
        update: updateMutation(builder),
        delete: deleteMutation(builder),
    })
}

export function useEntity<T>(slug:string)
{
    const useGetQuery = (uuid: string) =>
        EntityApi.endpoints.get.useQuery({ entityName: slug, uuid });
    
    const useListsQuery = () =>
        EntityApi.endpoints.list.useQuery(slug);
    
    const useCreateMutation = () => {
        const mutation = EntityApi.endpoints.create.useMutation();
        return [
            (data:T ) =>
                mutation[0]({ entityName: slug, data }),
            mutation[1],
        ] as const;
    };
    
    const useUpdateMutation = () => {
        const mutation = EntityApi.endpoints.update.useMutation();
        return [
            ({ uuid, data }: { uuid: string; data: Partial<T> }) =>
                mutation[0]({ entityName: slug, uuid, data }),
            mutation[1],
        ] as const;
    };
    
    const useDeleteMutation = () => {
        const mutation = EntityApi.endpoints.delete.useMutation();
        return [
            (uuid: string) => mutation[0]({ entityName: slug, uuid }),
            mutation[1],
        ] as const;
    };

    return {get:useGetQuery, list:useListsQuery, create:useCreateMutation, update:useUpdateMutation, remove:useDeleteMutation}

}

export const EntityApi = createApi(basePath);