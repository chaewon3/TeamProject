using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coroTest : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(TestRoutine());
	}

	IEnumerator TestRoutine()
	{
		Debug.Log("Run TestRoutine");
		yield return StartCoroutine(OtherRoutine());
		Debug.Log("Finish TestRoutine");
	}

	IEnumerator OtherRoutine()
	{
		Debug.Log("Run OtherRoutine #1");
		yield return new WaitForSeconds(1.0f);
		Debug.Log("Run OtherRoutine #2");
		yield return new WaitForSeconds(1.0f);
		Debug.Log("Run OtherRoutine #3");
		yield return new WaitForSeconds(1.0f);
		Debug.Log("Finish OtherRoutine");
	}

}
