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
    public int shopItemsCount = 4;
    public List<PowerUp> currentShop = new List<PowerUp>(); 
    public GameObject powerUpPrefab; 
    public Transform shopPanelTransform;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI rerollText;
    public int reroll = 3;
    public int rerollInc = 1;
    private Dictionary<PowerUp, GameObject> powerUpToGameObjectMap = new Dictionary<PowerUp, GameObject>();
    public AudioSource buySFX;
    public GameObject shopWelcome;
    [SerializeField] public static bool firstTimeInShop = true;
    public TextMeshProUGUI healthText;

    void Update()
    {
        balanceText.text = $"Balance: ${GameController.Instance.balance}";
        rerollText.text = $"Cost: ${reroll}";
        healthText.text = $"Health: {GameController.Instance.health}";
    }
    void Start()
    {
        powerUpManager = FindObjectOfType<PowerUpManager>();
        availablePowerUps = powerUpManager.powerUpRegister;
        InitializeShop();
        PopulateShopUI();
        Debug.Log("first time in shop is " + firstTimeInShop);
        if (firstTimeInShop)
        {
            ShowWelcomeDialog();
            firstTimeInShop = false; // Set the flag to false after showing the dialog
        }
    }

    void ShowWelcomeDialog() {
        shopWelcome.SetActive(true);

    }
    void PopulateShopUI()
    {
        powerUpToGameObjectMap.Clear();
        
        foreach (PowerUp powerUp in currentShop)
        {
                
            GameObject itemUI = Instantiate(powerUpPrefab, shopPanelTransform);
            Image imageComponent = itemUI.transform.Find("icon").GetComponent<Image>();
            imageComponent.sprite = powerUp.icon; 
            itemUI.GetComponentInChildren<Text>().text = $"{powerUp.powerUpName}: ${powerUp.cost}";

            
            CanvasGroup descriptionPanelCanvasGroup = itemUI.transform.Find("HoverPanel").GetComponent<CanvasGroup>();
    
            
            Text descriptionText = descriptionPanelCanvasGroup.transform.Find("Description").GetComponent<Text>();
            descriptionText.text = powerUp.description; // Set the initial description text

            
            descriptionPanelCanvasGroup.alpha = 0;
            descriptionPanelCanvasGroup.blocksRaycasts = false;

            powerUpToGameObjectMap[powerUp] = itemUI;

            //click and hover triggers
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
            availablePowerUps.RemoveAt(randIndex); 
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

    public void ExitShop() {
        foreach (var powerUp in currentShop) {
            if (!powerUpManager.powerUpRegister.Contains(powerUp)) {
                powerUpManager.powerUpRegister.Add(powerUp);
            }
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
                buySFX.Play();
            ApplyPowerUp(powerUp);
            GameController.Instance.balance -= (int)powerUp.cost;
            
            if (powerUpToGameObjectMap.TryGetValue(powerUp, out GameObject itemUI)) {
                Destroy(itemUI); 
                powerUpToGameObjectMap.Remove(powerUp); 
            }

            currentShop.Remove(powerUp); 
            powerUpManager.powerUpRegister.Remove(powerUp);
    } else {
        Debug.Log("Not enough currency to purchase this power-up.");
    }
}

    private bool PlayerHasEnoughCurrency(float cost)
    {
        if(GameController.Instance.balance >= cost) {
            return true; 
        }
        return false;
    }

    private void ApplyPowerUp(PowerUp powerUp)
    {
       
        GameController.Instance.ActivatePowerUp(powerUp);
    }
}