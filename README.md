﻿# eKözig Személy Listázó és Regisztráló szoftver

## Szoftver képességei

### "Listázás" fül
- Adatbázisban lévő rekordok kiírása, a cím részleteivel együtt
- Soronként adatmódosítás/törlés lehetősége
	- Törlés esetén pop-up ablakban kell megerősíteni a törlés szándékát

### "Új rekord fül" / módosítás
- Adatbázisba új személy adatainak felvitele
- "Listázás" oldalról kiinduló adatmódosítás esetén a személy adatai betöltődnek és ugyanazon oldal segítségével módosíthatóak
- Hibaüzenet szolgáltatása amennyiben:
	- Nem szabályos érték lett megadva
	- Kötelezően kitöldendő mező nem lett kitöltve

## Ismert gyengeségek
- A megjelenés, képernyőméret függően való alkalmazkodás front-end oldalon nem létező fogalom, ennek okai tudáshiány CSS/front-end oldalon

## Komponensek leírása
- A "Database_Export" mappában megtalálható az adatbázis sémája

## Felhasznált fő anyagok
- https://www.youtube.com/watch?v=T-e554Zt3n4&t=1267s
	- Ezen keresztül éreztem rá arra, hogy melyik komponens mit-hol csinál és alakítottam át az emlékeim szerint kért MVC modellre
- különböző neten keresett kódok
- ChatGPT
	- Tanulányaim és ezen projekt során is bevallottan aktívan használom
	- Amit szem előtt tartok a használat során
		- Programozni akarok megtanulni, nem programoztatni, a kódokat minden esetben átnézem
		- Az LLM-ek az eloszlás függvényük szerinti legjobban illő kódot írják ki és mivel a legnagyobb számosságú kódoknak a minősége minimum kérdőjeles, ezért extra odafigyeléssel kell értelmezni