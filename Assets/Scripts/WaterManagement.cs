// using UnityEngine;
// using UnityEngine.Tilemaps; // Avem nevoie de asta pentru a controla Tilemap-urile
// using System.Collections;

// public class WaterLevelManager : MonoBehaviour
// {
//     [Header("Setari Tilemaps")]
//     [Tooltip("Tilemap-ul cu raul initial")]
//     public Tilemap waterStart; 
    
//     [Tooltip("Tilemap-ul cu lacul format (final)")]
//     public Tilemap waterEnd;

//     [Header("Setari Tranzitie")]
//     [Tooltip("Cat dureaza tranzitia in secunde")]
//     public float transitionDuration = 2.0f;

//     [Header("Status Joc")]
//     // Aceasta este variabila pe care o vei modifica cand barajul e gata
//     public bool finished = false; 

//     private bool _hasTransitioned = false; // Ca sa nu rulam tranzitia de mai multe ori

//     void Start()
//     {
//         // LA INCEPUT:
//         // Ne asiguram ca apa de start e complet vizibila (Alpha 1)
//         SetTilemapAlpha(waterStart, 1f);
        
//         // Ne asiguram ca lacul final este invizibil (Alpha 0), dar activat ca GameObject
//         SetTilemapAlpha(waterEnd, 0f);
//         waterEnd.gameObject.SetActive(true); 
//     }

//     void Update()
//     {
//         // Verificam daca nivelul e gata si daca nu am facut deja tranzitia
//         if (finished && !_hasTransitioned)
//         {
//             StartCoroutine(TransitionToLake());
//             _hasTransitioned = true;
//         }
//     }

//     // Corutina care se ocupa de animatia de tranzitie
//     IEnumerator TransitionToLake()
//     {
//         float elapsedTime = 0;

//         while (elapsedTime < transitionDuration)
//         {
//             // Calculam procentul tranzitiei (de la 0 la 1)
//             float t = elapsedTime / transitionDuration;

//             // Scadem opacitatea la raul vechi (1 -> 0)
//             SetTilemapAlpha(waterStart, 1f - t);

//             // Crestem opacitatea la lacul nou (0 -> 1)
//             SetTilemapAlpha(waterEnd, t);

//             elapsedTime += Time.deltaTime;
//             yield return null; // Asteapta pana la urmatorul frame
//         }

//         // LA FINAL:
//         // Fortam valorile finale pentru a evita erori de rotunjire
//         SetTilemapAlpha(waterStart, 0f);
//         SetTilemapAlpha(waterEnd, 1f);

//         // Optional: Dezactivam complet vechiul tilemap pentru performanta
//         waterStart.gameObject.SetActive(false);
        
//         Debug.Log("Nivelul apei a crescut! Baraj finalizat.");
//     }

//     // Functie ajutatoare pentru a schimba alpha usor
//     void SetTilemapAlpha(Tilemap map, float alpha)
//     {
//         if (map != null)
//         {
//             Color color = map.color;
//             color.a = alpha;
//             map.color = color;
//         }
//     }
// }

using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class WaterRisingSequence : MonoBehaviour
{
    [Header("Secventa de Apa")]
    [Tooltip("Lista cu Tilemap-uri in ordine, de la Rau la Lacul plin. Trage-le aici in ordinea corecta!")]
    public Tilemap[] waterStages;

    [Header("Setari Timp")]
    [Tooltip("Cat dureaza TOATA umplerea lacului (secunde)")]
    public float totalDuration = 5.0f;

    [Header("Status")]
    public bool finished = false;

    private bool _isAnimating = false;

    void Start()
    {
        // Validare: Avem nevoie de cel putin 2 stadii
        if (waterStages.Length < 2)
        {
            Debug.LogError("Ai nevoie de cel putin 2 Tilemap-uri in lista waterStages!");
            return;
        }

        // Initializare:
        // Primul (index 0) e vizibil (Alpha 1)
        SetTilemapAlpha(waterStages[0], 1f);
        waterStages[0].gameObject.SetActive(true);

        // Restul sunt invizibile (Alpha 0)
        for (int i = 1; i < waterStages.Length; i++)
        {
            SetTilemapAlpha(waterStages[i], 0f);
            waterStages[i].gameObject.SetActive(true); // Le activam ca sa fie gata de fade
        }
    }

    void Update()
    {
        if (finished && !_isAnimating)
        {
            StartCoroutine(AnimateWaterSequence());
            _isAnimating = true; // Blocam sa nu porneasca de 2 ori
        }
    }

    IEnumerator AnimateWaterSequence()
    {
        // Calculam cate tranzitii avem (ex: 4 harti = 3 tranzitii)
        int transitionsCount = waterStages.Length - 1;
        
        // Cat dureaza o singura tranzitie intre doua harti
        float stepDuration = totalDuration / transitionsCount;

        // Trecem prin fiecare pereche: [0]->[1], apoi [1]->[2], etc.
        for (int i = 0; i < transitionsCount; i++)
        {
            Tilemap currentMap = waterStages[i];
            Tilemap nextMap = waterStages[i + 1];

            yield return StartCoroutine(CrossfadeMaps(currentMap, nextMap, stepDuration));
            
            // Dupa ce tranzitia s-a terminat, dezactivam harta veche pentru performanta
            currentMap.gameObject.SetActive(false);
        }

        Debug.Log("Lacul s-a umplut complet!");
    }

    // Aceasta functie face trecerea fina intre oricare doua harti
    IEnumerator CrossfadeMaps(Tilemap fromMap, Tilemap toMap, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Scade vechiul, creste noul
            SetTilemapAlpha(fromMap, 1f - t);
            SetTilemapAlpha(toMap, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asiguram valorile finale curate
        SetTilemapAlpha(fromMap, 0f);
        SetTilemapAlpha(toMap, 1f);
    }

    void SetTilemapAlpha(Tilemap map, float alpha)
    {
        if (map != null)
        {
            Color color = map.color;
            color.a = alpha;
            map.color = color;
        }
    }
}