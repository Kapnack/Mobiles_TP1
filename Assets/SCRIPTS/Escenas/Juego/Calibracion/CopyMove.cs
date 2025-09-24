using UnityEngine;
using System.Collections;

public class CopyMove : MonoBehaviour
{
	public Transform Target;

	private void LateUpdate () 
	{
		transform.position = Target.position;
	}
}
