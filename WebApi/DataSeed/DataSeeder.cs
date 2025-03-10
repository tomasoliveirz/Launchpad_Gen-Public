using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Moongy.RD.Launchpad.Data.Base;
using Moongy.RD.Launchpad.Data.Contexts;
using Moongy.RD.Launchpad.Data.Entities;
using Newtonsoft.Json;

namespace WebApi.DataSeed;

public static class DataSeeder
{
    private const string SeedFilesRoot = "DataSeed/Data.json";

    public class SeedData
    {
        public List<BlockchainNetwork> Networks { get; set; }
        public List<ContractType> ContractTypes { get; set; }
        public List<ContractFeature> ContractFeatures { get; set; }
        public List<ContractCharacteristic> ContractCharacteristics { get; set; }
        public List<ContractVariant> ContractVariants { get; set; }
        public List<CharacteristicInContractVariant> CharacteristicInContractVariant { get; set; }
        public List<ContractGenerationResult> GenerationResults { get; set; }
        public List<PublishResult> PublishResults { get; set; }
        public List<FeatureOnContractFeatureGroup> FeatureOnContractFeatureGroups { get; set; }
        public List<ContractFeatureGroup> ContractFeatureGroups { get; set; }
        public List<GenerationFeatureValue> GenerationFeatureValues { get; set; }


    }

    public static async Task Seed(LaunchpadContext context)
    {
        var data = DataToObjects();
        if (data == null) return;
        RelationshipProcessor.AutoProcessAllRelationships(data);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        await SeedEntitiesAsync(context, data.Networks);
        await SeedEntitiesAsync(context, data.ContractTypes);
        await SeedEntitiesAsync(context, data.ContractVariants);
        await SeedEntitiesAsync(context, data.ContractCharacteristics);


    }

 
    private static SeedData? DataToObjects()
    {
        var executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        var fullPath = Path.Combine(executionPath, SeedFilesRoot);
        if (!File.Exists(fullPath)) throw new FileNotFoundException();
        var jsonData = File.ReadAllText(fullPath);
        var deserializedData = JsonConvert.DeserializeObject<SeedData>(jsonData);
        return deserializedData;
    }

   


    public static async Task SeedEntitiesAsync<T>(DbContext context, List<T> entities) where T : Entity
    {
        var relatedEntityCache = new Dictionary<object, Dictionary<PropertyInfo, object>>();

        foreach (var entity in entities)
        {
            entity.Id = 0;
            var relatedEntities = new Dictionary<PropertyInfo, object>();

            foreach (var property in entity.GetType().GetProperties())
            {
                if (!typeof(IEnumerable).IsAssignableFrom(property.PropertyType) || property.PropertyType == typeof(string))
                    continue; // Skip non-collections

                var collection = property.GetValue(entity) as IEnumerable;
                if (collection != null)
                {
                    relatedEntities[property] = collection; // Store the related entities
                    property.SetValue(entity, null); // Temporarily remove them
                }
            }

            relatedEntityCache[entity] = relatedEntities;
        }

        // Step 2: Add only the parent entities
        context.AddRange(entities);
        await context.SaveChangesAsync();

        // Step 3: Restore related collections (without persisting them)
        foreach (var entity in entities)
        {
            if (relatedEntityCache.TryGetValue(entity, out var properties))
            {
                foreach (var kvp in properties)
                {
                    kvp.Key.SetValue(entity, kvp.Value); // Restore the collections
                }
            }
        }

        // Step 2: Process relationships dynamically
        foreach (var entity in entities)
        {
            foreach (var property in entity.GetType().GetProperties())
            {
                if (!typeof(IEnumerable).IsAssignableFrom(property.PropertyType) || property.PropertyType == typeof(string))
                    continue; // Skip non-collections

                var relatedEntities = property.GetValue(entity) as IEnumerable;
                if (relatedEntities == null) continue;

                foreach (var relatedEntity in relatedEntities)
                {
                    // Find ForeignKey property in the related entity that points to this entity
                    var foreignKeyProperty = relatedEntity.GetType().GetProperties()
                        .FirstOrDefault(p => p.GetCustomAttribute<ForeignKeyAttribute>()?.Name == entity.GetType().Name);

                    if (foreignKeyProperty != null)
                    {
                        // Set foreign key value to the primary key of the current entity
                        var primaryKey = entity.GetType().GetProperty("Id")?.GetValue(entity);
                        foreignKeyProperty.SetValue(relatedEntity, primaryKey);
                    }
                }
            }
        }

    }


}
public static class RelationshipProcessor
{
    public static void AutoProcessAllRelationships(object data)
    {
        var dataType = data.GetType();
        var properties = dataType.GetProperties();

        foreach (var sourceCollectionProperty in properties)
        {
            if (!typeof(IEnumerable).IsAssignableFrom(sourceCollectionProperty.PropertyType) ||
                sourceCollectionProperty.PropertyType == typeof(string))
                continue; // Skip non-collections

            var sourceCollection = sourceCollectionProperty.GetValue(data) as IEnumerable;
            if (sourceCollection == null) continue;

            var sourceElementType = sourceCollectionProperty.PropertyType.GetGenericArguments().FirstOrDefault();
            if (sourceElementType == null) continue;

            foreach (var foreignKeyProperty in sourceElementType.GetProperties().Where(p => p.GetCustomAttribute<ForeignKeyAttribute>() != null))
            {
                var targetEntityName = foreignKeyProperty.GetCustomAttribute<ForeignKeyAttribute>()!.Name;
                var targetCollectionProperty = properties.FirstOrDefault(p => p.Name == targetEntityName + "s");
                if (targetCollectionProperty == null) continue; // Skip if target collection is not found

                var targetCollection = targetCollectionProperty.GetValue(data) as IEnumerable;
                if (targetCollection == null) continue;

                var targetElementType = targetCollectionProperty.PropertyType.GetGenericArguments().FirstOrDefault();
                if (targetElementType == null) continue;

                var targetKeyProperty = targetElementType.GetProperty("Id");
                if (targetKeyProperty == null) continue;

                // Create dictionary for fast lookup
                var targetDict = targetCollection.Cast<object>().ToDictionary(
                    target => targetKeyProperty.GetValue(target),
                    target => target
                );

                var sourceNavProperty = sourceElementType.GetProperty(targetEntityName);
                var targetNavProperty = targetElementType.GetProperty(sourceCollectionProperty.Name);

                foreach (var source in sourceCollection)
                {
                    var foreignKeyValue = foreignKeyProperty.GetValue(source);
                    if (foreignKeyValue == null || !targetDict.TryGetValue(foreignKeyValue, out var targetEntity)) continue;

                    // Assign reference navigation property
                    sourceNavProperty?.SetValue(source, targetEntity);

                    // Assign collection navigation property
                    var targetList = targetNavProperty?.GetValue(targetEntity) as IList;
                    targetList?.Add(source);
                }
            }
        }
    }
}