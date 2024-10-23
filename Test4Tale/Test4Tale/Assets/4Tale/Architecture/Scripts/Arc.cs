using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private float spacing;
    [SerializeField] private float arrowAngle;
    [SerializeField] private int dotToSkip;
    private List<GameObject> _dotPool = new();
    private GameObject _currentArrow;
    private Vector3 _arrowDirection;
    
    
    private void Start()
    {
        arrowPrefab = Instantiate(arrowPrefab, transform);
        arrowPrefab.transform.localPosition = Vector3.zero;
        InitializeDotPool(poolSize);
    }

    private void UpdateArc(Vector3 start, Vector3 mid, Vector3 end)
    {
        int numDots = Mathf.CeilToInt(Vector3.Distance(start, end) / spacing);

        for (int i = 0; i < numDots && i < _dotPool.Count; i++)
        {
            float t = i / (float)numDots;
            t = Mathf.Clamp(t, 0f, 1f);

            Vector3 pos = QuadraticBezierPoint(start, mid, end, t);

            if (i != numDots - dotToSkip)
            {
                _dotPool[i].transform.position = pos;
                _dotPool[i].SetActive(true);
            }

            if (i == numDots - (dotToSkip + 1) && i - dotToSkip + 1 >= 0)
            {
                _arrowDirection = _dotPool[i].transform.position;
            }
        }

        for (int i = numDots - dotToSkip; i < _dotPool.Count; i++)
        {
            if (i > 0)
            {
                _dotPool[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        Vector3 startPosition = transform.position;
        Vector3 midPoint = CalculateMidPoint(startPosition, mousePosition);
        UpdateArc(startPosition, midPoint, mousePosition);
        PositionAndRotateArrow(mousePosition);
    }

    private void PositionAndRotateArrow(Vector3 position)
    {
        arrowPrefab.transform.position = position;
        Vector3 arrowDirection = _arrowDirection + position;
        float angle = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;
        angle -= arrowAngle;
        arrowPrefab.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private Vector3 CalculateMidPoint(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 midPoint = (startPosition + endPosition) / 2;
        float arcHeight = Vector3.Distance(startPosition, endPosition) / 3f;
        midPoint.y += arcHeight;
        return midPoint;
    }

    private Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * start;
        point += 2 * u * t * control;
        point += tt * end;
        return point;
    }
    
    private void InitializeDotPool(int dotCount)
    {
        for (int i = 0; i < dotCount; i++)
        {
            GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, transform);
            dot.SetActive(false);
            _dotPool.Add(dot);
        }
    }
}
