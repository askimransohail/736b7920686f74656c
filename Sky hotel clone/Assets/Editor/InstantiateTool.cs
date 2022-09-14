// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplySelectedPrefabs.cs" company="WittySol">
//   Copyright (c) 2018 WittySol. All rights reserved.
// </copyright>

// <author>
//   Adeel Riaz
//   adeelwitty@gmail.com
// </author>
// --------------------------------------------------------------------------------------------------------------------

namespace WittySol.EditorTools
{

    using UnityEngine;
    using UnityEditor;
    using System.Linq;


    /// <inheritdoc />
    /// <summary>
    /// Instantiate prefab in multiple gameObjects with same name
    /// </summary>
    public class InstantiateTool : EditorWindow
    {
        private GameObject prefab;

        private GameObject[] heads;

        private string nameToFind;

        [MenuItem("WittySol/Instantiate/Instantiate Tool")]
        public static void CreateNewCharacter()
        {
            GetWindow<InstantiateTool>();
        }

        void OnGUI()
        {
            Repaint();

            GUILayout.BeginVertical();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("Select the prefab folder from project to instantiate", MessageType.Info);
            prefab = EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), true,
                GUILayout.ExpandWidth(true)) as GameObject;

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Parent GameObject Name: ");
            nameToFind = EditorGUILayout.TextField(nameToFind);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Find GameObjects With Name \'" + nameToFind + "\'"))
            {
                if (prefab)
                {
                    var objects = FindObjectsOfType<GameObject>().Where(obj => obj.name.Equals(nameToFind));
                    heads = objects.ToArray();
                    Debug.Log("Total GameObject Found: " + heads.Length);
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (heads != null)
            {
                EditorGUILayout.LabelField("Total GameObjects Found: " + heads.Length);
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (prefab && heads != null)
            {
                if (GUILayout.Button("Instantiate \'" + prefab.name + "\'"))
                {
                    Debug.Log("Instantiate " + prefab.name);
                    for (int i = 0; i < heads.Length; i++)
                    {
                        Instantiate(prefab, heads[i].transform).name = prefab.name;
                    }
                }
            }

            GUILayout.EndVertical();
        }

    }
}
