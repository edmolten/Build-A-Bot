using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AddParts : MonoBehaviour {

  private bool partSelected = false;
  private GameObject part = null;
  private string currentType = null;
  private Dictionary<string, PrimitiveType> typeObjects; 

	// Use this for initialization
	void Start () {
    typeObjects = new Dictionary<string, PrimitiveType>();
    typeObjects["cube"] = PrimitiveType.Cube;
	  typeObjects["cylinder"] = PrimitiveType.Cylinder;
	}

  public void selectPart (string type) {
    if (partSelected) {
      return;
    }

    if (currentType != null) {
      if (type == currentType) {
        return;
      }
    }

    part = GameObject.CreatePrimitive(typeObjects[type]);
    partSelected = true;
    currentType = type;
  }

  void unSelectPart () {
    if (!partSelected) {
      return;
    }

    Destroy(part);
    partSelected = false;
    currentType = null;
  }
	
	// Update is called once per frame
	void Update () {
	   if (Input.GetMouseButtonUp(1)) {
        unSelectPart(); 
     }

     if (partSelected) {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f; 
        part.transform.position = Camera.main.ScreenToWorldPoint(temp); 
     }
	}
}
