using System;
using UnityEngine;

public class Dialog
{

    // Enum DialogListNames
    //{
    //    CastorApa,
    //    PersonajRau,
    //    PersonajBun
    //}

    static int lemneNecesare = 5;
    public static string[] CastorApa= {
        //pt castor apa
    //dupa interact from Titi to CastorApa
        $"Titi! Am nevoie de ajutorul tau! Am ramas fara lemne pentru baraj! Poti sa ma ajuti sa adun niste lemne din padure? Am nevoie de {lemneNecesare} bucati de lemne",
        "Multumesc Titi! Esti un prieten adevarat! Cu ajutorul tau, barajul va fi gata in curand.",
        "Ai adunat toate lemnele necesare! Acum pot sa continui constructia barajului. Iti sunt recunoscator!",
        "Oh, nu! Barajul s-a stricat din nou! Am nevoie de inca X bucati de lemne pentru a-l repara. Ma poti ajuta din nou, Titi?"
    };

    
   


    //pt personaj rau
    //static int obiecteNecesare = 3;
    public static string[] PersonajRau= {
        //dupa interact from Titi to PersonajRau
        "Haha! Credeai ca poti trece pe aici? Acesta este teritoriul meu si nu te voi lasa sa treci!",
        //se bate cu Titi
        "Nu ai nicio sansa impotriva mea! Pregateste-te sa fii invins!",
        //Titi castiga lupta
        "Nu... cum ai reusit sa ma invingi? Acesta nu este sfarsitul! Voi reveni mai puternic!",
        //Titi colecteaza ceva de la PersonajRau
        "Nu pot sa cred ca ai reusit sa ma invingi si sa iei ceea ce imi apartine! Acesta nu este sfarsitul nostru, Titi!"
            //dupa ce Titi a luat ceva de la PersonajRau
            //the end
    };


    //pt personaj bun
    //static int cadouSpecial = 1;
    public static string[] PersonajBun= {
        //dupa interact from Titi to PersonajBun
        "Buna, Titi! Am auzit ca ai avut o aventura interesanta in padure. Vrei sa imi povestesti mai multe despre ea?",
        //Titi povesteste aventurile
        "Wow, Titi! Esti cu adevarat curajos si aventuros! Sunt mandru de tine pentru tot ce ai realizat.",
        //PersonajBun ofera un cadou lui Titi
        "Pentru curajul si spiritul tau aventuros, vreau sa iti ofer acest cadou special. Sper sa iti aduca noroc in calatoriile tale viitoare!",
        //the end



    };



    public static string[][] dialogList = { CastorApa, PersonajRau, PersonajBun };
}
