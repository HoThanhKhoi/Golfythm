using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Preference")]
    [SerializeField] private Player player;

    [Header("Player Health")]
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
    [SerializeField] private GameObject heart;
    [SerializeField] private Transform heartContainer;
    [SerializeField] private Transform heartSpawnPos;
    [SerializeField] private float heartDistance = .5f;

    private List<Image> heartImageList;

    private void Start()
    {
        player.OnHealthChanged += UpdateHealth;
        heartImageList = new List<Image>();
        SetUpHealth();
    }

    public void SetUpHealth()
    {
        for (int i = 0; i < player.MaxHealth; i++)
        {
            GameObject heartGameObject = Instantiate(heart, heartSpawnPos.position, Quaternion.identity, heartContainer);
            heartGameObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(heartDistance * i, 0);

            Image heartImage = heartGameObject.GetComponent<Image>();
            heartImage.sprite = fullHeartSprite;

            heartImageList.Add(heartImage);
        }
    }

    public void UpdateHealth(int currentHealth)
    {
        if (currentHealth < 0) { return; }
        for(int i = 0; i < currentHealth; i++)
        {
            heartImageList[i].sprite = fullHeartSprite;
        }
        for(int i = currentHealth; i < heartImageList.Count; i++)
        {
            heartImageList[i].sprite = emptyHeartSprite;
        }
    }
}
