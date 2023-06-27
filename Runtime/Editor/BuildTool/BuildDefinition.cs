// The MIT License (MIT)
// Copyright (c) 2017 David Evans @phosphoer
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#if UNITY_EDITOR

namespace Rito.ut23.BuildTool
{
    using UnityEditor;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Diagnostics;
    using System.IO;

    [CreateAssetMenu(fileName = "new-build-definition", menuName = "Build Definition")]
    public class BuildDefinition : ScriptableObject
    {
        public string LocationPathName = "Relative/Path/Build";
        public string ProductNameOverride;
        public string CompanyNameOverride;
        public BuildTarget BuildTarget;
        public BuildTargetGroup BuildTargetGroup;
        public string[] Defines;
        public SceneField[] Scenes;

        // 빌드 시작 시 기존 디렉토리 제거
        public bool Overwrite = true;

        // 빌드 완료 시 디렉토리 열기
        public bool OpenFolderOnBuildCompleted = true;

        [Space(10)]
        public bool WriteBuildInfo = false;
        public string BuildInfoFile = "BuildInfo.cs";
        public string BuildName = "Main";

        private const string kBuildVersionText = "public static class BuildInfo {{ public static string Name = \"{0}\"; public static string Date = \"{1}\"; }}";

        [ContextMenu("Build")]
        public void Build()
        {
            string buildDir = Path.Combine(Path.GetDirectoryName(Application.dataPath), LocationPathName);
            if (Overwrite)
            {
                try
                {
                    Directory.Delete(buildDir, true);
                }
                catch { }
            }

            BuildPipeline.BuildPlayer(ApplyBuildSettings());

            if (OpenFolderOnBuildCompleted)
            {
                try
                {

                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = buildDir,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                catch { }
            }
        }


        [ContextMenu("Copy Scenes From Build Settings")]
        public void CopySceneListFromBuild()
        {
            Scenes = new SceneField[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
            {
                var sceneBuildSetting = EditorBuildSettings.scenes[i];
                SceneField sceneField = AssetDatabase.LoadAssetAtPath(sceneBuildSetting.path, typeof(SceneField)) as object as SceneField;
                Scenes[i] = new SceneField();
                Scenes[i].sceneAsset = sceneField.sceneAsset;
            }
        }

        [ContextMenu("Apply Build Settings")]
        public BuildPlayerOptions ApplyBuildSettings()
        {
            if (WriteBuildInfo)
            {
                string pathToBuildAsset = System.IO.Path.GetDirectoryName(AssetDatabase.GetAssetPath(this));
                string versionFileText = string.Format(kBuildVersionText, BuildName, System.DateTime.Now.ToString());
                System.IO.File.WriteAllText(System.IO.Path.Combine(pathToBuildAsset, BuildInfoFile), versionFileText);
                AssetDatabase.Refresh();
            }

            string defineList = string.Join(";", Defines);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup, defineList);

            // Parse Path
            Regex regex = new Regex(@"\..+$");
            string pathName = regex.Replace(LocationPathName, "");
            switch (BuildTarget)
            {
                case BuildTarget.StandaloneWindows:
                    pathName += ".exe";
                    break;
            }

            // Set basic options
            BuildPlayerOptions buildOptions = new BuildPlayerOptions();
            buildOptions.targetGroup = BuildTargetGroup;
            buildOptions.target = BuildTarget;
            buildOptions.locationPathName = pathName;

            if (!string.IsNullOrEmpty(ProductNameOverride)) PlayerSettings.productName = ProductNameOverride;
            if (!string.IsNullOrEmpty(CompanyNameOverride)) PlayerSettings.companyName = CompanyNameOverride;

            // Build scene list
            List<string> sceneList = new List<string>();
            List<EditorBuildSettingsScene> editorScenes = new List<EditorBuildSettingsScene>();
            foreach (SceneField scene in Scenes)
            {
                var sceneAsset = AssetDatabase.GetAssetPath(scene.sceneAsset);
                sceneList.Add(sceneAsset);
                editorScenes.Add(new EditorBuildSettingsScene(sceneAsset, true));
            }

            buildOptions.scenes = sceneList.ToArray();
            EditorBuildSettings.scenes = editorScenes.ToArray();

            return buildOptions;
        }
    }

    // ---------------------------------------------------------------------------------------------------------------

    [CustomEditor(typeof(BuildDefinition))]
    [CanEditMultipleObjects]
    public class BuildDefinitionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            // This creates an error in the inspector after build about the GUI stack
            // being in an invalid state, presumably Unity doesn't appreciate being told to 
            // do a build mid-inspector rendering. Not sure how to fix but seems relatively harmless.
            if (GUILayout.Button("Build"))
            {
                foreach (Object obj in targets)
                {
                    BuildDefinition buildDef = obj as BuildDefinition;
                    if (buildDef != null)
                    {
                        buildDef.Build();
                    }
                }
            }

            if (GUILayout.Button("Apply Settings"))
            {
                foreach (Object obj in targets)
                {
                    BuildDefinition buildDef = obj as BuildDefinition;
                    if (buildDef != null)
                    {
                        buildDef.ApplyBuildSettings();
                    }
                }
            }
        }
    }
}

#endif