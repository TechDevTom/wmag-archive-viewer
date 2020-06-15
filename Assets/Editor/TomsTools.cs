using UnityEngine;

using UnityEditor;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;


public class TomsTools : Editor
{
	#region Vuforia
	[MenuItem("TomsTools/Vuforia/CreateMultiMarker")]
	static void Doit() 
	{
		Object multiMarker = AssetDatabase.LoadAssetAtPath ("Assets/Vuforia/Prefabs/MultiTarget.prefab", typeof (GameObject));
		PrefabUtility.InstantiatePrefab(multiMarker);
	}
	#endregion

    #region Assets and Project
    [MenuItem("TomsTools/Assets and Project/Log All .asset Locations")]
    static void LogAllAssetFiles() 
    {
        List <string> assetFiles = Directory.GetFiles(Application.dataPath, "*asset", SearchOption.AllDirectories).ToList ();
        assetFiles.ForEach (x => Debug.Log (x.Replace (Application.dataPath, "")));
    }

    [MenuItem("TomsTools/Assets and Project/AssetPathToGUID")]
    static void AssetGUID() 
    {
        //     string t = AssetDatabase.AssetPathToGUID(Selection.assetGUIDs);
        foreach (string str in Selection.assetGUIDs)
        {
            Debug.Log(str);
        }
	}
	[MenuItem("TomsTools/Assets and Project/AssetPath")]
	static void AssetPath() 
	{
		foreach (string str in Selection.assetGUIDs)
		{
			Debug.Log(AssetDatabase.GUIDToAssetPath (str));
		}
    }

    [MenuItem("TomsTools/Assets and Project/Add Item To Hierarchy of Selected")]
    static void AddItemToPrefab() 
    {
        List <GameObject>   selected        = Selection.gameObjects.ToList ();
        GameObject          lastSelected    = selected.Last ();
        selected.Remove(selected.Last ());

        foreach (GameObject gamOb in selected)
        {
            PrefabUtility.ConnectGameObjectToPrefab(lastSelected, gamOb);
        }
    }

    [MenuItem ("TomsTools/Assets and Project/SetupSprites #&s")]
    public static void SetupSprites ()
    {
        int[] sizes = new int[]
            {
                8192,
                4096,
                2048,
                1024,
                512,
                256,
                128,
                64,
                32
            };
        int difference  = 0;
        int reSize      = 0;

        Object[] objects = Selection.objects;

		
        if(objects.Length == 0)
		{
            objects     = Selection.GetFiltered (typeof (Texture2D), SelectionMode.DeepAssets);
            objects.ToList().AddRange(Selection.GetFiltered (typeof (Sprite), SelectionMode.DeepAssets).ToList ());
		}


        foreach (Object obj in objects)
        {
            string texturePath = AssetDatabase.GetAssetPath(obj);
            Texture2D texture = (Texture2D)obj;

            TextureImporter tImporter = AssetImporter.GetAtPath(texturePath) as TextureImporter;
            tImporter.textureType = TextureImporterType.Sprite;
            AssetDatabase.ImportAsset(texturePath);

            tImporter.maxTextureSize = sizes[0];    // Make sure the size of the texture is set to the max, then shrink in down from there.

            difference = 999999;
            foreach (int size in sizes)
            {
                if (texture.width <= size && texture.height <= size)
                {
                    reSize = size;
                }
            }
			tImporter.spriteImportMode = SpriteImportMode.Single;
            tImporter.textureCompression = TextureImporterCompression.CompressedHQ;
            tImporter.maxTextureSize = reSize;
            tImporter.mipmapEnabled = false;
            tImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor;
            tImporter.wrapMode = TextureWrapMode.Clamp;
            tImporter.filterMode = FilterMode.Point;
            tImporter.spritePackingTag = "";
            AssetDatabase.ImportAsset(texturePath);
        }
	}

	[MenuItem("TomsTools/Assets and Project/Textures/Advanced Fix")]
	//  [Tooltip ("Use this tool to assign a Material to every Material slot in every Renderer underneath a selected parent GameObject. Select a parent GameObject.  Select a Material.  Press this button!")]
	public static void Texture_AdvancedFix ()
	{
		Object[] textureObjects 	= Selection.GetFiltered (typeof (Texture), SelectionMode.DeepAssets);

		int count = 0;
		foreach (Object obj in textureObjects)
		{
			string texturePath = AssetDatabase.GetAssetPath(obj);
			Texture texture = (Texture) obj;

			TextureImporter tImporter = AssetImporter.GetAtPath(texturePath) as TextureImporter;

			if(tImporter != null && texturePath != "")
			{
				if (tImporter.textureType == TextureImporterType.SingleChannel)
				{
					tImporter.textureType = TextureImporterType.Default;
					tImporter.textureShape = TextureImporterShape.Texture2D;
					AssetDatabase.ImportAsset(texturePath);
					count += 1;
					Debug.Log(texturePath + ",  " + count);
				}
			}
		}

//		Renderer[] renderers = parent.GetComponentsInChildren <Renderer> (true);
//
//		Debug.Log (material.color);
//		int rendererCount = 0;
//		int materialCount = 0;
//		foreach (Renderer renderer in renderers)
//		{
//			List <Material> materialsList = new List<Material> ();
//			for (int i = 0; i < renderer.materials.Length; i++)
//			{
//				materialsList.Add (material);
//				materialCount += 1;
//			}
//			renderer.materials = materialsList.ToArray ();
//			rendererCount += 1;
//		}
//		Debug.Log ("Changed " + materialCount + " Materials on " + rendererCount + "Renderers.");
	}
    #endregion Assets and Project



