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
    public GameObject powerUpPrefab; // Prefab for displaying power-ups
    public Transform activePowerUpsPanelTransform; // The parent transform where active power-ups are displayed
    private Dictionary<PowerUp, GameObject> powerUpToGameObjectMap = new Dictionary<PowerUp, GameObject>();

    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        PopulateActivePowerUpsUI();
    }

    void OnEnable()
{
    powerUpManager = FindObjectOfType<PowerUpManager>();
    ClearActivePowerUpsUI(); // Ensure the UI is cleared before repopulating
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
        descriptionText.text = powerUp.description; // Set the initial description text

        // Make sure the description is initially hidden
        itemDescriptionPanelCanvasGroup.alpha = 0;
        itemDescriptionPanelCanvasGroup.blocksRaycasts = false;

        powerUpToGameObjectMap[powerUp] = itemUI;
        // Set up the event triggers for this specific item
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
            // You might also want to hide the description here
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
        // Implement the logic to sell the power-up, which might involve adding currency to the player's balance,
        // removing the power-up from activePowerUps, and updating the UI.
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