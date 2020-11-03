using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestRoomManager : MonoBehaviour
{
    public Quests quests;
    public TMP_Text cofixText;
    public TMP_Text KFCText;
    public TMP_Text homeText;

    public Button cofixButton;
    public Button KFCButton;
    public Button homeButton;

    
    private void Start()
    {
        if (quests.otherRestCooldown > 1)
        {
            cofixText.text = "Перейти";
            KFCText.text = "Перейти";
        }
        else
        {
            cofixText.text = $"Осталось выполнить заданий: {2 - quests.otherRestCooldown}";
            KFCText.text = cofixText.text;

            cofixButton.image.color = cofixButton.colors.disabledColor;
            KFCButton.image.color = KFCButton.colors.disabledColor;
            cofixButton.enabled = false;
            KFCButton.enabled = false;
        }

        if (quests.homeRestCooldown > 2)
        {
            homeText.text = "Перейти";
        }
        else
        {
            homeText.text = $"Осталось выполнить заданий: {3 - quests.homeRestCooldown}";
          
            homeButton.image.color = homeButton.colors.disabledColor;
            homeButton.enabled = false;
        }
    }
}
