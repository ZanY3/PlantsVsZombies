using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject plantPrefab;
    public GameObject icon;
    public GameObject plantGhostPrefab;  // �������, �������� ����� ���������
    public int plantPrice;

    private GameObject gridObj;
    private GameObject plantGhostInstance;  // ��������� ��������
    private bool isDragging = false;
    private GameObject cellVariable;
    private SunManager sunManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (plantPrefab != null && sunManager.count >= plantPrice)
        {
            gridObj.SetActive(true);
            plantGhostInstance = Instantiate(plantGhostPrefab);
        }
        isDragging = true;
    }

    // ����� ����� ��������� �������
    public void OnPointerUp(PointerEventData eventData)
    {
        if (plantGhostInstance != null)
        {
            PlacePlantInNearestCell();
            Destroy(plantGhostInstance);
        }
        isDragging = false;
        gridObj.SetActive(false);
    }

    private void Awake()
    {
        sunManager = FindAnyObjectByType<SunManager>();
        gridObj = GameObject.FindGameObjectWithTag("Grid");
    }

    private void Start()
    {
        gridObj.SetActive(false);
    }

    void Update()
    {
        if (sunManager.count < plantPrice)
            SetAlpha(0.5f);
        else
            SetAlpha(1f);

        if (isDragging && plantGhostInstance != null)
        {
            plantGhostInstance.transform.position = GetPointerWorldPosition();
        }
    }

    public void SetAlpha(float alpha)
    {
        alpha = Mathf.Clamp01(alpha);

        // �������� ��������� Image � ������������� ���� � ����� �����-�������
        UnityEngine.UI.Image gameObjImage = gameObject.GetComponent<UnityEngine.UI.Image>();  // ���� ��������� ������������ ����
        Color gameObjColor = gameObjImage.color;
        gameObjColor.a = alpha;
        gameObjImage.color = gameObjColor;

        UnityEngine.UI.Image iconImage = icon.GetComponent<UnityEngine.UI.Image>();
        Color iconObjColor = iconImage.color;
        iconObjColor.a = alpha;
        iconImage.color = iconObjColor;
    }

    // ����������� ������� ��������� � ������� ����������
    private Vector3 GetPointerWorldPosition()
    {
        Vector3 pointerPosition = Input.mousePosition;
        pointerPosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(pointerPosition);
    }

    private void PlacePlantInNearestCell()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");

        GameObject nearestCell = null;
        float minDistance = Mathf.Infinity;

        // ����������� �� ���� ������� � ������� ���������
        foreach (GameObject cell in cells)
        {
            float distance = Vector3.Distance(cell.transform.position, plantGhostInstance.transform.position);
            if (distance < minDistance)
            {
                cellVariable = cell;
                minDistance = distance;
                nearestCell = cell;
            }
        }

        if (nearestCell != null && cellVariable.GetComponent<Cell>().canPlace)
        {
            sunManager.SunMinus(plantPrice);
            Instantiate(plantPrefab, nearestCell.transform.position, Quaternion.identity);

            cellVariable.GetComponent<SpriteRenderer>().enabled = false;
            cellVariable.GetComponent<Cell>().canPlace = false;
        }
        else
        {
            ReturnPlant();
        }
    }

    public void ReturnPlant()
    {
        Destroy(plantGhostInstance);
        isDragging = false;
    }
    public void ExemptCell()
    {
        cellVariable.GetComponent<SpriteRenderer>().enabled = true;
        cellVariable.GetComponent<Cell>().canPlace = true;
        cellVariable = null;
    }
}