    #region Hierarchy
    [MenuItem("TomsTools/Hierarchy/Materials/MaterialAssigner")]
    //  [Tooltip ("Use this tool to assign a Material to every Material slot in every Renderer underneath a selected parent GameObject. Select a parent GameObject.  Select a Material.  Press this button!")]
    public static void MaterialAssigner ()
    {
        Object[]    selected    = Selection.objects;
        GameObject  parent      = null;
        Material    material    = null;
        foreach (Object obj in selected)
        {
            if (obj.GetType () == typeof(Material))
            {
                material = (Material) obj;
            }
            if (obj.GetType () == typeof(GameObject))
            {
                parent = (GameObject) obj;
            }
        }

        MeshRenderer[] renderers = parent.GetComponentsInChildren <MeshRenderer> (true);
        SkinnedMeshRenderer[] skinnedRenderers = parent.GetComponentsInChildren <SkinnedMeshRenderer> (true);

            Debug.Log (material.color);
//        int rendererCount = 0;
//        int materialCount = 0;
        foreach (MeshRenderer renderer in renderers)
        {
            AssignMaterials(renderer, material);

//            List <Material> materialsList = new List<Material> ();
//            for (int i = 0; i < renderer.materials.Length; i++)
//            {
//                materialsList.Add (material);
//                materialCount += 1;
//            }
//            renderer.materials = materialsList.ToArray ();
//            rendererCount += 1;
        }
        foreach (SkinnedMeshRenderer renderer in skinnedRenderers)
        {
            AssignMaterials(renderer, material);
        }
//        Debug.Log ("Changed " + materialCount + " Materials on " + rendererCount + "Renderers.");
    }

    public static void AssignMaterials (Renderer renderer, Material material)
    {
        int rendererCount = 0;
        int materialCount = 0;

        List <Material> materialsList = new List<Material> ();
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            materialsList.Add (material);
            materialCount += 1;
        }
        renderer.materials = materialsList.ToArray ();
        rendererCount += 1;
    }

    [MenuItem("TomsTools/Hierarchy/ObjectCounter")]
    public static void ObjectCounter ()
    {
        Transform selected = Selection.activeTransform;
        Transform[] transforms= selected.GetComponentsInChildren <Transform> (true);
        int transformCount = 0;
        foreach (Transform trans in transforms)
        {
            transformCount += 1;
        }
        Debug.Log ("There are " + transformCount + " GameObjects in this hierarchy.");
    }

//    [MenuItem("TomsTools/Hierarchy/ParticleSystems/ChangeSortLayerValue")]
//    public static void ObjectCounter ()
//    {
//        Transform selected = Selection.activeTransform;
//        Transform[] transforms  = selected.GetComponentsInChildren <Transform> (true);
//        foreach (Transform trans in transforms)
//        {
//            partir pSys = trans.GetComponent <ParticleSystem>();
//            pSys.
//        }
//    }

    [MenuItem("TomsTools/Hierarchy/Sprites/MoveLayerForward #&=")]
    public static void Sprites_MoveLayerForward ()
    {
        GameObject[] selected = Selection.gameObjects;
        foreach (GameObject gamOb in selected)
        {
            SpriteRenderer spriteRenderer = gamOb.GetComponent <SpriteRenderer> ();
            spriteRenderer.sortingOrder += 1;
        }

    }
    [MenuItem("TomsTools/Hierarchy/Sprites/MoveLayerBackward #&-")]
    public static void Sprites_MoveLayerBackward ()
    {
        GameObject[] selected = Selection.gameObjects;
        foreach (GameObject gamOb in selected)
        {
            SpriteRenderer spriteRenderer = gamOb.GetComponent <SpriteRenderer> ();
            spriteRenderer.sortingOrder -= 1;
        }
    }
    #endregion Hierarchy


    #region Specific/Unstable Prototypes
    [MenuItem ("TomsTools/Unstable Prototypes/MeshChanger_")]
    public static void MeshChanger ()
    {
        foreach (GameObject gamOb in Selection.gameObjects)
        {
            MeshFilter meshFilter = gamOb.GetComponent <MeshFilter> ();
            meshFilter.mesh = (Mesh)AssetDatabase.LoadAssetAtPath ("Assets/3DAssets/people_sitting_armsOnKnees01.fbx", typeof(Mesh));
        }

        //Assets/3DAssets/people_sitting_armsOnKnees01.fbx
        Debug.Log ("Mesh Changed");
    }
    #endregion Specific/Unstable Prototypes
}
