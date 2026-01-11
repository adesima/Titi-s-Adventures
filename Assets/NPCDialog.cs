using System;
using TMPro;
using Unity.VisualScripting;


//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCDialog : MonoBehaviour
{


    static GameObject button;
    static GameObject background;
    static TextMeshProUGUI textdialog;
    static TextMeshProUGUI textbuton;
    static bool isTalking = false;
    static int dialogIndex = 0;
    static string[] dialog;
    static int delay = 10;
    static float currenTime = 0;
    

    void Start()
    {
        button = GameObject.Find("Button");
        background = GameObject.Find("Background");
        textdialog = background.GetComponentInChildren<TextMeshProUGUI>();
        textbuton = button.GetComponentInChildren<TextMeshProUGUI>();

        button.SetActive(false);
        background.SetActive(false);
    }

    public static void StartDialog(int dialogNumber)
    {
        button.SetActive(true);
        background.SetActive(true);
        isTalking = true;
        dialog = Dialog.dialogList[dialogNumber];
        Time.timeScale = 0.02f;
        UpdateDialogDisplay();
    }

    public static void StopDialog()
    {
        button.SetActive(false);
        background.SetActive(false);
        isTalking = false;
        Time.timeScale = 1;
    }

    public static void UpdateDialogDisplay()
    {
        if(dialog.Length <= dialogIndex)
        {
            dialogIndex = 0;
            StopDialog();
            return;
        }
        textdialog.SetText(dialog[dialogIndex]);
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StopDialog();
        }

        if ((isTalking) && (Input.GetMouseButton(0)))
        {
            dialogIndex++;
            UpdateDialogDisplay();
        }
    }
}



