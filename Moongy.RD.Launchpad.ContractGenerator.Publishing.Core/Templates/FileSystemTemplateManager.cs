using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Interfaces;

namespace Moongy.RD.Launchpad.ContractGenerator.Publishing.Core.Templates
{
    /// <summary>
    /// Provides template content from the file system with caching capabilities.
    /// </summary>
    public class FileSystemTemplateManager : ITemplateManager
    {
        private readonly Dictionary<string, string> _templateCache = new Dictionary<string, string>();
        private readonly string _basePath;
        private readonly bool _useCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemTemplateManager"/> class.
        /// </summary>
        /// <param name="basePath">The base directory where templates are located.</param>
        /// <param name="useCache">Indicates whether to cache templates after first load.</param>
        public FileSystemTemplateManager(string basePath, bool useCache = true)
        {
            _basePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
            _useCache = useCache;
        }

        /// <inheritdoc/>
        public string GetTemplate(string templatePath)
        {
            if (string.IsNullOrEmpty(templatePath))
                throw new ArgumentNullException(nameof(templatePath));

            // Check cache first if enabled
            if (_useCache && _templateCache.TryGetValue(templatePath, out var cachedTemplate))
                return cachedTemplate;

            var template = LoadTemplateFromFileSystem(templatePath);
            
            // Cache the result if enabled
            if (_useCache)
                _templateCache[templatePath] = template;
            
            return template;
        }

        /// <inheritdoc/>
        public async Task<string> GetTemplateAsync(string templatePath)
        {
            if (string.IsNullOrEmpty(templatePath))
                throw new ArgumentNullException(nameof(templatePath));

            // Check cache first if enabled
            if (_useCache && _templateCache.TryGetValue(templatePath, out var cachedTemplate))
                return cachedTemplate;

            var template = await LoadTemplateFromFileSystemAsync(templatePath);
            
            // Cache the result if enabled
            if (_useCache)
                _templateCache[templatePath] = template;
            
            return template;
        }

        /// <inheritdoc/>
        public void ClearCache()
        {
            _templateCache.Clear();
        }

        private string LoadTemplateFromFileSystem(string templatePath)
        {
            // Build the primary path based on the configured base path
            var fullPath = Path.Combine(_basePath, templatePath);
            
            // Check if the primary path exists
            if (File.Exists(fullPath))
                return File.ReadAllText(fullPath);

            // Try alternative locations for resilience
            var alternativePaths = GetAlternativeTemplatePaths(templatePath);
            
            foreach (var path in alternativePaths)
            {
                if (File.Exists(path))
                    return File.ReadAllText(path);
            }

            // No template found in any location
            throw new FileNotFoundException($"Template file not found: {templatePath}", templatePath);
        }

        private async Task<string> LoadTemplateFromFileSystemAsync(string templatePath)
        {
            // Build the primary path based on the configured base path
            var fullPath = Path.Combine(_basePath, templatePath);
            
            // Check if the primary path exists
            if (File.Exists(fullPath))
                return await File.ReadAllTextAsync(fullPath);

            // Try alternative locations for resilience
            var alternativePaths = GetAlternativeTemplatePaths(templatePath);
            
            foreach (var path in alternativePaths)
            {
                if (File.Exists(path))
                    return await File.ReadAllTextAsync(path);
            }

            // No template found in any location
            throw new FileNotFoundException($"Template file not found: {templatePath}", templatePath);
        }

        private static string[] GetAlternativeTemplatePaths(string templatePath)
        {
            return new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", templatePath),
                Path.Combine(Environment.CurrentDirectory, "Templates", templatePath),
                Path.Combine(Path.GetDirectoryName(typeof(FileSystemTemplateManager).Assembly.Location) ?? string.Empty, "Templates", templatePath)
            };
        }
    }
}