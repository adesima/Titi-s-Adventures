using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCDialog : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private GameObject dialogButton;
    [SerializeField] private GameObject dialogBackground;
    [SerializeField] GameObject itemToDrop;

    private GameObject staticButton;
    private GameObject staticBackground;
    private TextMeshProUGUI textDialog;
    private bool isTalking = false;
    private bool doesDropItem = false;
    private bool itemedDropped = false;
    private int dialogIndex = 0;
    private string[] currentDialog;

    DamController dam;

    int CastorDialogIndex = 0;
    int VeveritaDialogIndex = 0;
    public enum DialogListNames
    {
        CastorApa,
        PersonajRau,
        Veverita
    }
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

    private void Start()
    {
        dam = GameObject.Find("DamSystem").GetComponent<DamController>();
    }

    public void StartDialog(DialogListNames names, int nrLemne)
    {

        if (staticButton == null || staticBackground == null) return;

        isTalking = true;
        staticButton.SetActive(true);
        staticBackground.SetActive(true);

        string sceneName = SceneManager.GetActiveScene().name;


        if (names == DialogListNames.CastorApa)
        {
            if ((CastorDialogIndex == 1) && (nrLemne < lemneNecesare))
            {

                currentDialog = new string[] { $"Nu ai destule lemne! Iti trebuie {lemneNecesare}" };
            }
            else if (dam.getReparat())
            {
                currentDialog = new string[] { "Multumesc ca ai reparat barajul!" };
            }
            else
            {

                if (sceneName.Contains("1"))
                {
                    currentDialog = CastorApa1[CastorDialogIndex];
                }
                else
                {
                    lemneNecesare = 10;
                    currentDialog = CastorApa2[CastorDialogIndex];
                }
                CastorDialogIndex++;
            }
        }
        else if (names == DialogListNames.Veverita)
        {
            if (VeveritaDialogIndex == 1)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0)
                {
                    currentDialog = new string[] { "Cred ca ar trebui sa mai fie sobolani prin padure..." };
                }
                else
                {
                    doesDropItem = true;
                    currentDialog = Veverita[VeveritaDialogIndex];
                    VeveritaDialogIndex++;
                }
            }
            else
            {
                currentDialog = Veverita[VeveritaDialogIndex];
                VeveritaDialogIndex++;
            }
        }

        Time.timeScale = 0.02f;
        UpdateDialogDisplay();
    }

    public void UpdateDialogDisplay()
    {
        if (currentDialog == null || dialogIndex >= currentDialog.Length)
        {
            StopDialog();
            return;
        }
        textDialog.SetText(currentDialog[dialogIndex]);
    }

    public void StopDialog()
    {
        isTalking = false;
        staticButton.SetActive(false);
        staticBackground.SetActive(false);
        Time.timeScale = 1f;
        dialogIndex = 0;

        if (doesDropItem && !itemedDropped)
        {
            GameObject item = Instantiate(itemToDrop);
            item.transform.position = new Vector3(-5, -33, 0);
            itemedDropped = true;
            doesDropItem = false;
        }
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

    static int lemneNecesare = 5;
    public static string[][] CastorApa1 = { 
        //pt castor apa
    //dupa interact from Titi to CastorApa
       new string[]{ $"Titi! Am nevoie de ajutorul tau! Am ramas fara lemne pentru baraj! Poti sa ma ajuti sa adun niste lemne din padure? Am nevoie de {lemneNecesare} " +
           $"bucati de lemne" },

         //dupa ce Titi aduna lemnele

        new string[]
        {"Multumesc Titi! Esti un prieten adevarat! Cu ajutorul tau, barajul va fi gata in curand.",
        "Ai adunat toate lemnele necesare! Acum pot sa continui constructia barajului. Iti sunt recunoscator!",
        }
    };
    public static string[][] CastorApa2 = { 
        //pt castor apa
    //dupa interact from Titi to CastorApa
       new string[]{ $"Titi! Am nevoie din nou de ajutorul tau! Barajul s-a stricat! Poti sa ma ajuti sa adun niste lemne din padure? Am nevoie de 10 " +
           $"bucati de lemne" },

         //dupa ce Titi aduna lemnele

        new string[]
        {"Multumesc Titi! Ai adunat toate lemnele necesare! Acum putem reface barajul. ",
        "Iti sunt recunoscator!",
        }
    };
    public static string[][] Veverita =
    {
        //dupa interact from Titi to PersonajBun
        new string[]{"Buna, Titi! Am auzit ca tu vei strange lemnele pentru baraj. Lemnele le vei gasi in padure,dar...",
        
        "Trebuie sa ai grija in padure, pentru ca sunt tot felul de sobolani!" , "Pentru a putea lua lemnele pentru baraj, va trebui sa omori toti sobolanii!",
        "Dupa ce ai terminat de omorat toti sobolanii, te rog sa vii ca sa iti ofer un cadou!" },
        //PersonajBun ofera un cadou lui Titi
        new string[]{
        "Buna, Titi! Te-ai intors!", "Ai reusit sa omori toti sobolanii?", "Woow! Eroul nostru! Acum vom avea, in sfarsit, linisite in padure!",
        "Pentru curajul si spiritul tau aventuros, vreau sa iti ofer acest cadou special. Sper sa iti aduca noroc in calatoriile tale viitoare!" }
        //the end
    };
}


