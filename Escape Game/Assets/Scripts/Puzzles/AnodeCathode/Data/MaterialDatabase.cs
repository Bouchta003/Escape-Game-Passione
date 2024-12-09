using UnityEngine;
using System.Collections.Generic;

public static class MaterialDatabase
{
    private static Dictionary<string, MaterialData> materials = new Dictionary<string, MaterialData>();
    private static bool isInitialized = false;

    /// <summary>
    /// Initializes the database by loading all MaterialData from the Resources folder.
    /// </summary>
    public static void InitializeDatabase()
    {
        if (isInitialized) return; // Avoid reinitializing

        MaterialData[] loadedMaterials = Resources.LoadAll<MaterialData>("ScriptableObjects/MaterialData");
        Debug.Log(loadedMaterials);
        foreach (MaterialData material in loadedMaterials)
        {
            RegisterMaterial(material);
        }

        Debug.Log($"MaterialDatabase initialized with {materials.Count} materials.");
        isInitialized = true;
    }

    /// <summary>
    /// Registers a material in the database.
    /// </summary>
    public static void RegisterMaterial(MaterialData material)
    {
        if (!materials.ContainsKey(material.materialName))
        {
            materials.Add(material.materialName, material);
            Debug.Log($"Registered material: {material.materialName}");
        }
        else
        {
            Debug.LogWarning($"Material {material.materialName} is already registered!");
        }
    }

    /// <summary>
    /// Retrieves a material by name from the database.
    /// </summary>
    public static MaterialData GetMaterialData(string materialName)
    {
        if (materials.TryGetValue(materialName, out var material))
        {
            return material;
        }

        Debug.LogWarning($"Material {materialName} not found in database!");
        return null;
    }
}