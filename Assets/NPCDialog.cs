using TMPro;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    
    [Header("UI References")]
    [SerializeField] private GameObject dialogButton;
    [SerializeField] private GameObject dialogBackground;

    private static GameObject staticButton;
    private static GameObject staticBackground;
    private static TextMeshProUGUI textDialog;
    private static bool isTalking = false;
    private static int dialogIndex = 0;
    private static string[] currentDialog;

    void Awake()
    {
        
        staticButton = dialogButton;
        staticBackground = dialogBackground;

        if (staticBackground != null)
        {
            textDialog = staticBackground.GetComponentInChildren<TextMeshProUGUI>();
            staticBackground.SetActive(false);
        }

        if (staticButton != null)
        {
            staticButton.SetActive(false);
        }
    }

    public static void StartDialog(int dialogNumber)
    {
        if (staticButton == null || staticBackground == null) return;

        isTalking = true;
        staticButton.SetActive(true);
        staticBackground.SetActive(true);

        
        currentDialog = Dialog.dialogList[dialogNumber];
        dialogIndex = 0;

        Time.timeScale = 0f; 
        UpdateDialogDisplay();
    }

    public static void UpdateDialogDisplay()
    {
        if (currentDialog == null || dialogIndex >= currentDialog.Length)
        {
            StopDialog();
            return;
        }
        textDialog.SetText(currentDialog[dialogIndex]);
    }

    public static void StopDialog()
    {
        isTalking = false;
        staticButton.SetActive(false);
        staticBackground.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) StopDialog();

            
            if (Input.GetMouseButtonDown(0))
            {
                dialogIndex++;
                UpdateDialogDisplay();
            }
        }
    }
}