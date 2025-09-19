using UnityEngine;
using System.Collections;

public class CopyMove : MonoBehaviour
{
	public Transform Target;

	private void LateUpdate () 
	{
		transform.position = Target.position;// + Target.transform.right * Diferencia;
		//transform.localRotation = Target.localRotation;
	}
}
