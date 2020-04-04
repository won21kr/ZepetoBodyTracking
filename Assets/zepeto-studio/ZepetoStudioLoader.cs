using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using Zepeto;

public class ZepetoStudioLoader : MonoBehaviour
{
    private ZepetoCharacterCustomLoader _loader;

    /////////////////////////////////////////////
    /// CLOTH
    /////////////////////////////////////////////
    [MenuItem("Assets/Zepeto Studio/Convert to ZEPETO style", false)]
    public static void ConvertCloth()
    {
        ZepetoModelImporter.ConvertAsset(ZepetoClothModelImporter.ConvertClothWithBones);
    }

    private static void ArchiveEntry(ZipArchive zipFile, string srcFile, string dest)
    {
        if (string.IsNullOrEmpty(srcFile) || File.Exists(srcFile) == false) return;

        var entry = zipFile.CreateEntry(dest);
        using (var stream = entry.Open())
        {
            var bytes = File.ReadAllBytes(srcFile);
            stream.Write(bytes, 0, bytes.Length);
        }
    }

    [MenuItem("Assets/Zepeto Studio/Export as .zepeto", false, 100)]
    public static void Archive()
    {
        if (Selection.activeGameObject == null)
            return;

        var path = AssetDatabase.GetAssetPath(Selection.activeGameObject);
        var dependencies = AssetDatabase.GetDependencies(path, false).ToList();

        var result = new StringBuilder();
        result.AppendLine("[ZEPETO STUDIO ARCHIVE RESULT]");


        var parentDirectory = Path.GetDirectoryName(path);
        var nameWithoutExtension = Path.GetFileNameWithoutExtension(path);

        if (string.IsNullOrEmpty(parentDirectory))
        {
            return;
        }


        // Add Extra Dependencies
        var includeRegex = new Regex("#include\\s+\"([^\"]+)\"", RegexOptions.Multiline);
        foreach (var dependency in dependencies.ToArray())
        {
            if (string.IsNullOrEmpty(dependency) || File.Exists(dependency) == false) continue;
            if (!Path.GetExtension(dependency).Equals(".shader", StringComparison.OrdinalIgnoreCase)) continue;

            var shaderCode = File.ReadAllText(dependency);
            var allMatches = includeRegex.Matches(shaderCode);
            dependencies.AddRange(allMatches.Cast<Match>()
                .Select(match => Path.Combine(parentDirectory, match.Groups[1].Value)));
        }

        var zipFilePath = Path.Combine(parentDirectory, nameWithoutExtension + ".zepeto");
        if (File.Exists(zipFilePath)) File.Delete(zipFilePath);

        dependencies.Add(path);

        result.AppendLine($"# {dependencies.Count} Files");

        using (var zipFile = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
        {
            foreach (var dependency in dependencies.Where(dependency =>
                !string.IsNullOrEmpty(dependency) && File.Exists(dependency)))
            {
                ArchiveEntry(zipFile, dependency, Path.GetFileName(dependency));
                ArchiveEntry(zipFile, dependency + ".meta", Path.GetFileName(dependency) + ".meta");

                result.AppendLine(dependency);
            }
        }

        AssetDatabase.Refresh();

        Debug.Log(result.ToString());
    }

    [MenuItem("Assets/Zepeto Studio/Archive", true, 100)]
    public static bool ArchiveValidation()
    {
        if (Selection.activeGameObject == null)
            return false;
        var path = AssetDatabase.GetAssetPath(Selection.activeGameObject);
        return string.IsNullOrEmpty(path) == false &&
               Path.GetExtension(path).Equals(".prefab", StringComparison.OrdinalIgnoreCase);
    }


    private void Start()
    {
        StartCoroutine(OnLoad());
    }

    private IEnumerator OnLoad()
    {
        _loader = GetComponent<ZepetoCharacterCustomLoader>();
        var internalResource =
            ZepetoStreamingAssets.GetAssetPath(ZepetoSDKEditorSettings.InternalResourceAssetBundleName);
        var internalResourceRequest = ZepetoAssetBundleRequest.Create(internalResource);
        yield return internalResourceRequest;
        var assetBundle = internalResourceRequest.assetBundleRef.AssetBundle;
        var materials = Resources.LoadAll<Material>(ZepetoInitializer.BASE_MODEL_RESOURCE);
        foreach (var current in materials)
        {
            switch (current.name)
            {
                case "eye":
                    current.shader = assetBundle.LoadAsset<Shader>("assets/zepeto-sdk/resources-internal/eye.shader");
                    break;
                case "body":
                    current.shader = assetBundle.LoadAsset<Shader>("assets/zepeto-sdk/resources-internal/skin.shader");
                    break;
            }
        }

        _loader.enabled = true;
    }
}

[ScriptedImporter(1, "zepeto")]
public class ZepetoStudioImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        var texture = Resources.Load<Texture2D>("zepeto-studio-icon");
        ctx.AddObjectToAsset("Texture", texture, texture);
    }
}