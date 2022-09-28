using UnityEngine;

public static class UIUtils
{
	public static Vector2 GetAnchoredPostionFromWorldSpacePoint(Vector3 point, Canvas canvas)
	{
		RectTransform canvasRect = canvas.GetComponent<RectTransform>();
		Vector2 adjustedPosition = Camera.main.WorldToScreenPoint(point);

		adjustedPosition.x *= canvasRect.rect.width / (float)Camera.main.pixelWidth;
		adjustedPosition.y *= canvasRect.rect.height / (float)Camera.main.pixelHeight;
		return adjustedPosition - canvasRect.sizeDelta / 2f;
	}
}
