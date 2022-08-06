using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public TextMeshProUGUI TextUI;

    private int itemsCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            Destroy(other.gameObject);
            itemsCount++;

            if (TextUI != null)
            {
                TextUI.text = $"Coins: {itemsCount}";
            }
        }

        if (other.gameObject.tag == "book")
        {
            Destroy(other.gameObject);

            GameManager.Instance.UpdateGameState(GameState.ChooseAblility);
        }
    }
}
