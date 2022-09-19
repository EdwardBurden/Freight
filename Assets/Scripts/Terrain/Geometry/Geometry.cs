using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Terrain
{
	public static class Geometry
	{
		//Where is p in relation to a-b
		// < 0 -> to the right
		// = 0 -> on the line
		// > 0 -> to the left
		public static float IsAPointLeftOfVectorOrOnTheLine(Vector2 a, Vector2 b, Vector2 p)
		{
			float determinant = (a.x - p.x) * (b.y - p.y) - (a.y - p.y) * (b.x - p.x);

			return determinant;
		}

		//Is a triangle in 2d space oriented clockwise or counter-clockwise
		//https://math.stackexchange.com/questions/1324179/how-to-tell-if-3-connected-points-are-connected-clockwise-or-counter-clockwise
		//https://en.wikipedia.org/wiki/Curve_orientation
		public static bool IsTriangleOrientedClockwise(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			bool isClockWise = true;

			float determinant = p1.x * p2.y + p3.x * p1.y + p2.x * p3.y - p1.x * p3.y - p3.x * p2.y - p2.x * p1.y;

			if (determinant > 0f)
			{
				isClockWise = false;
			}

			return isClockWise;
		}

		//Is a point d inside, outside or on the same circle as a, b, c
		//https://gamedev.stackexchange.com/questions/71328/how-can-i-add-and-subtract-convex-polygons
		//Returns positive if inside, negative if outside, and 0 if on the circle
		public static float IsPointInsideOutsideOrOnCircle(Vector2 aVec, Vector2 bVec, Vector2 cVec, Vector2 dVec)
		{
			//This first part will simplify how we calculate the determinant
			float a = aVec.x - dVec.x;
			float d = bVec.x - dVec.x;
			float g = cVec.x - dVec.x;

			float b = aVec.y - dVec.y;
			float e = bVec.y - dVec.y;
			float h = cVec.y - dVec.y;

			float c = a * a + b * b;
			float f = d * d + e * e;
			float i = g * g + h * h;

			float determinant = (a * e * i) + (b * f * g) + (c * d * h) - (g * e * c) - (h * f * a) - (i * d * b);

			return determinant;
		}

		//Is a quadrilateral convex? Assume no 3 points are colinear and the shape doesnt look like an hourglass
		public static bool IsQuadrilateralConvex(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
		{
			bool isConvex = false;

			bool abc = Geometry.IsTriangleOrientedClockwise(a, b, c);
			bool abd = Geometry.IsTriangleOrientedClockwise(a, b, d);
			bool bcd = Geometry.IsTriangleOrientedClockwise(b, c, d);
			bool cad = Geometry.IsTriangleOrientedClockwise(c, a, d);

			if (abc && abd && bcd & !cad)
			{
				isConvex = true;
			}
			else if (abc && abd && !bcd & cad)
			{
				isConvex = true;
			}
			else if (abc && !abd && bcd & cad)
			{
				isConvex = true;
			}
			//The opposite sign, which makes everything inverted
			else if (!abc && !abd && !bcd & cad)
			{
				isConvex = true;
			}
			else if (!abc && !abd && bcd & !cad)
			{
				isConvex = true;
			}
			else if (!abc && abd && !bcd & !cad)
			{
				isConvex = true;
			}


			return isConvex;
		}

		//http://thirdpartyninjas.com/blog/2008/10/07/line-segment-intersection/
		public static bool AreLinesIntersecting(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2, bool shouldIncludeEndPoints)
		{
			bool isIntersecting = false;

			float denominator = (l2_p2.y - l2_p1.y) * (l1_p2.x - l1_p1.x) - (l2_p2.x - l2_p1.x) * (l1_p2.y - l1_p1.y);

			//Make sure the denominator is > 0, if not the lines are parallel
			if (denominator != 0f)
			{
				float u_a = ((l2_p2.x - l2_p1.x) * (l1_p1.y - l2_p1.y) - (l2_p2.y - l2_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;
				float u_b = ((l1_p2.x - l1_p1.x) * (l1_p1.y - l2_p1.y) - (l1_p2.y - l1_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;

				//Are the line segments intersecting if the end points are the same
				if (shouldIncludeEndPoints)
				{
					//Is intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
					if (u_a >= 0f && u_a <= 1f && u_b >= 0f && u_b <= 1f)
					{
						isIntersecting = true;
					}
				}
				else
				{
					//Is intersecting if u_a and u_b are between 0 and 1
					if (u_a > 0f && u_a < 1f && u_b > 0f && u_b < 1f)
					{
						isIntersecting = true;
					}
				}

			}

			return isIntersecting;
		}

		//Calculate the center of circle in 2d space given three coordinates
		//http://paulbourke.net/geometry/circlesphere/
		public static Vector2 CalculateCircleCenter(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			Vector2 center = new Vector2();

			float ma = (p2.y - p1.y) / (p2.x - p1.x);
			float mb = (p3.y - p2.y) / (p3.x - p2.x);

			center.x = (ma * mb * (p1.y - p3.y) + mb * (p1.x + p2.x) - ma * (p2.x + p3.x)) / (2 * (mb - ma));

			center.y = (-1 / ma) * (center.x - (p1.x + p2.x) / 2) + (p1.y + p2.y) / 2;

			return center;
		}

		//Clamp list indices
		//Will even work if index is larger/smaller than listSize, so can loop multiple times
		public static int ClampListIndex(int index, int listSize)
		{
			index = ((index % listSize) + listSize) % listSize;

			return index;
		}

		//Is a point to the left, to the right, or on a plane
		//https://gamedevelopment.tutsplus.com/tutorials/understanding-sutherland-hodgman-clipping-for-physics-engines--gamedev-11917
		//Notice that the plane normal doesnt have to be normalized
		public static float DistanceFromPointToPlane(Vector3 planeNormal, Vector3 planePos, Vector3 pointPos)
		{
			//Positive distance denotes that the point p is on the front side of the plane 
			//Negative means it's on the back side
			float distance = Vector3.Dot(planeNormal, pointPos - planePos);

			return distance;
		}

		//Get the coordinate if we know a ray-plane is intersecting
		public static Vector3 GetRayPlaneIntersectionCoordinate(Vector3 planePos, Vector3 planeNormal, Vector3 rayStart, Vector3 rayDir)
		{
			float denominator = Vector3.Dot(-planeNormal, rayDir);

			Vector3 vecBetween = planePos - rayStart;

			float t = Vector3.Dot(vecBetween, -planeNormal) / denominator;

			Vector3 intersectionPoint = rayStart + rayDir * t;

			return intersectionPoint;
		}
	}
}