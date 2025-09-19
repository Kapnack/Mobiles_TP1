using UnityEngine;
using System.Collections;

public class MousePos : MonoBehaviour 
{

	public enum AxisRelation{Horizontal, Vertical}

	public static float Relation(AxisRelation axisR)
	{
		float res;
		switch(axisR)
		{
		case AxisRelation.Horizontal:
			res = Input.mousePosition.x / Screen.width *2 -1;
				return res;


		case AxisRelation.Vertical:
			res = Input.mousePosition.y / Screen.height *2 -1;
			return res;
		}
		return -1;
	}
}
