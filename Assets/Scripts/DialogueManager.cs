using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogPanel;
    public GameObject npcName;
    public GameObject dialogText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    public void HideDialoguePanel()
    {
        dialogPanel.SetActive(false);
    }
}
