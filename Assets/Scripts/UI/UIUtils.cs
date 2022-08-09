using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public static class UIUtils
{
	public static Vector2 GetAnchoredPostionFromWorldSpacePoint(Vector3 point, Canvas canvas)
	{
		RectTransform canvasRect = canvas.GetComponent<RectTransform>();
		Vector2 adjustedPosition = GameUIManager.instance.worldCamera.WorldToScreenPoint(point);

		adjustedPosition.x *= canvasRect.rect.width / (float)GameUIManager.instance.worldCamera.pixelWidth;
		adjustedPosition.y *= canvasRect.rect.height / (float)GameUIManager.instance.worldCamera.pixelHeight;
		Debug.Log(adjustedPosition);
		// set it
		Debug.Log(adjustedPosition - canvasRect.sizeDelta / 2f);
		return adjustedPosition - canvasRect.sizeDelta / 2f;
	}
}
