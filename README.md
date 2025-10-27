# 🦫 Titi's Adventures
## 🧩 Concept general
„Titi's Adventures” este un joc 2D top-down adventure-platformer, cu elemente de collectathon, puzzle solving și light combat.
Jucătorul controlează un castor care explorează o pădure plină de viață și pericole, colectând lemne pentru a construi un baraj la finalul fiecărui nivel. Pe parcurs, descoperă povestea locului, interacționează cu alte animale și trece prin provocări tot mai complexe.

## 🏞️ Poveste & atmosferă
Castorul trăiește într-o pădure afectată de o secetă neobișnuită. Pentru a restabili echilibrul naturii, el trebuie să construiască o serie de baraje care readuc apa în zonele uscate.
Pe drum, va întâlni:
- Alți castori care îi oferă sfaturi, misiuni și fragmente din poveste.
- Animale ostile (vulpi, lupi, bufnițe etc.) care îl atacă.
- Animale prietenoase (căprioare, arici, păsări) pe care le poate ajuta în schimbul unor lemne sau abilități.

## 🎯 Obiectiv principal
Colectează toate lemnele din nivel pentru a reconstrui barajul și a trece la următorul.

## 🪓 Iteme și power-ups
1. Lemne – obiectivul principal de colectat pentru a termina nivelul.
2. Ghinde – colectabile secundare care cresc viața sau regenerează HP.
3. Iteme magice (frunze speciale / pietre runice / glande energetice) – oferă puteri temporare precum:
    - viteză crescută
    - sărituri mai lungi
    - rezistență sporită la atacuri

## 🕹️ Mecanici de joc principale
- Mișcare 2D top-down: deplasare în toate direcțiile.
- Combat simplu: atac de aproape (cu coada, poate mai târziu unelte din lemn).
- Interacțiune: vorbit cu NPC-uri, rezolvat puzzle-uri, activat obiecte.
- Colectare: ridicarea obiectelor, gestionarea inventarului simplificat.
- Construire: secvență finală de construcție a barajului (mini-joc sau animație interactivă).
- Progresie pe nivele: creșterea dificultății (inamicii mai agresivi, puzzle-uri mai complexe, zone mai mari).

## 🧠 Tipuri de gameplay
- Exploration / Adventure – descoperirea lumii și a poveștii.
- Puzzle-solving – misiuni de logică pentru a obține lemne.
- Action – lupte cu animale ostile.
- Resource management – colectarea și folosirea materialelor pentru baraj.

## 🧩 Categorii în care se încadrează jocul
- Genre principal: Adventure 2D top-down
- Sub-genres: Action, Puzzle, Collectathon
- Style: Narrative + casual progression

## 👥 Împărțirea sarcinilor — cu minimă dependență
### 🧍 Persoana 1 – Player Controller & Interaction System
  - Mișcarea jucătorului (walk, run, attack)
  - Scripturi pentru interacțiuni (colectare, vorbit cu NPC, activare obiecte)
  - Sistemul de viață al jucătorului
### 🐾 Persoana 2 – Enemies & AI System
  - Script pentru comportament inamici (patrulare, urmărire, atac)
  - Sistem viață inamici + drop item la moarte
  - Tipuri diferite de inamici (rapid, puternic, ranged etc.)
### 🌲 Persoana 3 – World, Level Design & Item System
  - Crearea tilemap-urilor (pădure, obstacole, apă etc.)
  - Scripturi pentru obiecte colectabile (lemne, ghinde, iteme magice)
  - Mecanica de construcție a barajului (final de nivel)
