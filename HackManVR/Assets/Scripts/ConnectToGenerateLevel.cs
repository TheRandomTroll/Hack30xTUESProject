using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToGenerateLevel : MonoBehaviour {


    public TestRMS testrms;
    public GenerateLevel generator;

	void Start () {
        StartCoroutine(GenerateLevel());
	}
	
	IEnumerator GenerateLevel()
    {
        yield return new WaitForEndOfFrame();
        testrms = FindObjectOfType<TestRMS>();
        generator = FindObjectOfType<GenerateLevel>();
        // generator.levelDict = testrms.randomNew.randGrid; TODO: Fix
        generator.LoadLevelInfo();
    }
}
