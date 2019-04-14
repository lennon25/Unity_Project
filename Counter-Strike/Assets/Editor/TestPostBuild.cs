using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;


public static class TestPostBuild{

	[PostProcessBuild]
	public static void OnPosstprocessBuild(BuildTarget target, string pathToBuildProject){
		Debug.Log("Build location: " + pathToBuildProject);
	}
}