using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject plantPrefab;
    public GameObject icon;
    public GameObject plantGhostPrefab;  // Призрак, задается через инспектор
    public int plantPrice;
    public float buyCd;

    [Space]
    public AudioClip plantSetSound;
    public Image cdImage;

    private GameObject gridObj;
    private GameObject plantGhostInstance;  // Экземпляр призрака
    private bool isDragging = false;
    private GameObject cellVariable;
    private SunManager sunManager;
    private AudioSource source;
    private float startBuyCd;

    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (plantPrefab != null && sunManager.count >= plantPrice && buyCd <= 0)
        {
            gridObj.SetActive(true);
            plantGhostInstance = Instantiate(plantGhostPrefab);
        }
        isDragging = true;
    }

    // когда игрок отпускает нажатие
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
        Time.timeScale = 1f;
        sunManager = FindAnyObjectByType<SunManager>();
        gridObj = GameObject.FindGameObjectWithTag("Grid");
    }

    private void Start()
    {
        cdImage.gameObject.SetActive(false);
        gridObj.SetActive(false);
        startBuyCd = buyCd;
        buyCd = 0;
        source = GetComponentInParent<AudioSource>();
    }

    void Update()
    {
        if (buyCd > 0)
        {
            cdImage.gameObject.SetActive(true);
            buyCd -= Time.deltaTime;

            cdImage.fillAmount = buyCd / startBuyCd;
        }
        else
        {
            cdImage.fillAmount = 1;
            cdImage.gameObject.SetActive(false);
        }


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

        // Получаем компонент Image и устанавливаем цвет с новым альфа-каналом
        UnityEngine.UI.Image gameObjImage = gameObject.GetComponent<UnityEngine.UI.Image>();  // Явно указываем пространство имен
        Color gameObjColor = gameObjImage.color;
        gameObjColor.a = alpha;
        gameObjImage.color = gameObjColor;

        UnityEngine.UI.Image iconImage = icon.GetComponent<UnityEngine.UI.Image>();
        Color iconObjColor = iconImage.color;
        iconObjColor.a = alpha;
        iconImage.color = iconObjColor;
    }

    // Преобразуем позицию указателя в мировые координаты
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

        // Пробегаемся по всем клеткам и находим ближайшую
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

        if (nearestCell != null && cellVariable.GetComponent<Cell>().canPlace && buyCd <= 0)
        {
            source.PlayOneShot(plantSetSound);
            sunManager.SunMinus(plantPrice);
            GameObject plant = Instantiate(plantPrefab, nearestCell.transform.position, Quaternion.identity);

            // Связываем растение с клеткой
            plant.GetComponent<Plant>().SetCell(cellVariable);

            cellVariable.GetComponent<SpriteRenderer>().enabled = false;
            cellVariable.GetComponent<Cell>().canPlace = false;

            buyCd = startBuyCd;
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
