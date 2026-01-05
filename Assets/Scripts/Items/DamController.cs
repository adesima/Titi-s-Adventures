using UnityEngine;
using UnityEngine.UI; // Necesar pentru UI (Text, Mesaje)

public class DamController : MonoBehaviour
{
    [Header("Componente Vizuale")]
    public GameObject visualsStricat;
    public GameObject visualsReparat;
    
    [Header("Setari Resurse")]
    public int lemneNecesare = 5;
    public GameObject mesajLipsaLemne; // Un text UI sau Panel care zice "Nu ai lemne!"

    [Header("Legaturi Externe")]
    // Aici vom lega scriptul lacului ca sa pornim apa
    public WaterManagement scriptLac; 

    private bool esteLangaBaraj = false;
    private bool esteReparat = false;

    void Start()
    {
        // Ne asiguram ca la inceput e stricat
        visualsStricat.SetActive(true);
        visualsReparat.SetActive(false);
        
        if(mesajLipsaLemne != null)
            mesajLipsaLemne.SetActive(false);
    }

    void Update()
    {
        // Daca suntem langa baraj, nu e reparat inca, si apasam tasta E
        if (esteLangaBaraj && !esteReparat && Input.GetKeyDown(KeyCode.E))
        {
            IncercareReparare();
        }
    }

    void IncercareReparare()
    {
        // 1. Verificam daca playerul are lemne.
        // NOTA: Aici trebuie sa legi cu scriptul tau de Inventory.
        // De exemplu: FindObjectOfType<Inventory>().GetWoodCount();
        // Pentru acum, voi simula ca avem lemne cu o variabila fictiva:
        int lemnePlayer = FindObjectOfType<PlayerInventory>().GetWoodCount(); // <--- MODIFICA AICI cu logica ta reala

        if (lemnePlayer >= lemneNecesare)
        {
            ReparaBarajul();
            // Scadem lemnele din inventar aici (ex: inventory.ScadeLemne(5));
        }
        else
        {
            AfiseazaMesajEroare();
        }
    }

    void ReparaBarajul()
    {
        esteReparat = true;

        // 2. Schimbam vizualul (Aici poti pune si o animatie daca vrei)
        visualsStricat.SetActive(false);
        visualsReparat.SetActive(true);

        // 3. Activam apa in lac
        if (scriptLac != null)
        {
            scriptLac.finished = true; // Sau cum ai numit variabila bool in scriptul Lacului
        }
        
        Debug.Log("Baraj reparat! Apa a pornit.");
    }

    void AfiseazaMesajEroare()
    {
        if(mesajLipsaLemne != null)
        {
            mesajLipsaLemne.SetActive(true);
            // Ascundem mesajul automat dupa 2 secunde
            Invoke("AscundeMesaj", 2f);
        }
        Debug.Log("Nu ai suficiente lemne!");
    }

    void AscundeMesaj()
    {
        if(mesajLipsaLemne != null)
            mesajLipsaLemne.SetActive(false);
    }

    // Detectam cand playerul intra in zona Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            esteLangaBaraj = true;
            Debug.Log("Player langa baraj. Apasa E.");
        }
    }

    // Detectam cand playerul pleaca
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            esteLangaBaraj = false;
        }
    }
}