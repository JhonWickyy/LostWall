using UnityEngine;
using UnityEngine.UI;

public class UIImage : Image
{
   public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
   {
      return GetComponent<PolygonCollider2D>().OverlapPoint(screenPoint);
   }
}
