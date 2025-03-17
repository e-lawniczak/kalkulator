# Calcualtor app

Aplikacja kalkulatora, implementująca podstawowe, oraz rozszerzone operacje matematyczne.

Projekt przygotowany w oparciu o C# .NET 8.0 z WPF. Do połączenia z bazą wykorzystano EntityFramework Core

## Uruchomienie aplikacji
W repozytorium znajduje się plik Calculator.zip. Po rozpakowaniu go w folderze znajdziemy plik Calculator.exe, który uruchamia aplikację.

## Dokumentacja kodu


### Struktura folderów
W głównym folderze projektu *Kalkulator* mamy główne pliki takie jak:
- App.xaml/.xaml.cs - plik startowy aplikacji
- MainWindow.xaml/.xaml.cs - główny plik widoku aplikacji wraz z główną logiką
- Operations.cs - klasa zarządzająca operacjami
- HisoryDb.cs - klasa obsługująca operacje na bazie danych
- Helpers.cs - funkcje pomocnicze wykorzystywane w więcej niż jednym pliku

Dodatkowo wyróżniamy foldery:
- Model - zawierający opis modeli bazy danych oraz kontekst bazy danych
- Migrations - zawierający migracje bazy danych
- database - folder tworzony podczas pierwszego uruchomienia aplikacji, w którym znajduje się plik z bazą danych

### Opis bazy danych
Baza danych skłąda sięz 2 tabel:
- Operations - tabeli, w której trzymane są kolejne operacje (Value A, Value B, Result, OperatorId)
  - OperatorId jest kluczem obcym łączącym tabelę z tabelą Operators
- Operators - tabela zawierająca dostępne w aplikacji operatory

Zdecydowałem się stworzenie relacyjnej reprezentacji danych, aby uniknąć powtórzeń i umożliwić ewentualną zmianę nazwy/skrótu operatora.

Można by też zastosować tutaj bazę NoSql z uwagi na niezbyt skomplikowaną strukturę danych.

### Typy danych
W projekcie użyłem typu danych decimal, który zapewnia większą dokładność niż double i nie powoduje dziwnych zaokrągleń pozwalając na lepsze odwzorowanie długich ułamków czy ułamków okresowych.
W bazie danych zapisany jest jako typ text, ponieważ SQLite nie udostępnia typu, któy mógłby bezstratnie przechować wartosć zmiennej decimal.
 
