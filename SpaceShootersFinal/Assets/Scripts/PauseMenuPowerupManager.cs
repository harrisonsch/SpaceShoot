using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for the EventTrigger
using System.Collections.Generic;
using TMPro;

public class PauseMenuPowerUpManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup descriptionPanelCanvasGroup;
    [SerializeField] private Text descriptionText;
    private PowerUpManager powerUpManager;
    public GameObject powerUpPrefab; 
    public Transform activePowerUpsPanelTransform; 
    private Dictionary<PowerUp, GameObject> powerUpToGameObjectMap = new Dictionary<PowerUp, GameObject>();

    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        ClearActivePowerUpsUI();
        PopulateActivePowerUpsUI();
    }

    void OnEnable()
{
    powerUpManager = FindObjectOfType<PowerUpManager>();
    ClearActivePowerUpsUI(); //clear UI (might not clear on menu close)
    PopulateActivePowerUpsUI();
}

void ClearActivePowerUpsUI()
{
    foreach (Transform child in activePowerUpsPanelTransform)
    {
        Destroy(child.gameObject);
    }
    powerUpToGameObjectMap.Clear();
}

    void PopulateActivePowerUpsUI()
{
    foreach (PowerUp powerUp in GameController.Instance.activePowerUps)
    {
        GameObject itemUI = Instantiate(powerUpPrefab, activePowerUpsPanelTransform);
        Image imageComponent = itemUI.transform.Find("icon").GetComponent<Image>();
        imageComponent.sprite = powerUp.icon;
        itemUI.GetComponentInChildren<Text>().text = powerUp.powerUpName;

        // Assign the CanvasGroup for each instantiated prefab
        CanvasGroup itemDescriptionPanelCanvasGroup = itemUI.transform.Find("HoverPanel").GetComponent<CanvasGroup>();
        Text descriptionText = itemDescriptionPanelCanvasGroup.transform.Find("Description").GetComponent<Text>();
        descriptionText.text = powerUp.description; 

        itemDescriptionPanelCanvasGroup.alpha = 0;
        itemDescriptionPanelCanvasGroup.blocksRaycasts = false;

        powerUpToGameObjectMap[powerUp] = itemUI;
        SetupEventTriggers(itemUI, powerUp, itemDescriptionPanelCanvasGroup);
    }
}

void SetupEventTriggers(GameObject itemUI, PowerUp powerUp, CanvasGroup descriptionPanelCanvasGroup)
{
    EventTrigger trigger = itemUI.GetComponent<EventTrigger>() ?? itemUI.AddComponent<EventTrigger>();

    var pointerEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
    pointerEnter.callback.AddListener((data) => {
        descriptionPanelCanvasGroup.alpha = 1;
        descriptionPanelCanvasGroup.blocksRaycasts = true;
    });
    trigger.triggers.Add(pointerEnter);

    var pointerExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
    pointerExit.callback.AddListener((data) => {
        descriptionPanelCanvasGroup.alpha = 0;
        descriptionPanelCanvasGroup.blocksRaycasts = false;
    });
    trigger.triggers.Add(pointerExit);

    var pointerClick = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
    pointerClick.callback.AddListener((data) => {
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            SellPowerUp(powerUp);
            descriptionPanelCanvasGroup.alpha = 0;
            descriptionPanelCanvasGroup.blocksRaycasts = false;
        }
    });
    trigger.triggers.Add(pointerClick);
}



    void ShowDescription(string description)
    {
        Debug.Log($"Showing description: {description}");
        descriptionText.text = description;
        descriptionPanelCanvasGroup.alpha = 1f;
        descriptionPanelCanvasGroup.blocksRaycasts = true;
    }

    void HideDescription()
    {
        descriptionPanelCanvasGroup.alpha = 0f;
        descriptionPanelCanvasGroup.blocksRaycasts = false;
    }

    void SellPowerUp(PowerUp powerUp)
    {
        GameController.Instance.balance += (int)powerUp.cost; 
        Debug.Log("adding " + (int) powerUp.cost + " for " + powerUp.powerUpName);
        Debug.Log("balance is " + GameController.Instance.balance);
        GameController.Instance.DeactivatePowerUp(powerUp); 

        if (powerUpToGameObjectMap.TryGetValue(powerUp, out GameObject itemUI))
        {
            Destroy(itemUI);
            powerUpToGameObjectMap.Remove(powerUp);
        }
    }
}