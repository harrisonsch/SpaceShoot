using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine.EventSystems; 
using TMPro;

public class ShopInstance : MonoBehaviour
{
[SerializeField] private CanvasGroup descriptionPanelCanvasGroup;
    [SerializeField] private Text descriptionText;
    private PowerUpManager powerUpManager;
    private List<PowerUp> availablePowerUps;
    public int shopItemsCount = 2;
    public List<PowerUp> currentShop = new List<PowerUp>(); 
    public GameObject powerUpPrefab; 
    public Transform shopPanelTransform;
    public TextMeshProUGUI balanceText;
    public int reroll = 3;
    public int rerollInc = 1;
    private Dictionary<PowerUp, GameObject> powerUpToGameObjectMap = new Dictionary<PowerUp, GameObject>();

    void Update()
    {
        balanceText.text = $"Balance: ${GameController.Instance.balance}";
    }
    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        availablePowerUps = powerUpManager.powerUpRegister;
        InitializeShop();
        PopulateShopUI();
    }
    void PopulateShopUI()
    {
        powerUpToGameObjectMap.Clear();
        
        foreach (PowerUp powerUp in currentShop)
        {
                
            GameObject itemUI = Instantiate(powerUpPrefab, shopPanelTransform);
            Image imageComponent = itemUI.transform.Find("icon").GetComponent<Image>();
            imageComponent.sprite = powerUp.icon; 
            itemUI.GetComponentInChildren<Text>().text = $"{powerUp.name}: {powerUp.cost}";

            // Add event listeners for hover
            CanvasGroup descriptionPanelCanvasGroup = itemUI.transform.Find("HoverPanel").GetComponent<CanvasGroup>();
    
            // Access the Text component for the description
            Text descriptionText = descriptionPanelCanvasGroup.transform.Find("Description").GetComponent<Text>();
            descriptionText.text = powerUp.description; // Set the initial description text

            // Initially hide the description panel
            descriptionPanelCanvasGroup.alpha = 0;
            descriptionPanelCanvasGroup.blocksRaycasts = false;

            powerUpToGameObjectMap[powerUp] = itemUI;
            // Setup event listeners for showing/hiding the description on hover
            EventTrigger trigger = itemUI.AddComponent<EventTrigger>();
            var pointerClick = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
            pointerClick.callback.AddListener((data) => Purchase(powerUp));
            trigger.triggers.Add(pointerClick);
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
        }
    }

   void ShowDescription(string description)
{
    descriptionText.text = description; 
    descriptionPanelCanvasGroup.alpha = 1f; 
    descriptionPanelCanvasGroup.blocksRaycasts = true; 
}

void HideDescription()
{
    descriptionPanelCanvasGroup.alpha = 0f; 
    descriptionPanelCanvasGroup.blocksRaycasts = false; 
}

    void InitializeShop()
    {
        Debug.Log("Powerups are" + availablePowerUps.Count);
        for (int i = currentShop.Count; i < shopItemsCount && availablePowerUps.Count > 0; i++)
        {
            int randIndex = Random.Range(0, availablePowerUps.Count);
            currentShop.Add(availablePowerUps[randIndex]);
            availablePowerUps.RemoveAt(randIndex); // Prevents the same power-up from being added twice
        }
    }

    void ClearShopUI()
    {
        foreach (Transform child in shopPanelTransform)
        {
            Destroy(child.gameObject);
        }
        currentShop.Clear();
    }

    public void RefreshShop() {
    if (PlayerHasEnoughCurrency(reroll)) {
        GameController.Instance.balance -= reroll;
        // Only add back power-ups that aren't already in the register
        foreach (var powerUp in currentShop) {
            if (!powerUpManager.powerUpRegister.Contains(powerUp)) {
                powerUpManager.powerUpRegister.Add(powerUp);
            }
        }

        ClearShopUI();
        availablePowerUps = new List<PowerUp>(powerUpManager.powerUpRegister); // Reset availablePowerUps
        InitializeShop();
        PopulateShopUI();
        reroll += rerollInc;
    }
    else {
        Debug.Log("Cannot afford reroll");
    }
}
    public void Purchase(PowerUp powerUp) {
        if (PlayerHasEnoughCurrency(powerUp.cost)) {
            ApplyPowerUp(powerUp);
            GameController.Instance.balance -= (int)powerUp.cost;
            
            if (powerUpToGameObjectMap.TryGetValue(powerUp, out GameObject itemUI)) {
                Destroy(itemUI); // Remove the UI element from the scene
                powerUpToGameObjectMap.Remove(powerUp); // Remove the mapping
            }

            currentShop.Remove(powerUp); // Remove from current shop listing
            powerUpManager.powerUpRegister.Remove(powerUp);
    } else {
        Debug.Log("Not enough currency to purchase this power-up.");
    }
}

    private bool PlayerHasEnoughCurrency(float cost)
    {
        if(GameController.Instance.balance >= cost) {
            return true; // Placeholder
        }
        return false;
    }

    private void ApplyPowerUp(PowerUp powerUp)
    {
       
        GameController.Instance.ActivatePowerUp(powerUp);
    }
}