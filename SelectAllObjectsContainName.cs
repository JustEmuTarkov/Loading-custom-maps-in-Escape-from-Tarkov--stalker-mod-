﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelectAllObjectsContainName : EditorWindow
{
    [MenuItem("Terrain/Select to", false, 2000)]
    static void OpenWindow()
    {


        GameObject[] objects = FindObjectsOfType<GameObject>();
        foreach (GameObject gameObject in objects)
        {
            string textureName = gameObject.name.Substring(gameObject.name.LastIndexOf("/") + 1, gameObject.name.Length - gameObject.name.LastIndexOf("/") - 1);
            if (textureName.IndexOf(".") > 0)
            {
                textureName = textureName.Substring(0, textureName.IndexOf(".")) + ".dds";
                textureName = textureName.Replace("\\", "/");
                Material mat = (Material)AssetDatabase.LoadAssetAtPath("Assets/Materials/" + textureName + ".mat", typeof(Material));
                if (!mat)
                {
                    Texture main = (Texture)AssetDatabase.LoadAssetAtPath("Assets/textures/" + textureName, typeof(Texture));
                    if (main)
                    {
                        mat = new Material(Shader.Find("Diffuse"));
                        mat.SetTexture("_MainTex", main);
                        AssetDatabase.CreateAsset(mat, "Assets/Materials/" + textureName + ".mat");
                        Debug.Log("Creating mat " + "Assets/Materials/" + textureName + ".mat");
                    }
                }
                else Debug.Log("Using mat: " + "Assets/Materials/" + textureName + ".mat");
                if (!mat) Debug.LogError("No mat for " +gameObject.name + " " + textureName );
                gameObject.GetComponent<Renderer>().material = mat;
                gameObject.GetComponent<Renderer>().sharedMaterial = mat;
            }

            //if (gameObject.name.Contains("crete_dirt_2"))
            //{
            //    Debug.Log(gameObject);
            //    Object[] selectedObjects = new Object[Selection.objects.Length + 1];
            //    for (int i = 0; i < Selection.objects.Length; i++)
            //    {
            //        selectedObjects[i] = Selection.objects[i];
            //    }
            //    selectedObjects[Selection.objects.Length] = gameObject;
            //    Selection.objects = selectedObjects;

            //}
        }
    }

}
