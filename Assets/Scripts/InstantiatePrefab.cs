using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
	public GameObject prefab;
	public Transform point;
	public float livingTime;

    public void Instantiate()
	{
		// Instancia un objeto con el prefab seleccionado en el punto donde queremos
		GameObject instantiatedObject = Instantiate(prefab, point.position, Quaternion.identity) as GameObject;

		if (livingTime > 0f) {
			// Destruye el objeto despues del tiempo seteado
			Destroy(instantiatedObject, livingTime);
		}
	}
}
