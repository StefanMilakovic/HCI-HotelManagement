# Hotel Management System

## Opis

Ova aplikacija je razvijena u sklopu projekta na predmetu Interakcija Čovjek-Računar na Elektrotehničkom fakultetu u Banjoj Luci. Aplikacija omogućava upravljanje osnovnim operacijama hotela, kao što su upravljanje gostima, rezervacijama, servisima i računima.

Aplikacija je dizajnirana tako da omogućava rad sa dva tipa korisnika: recepcionerima i administratorima. Aplikacija se sastoji od tri prozora: prozor za prijavu, prozor za recepcionera i prozor za administratora. U prozorima za administratora i recepcionera, u gornjem dijelu prozora nalazi se navigacioni bar. Na desnom kraju navigacione trake se nalazi dugme za odjavu. 

---
Sadržaj:

- [Korisnici](#korisnici)
  - [Recepcioner](#recepcioner)
  - [Administrator](#administrator)
- [Korisnička podešavanja](#Korisnička-podešavanja)
- [Korištene tehnologije](#Korištene-tehnologije)


## Korisnici
<img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/LoginWindow.png?raw=true" alt="LoginWindow" width="350">
Da bi se korisnik prijavio na svoj nalog potrebno je da unese svoje korisničko ime i lozinku u formi za prijavu.

### Recepcioner
<img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/RecBar.png?raw=true" alt="RecBar" width="600">
Recepcioner ima sljedeće opcije i funkcionalnosti u aplikaciji:

- **Upravljanje gostima**  
Ova opcija se sastoji od dva taba:

  Dodaj gosta: Za unos novog gosta, recepcioner je obavezan da popuni sva tražena polja, koja uključuju ime, prezime, broj pasoša, email i broj telefona. Aplikacija neće dozvoliti unos gosta dok sva polja ne budu popunjena. Nakon unosa podataka, potrebno je pritisnuti dugme Dodaj.
  
  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/DodajgGosta.png?raw=true" alt="DodajGosta" width="500">

  Pregled gostiju: Ovaj tab omogućava tabelarni prikaz svih gostiju unesenih u bazu podataka hotela, sa svim relevantnim informacijama. Pored svakog gosta, nalazi se dugme za brisanje, čime je moguće ukloniti gosta iz sistema. Takođe, na dnu prozora nalazi se search box koji omogućava pretragu gostiju prema broju pasoša. Korištenjem ovog pretraživača, lista gostiju će se automatski ažurirati kako korisnik kuca.
  
  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/PregledGostiju.png?raw=true" alt="PregledGostiju" width="500">
  
- **Upravljanje rezervacijama**  
Ova opcija se sastoji od dva taba:

    Nova rezervacija: Recepcioner mora izabrati gosta iz liste gostiju koji su već zavedeni u hotelskoj bazi podataka. Ako gosta nema na listi, postoji dugme koje vodi do prikaza za dodavanje novog gosta. Nakon odabira gosta, potrebno je izabrati tip sobe (Single room, Double room, Suite, Deluxe room ili Family room) i odabrati datum Check-in i Check-out. Na osnovu tih parametara, aplikacija će prikazati listu dostupnih soba.
  
  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/NovaRezervacija.png?raw=true" alt="NovaRezervacija" width="500">

    Pregled rezervacija: Ovaj tab omogućava tabelarni prikaz svih postojećih rezervacija, uz podatke o rezervaciji i ime recepcionera koji je napravio rezervaciju. Pored svake rezervacije nalazi se dugme za brisanje, čime se omogućava uklanjanje rezervacije. Na dnu prikaza se nalazi search box za pretragu rezervacija prema imenu gosta. Pretraga se automatski ažurira dok korisnik unosi tekst.

  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/PregledRezervacija.png?raw=true" alt="PregledRezervacija" width="500">

- **Upravljanje servisima**  
Ova opcija se sastoji od dva taba: 

    Odabir rezervacije: U prvom tabu, rezervacije su prikazane u obliku kartica, sa dugmetom za odabir pored svake kartice. Da bi dodijelio servis rezervaciji, recepcioner mora odabrati jednu rezervaciju. Nakon odabira, u donjem lijevom uglu prikazuje se ID odabrane rezervacije, a takođe je dostupna i opcija pretrage rezervacija prema imenu gosta putem search box-a.

  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/IzaberiRezervaciju.png?raw=true" alt="IzaberiRezervaciju" width="500">

    Odabir servisa: Nakon što je rezervacija odabrana, recepcioner prelazi na drugi tab, gdje se prikazuju svi dostupni servisi u bazi podataka hotela. Servisi su prikazani u obliku kartica. Da bi se servis dodijelio rezervaciji, potrebno je odabrati željeni servis, unijeti količinu (broj puta koji će gost koristiti uslugu) i pritisnuti dugme Dodaj.

  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/IzaberiServis.png?raw=true" alt="IzaberiServis" width="500">

- **Upravljanje računima**  
Ova opcija omogućava izdavanje i pregled računa.

    Generisanje računa: U prvom tabu, recepcioner mora odabrati rezervaciju, a dostupna je i opcija pretrage rezervacija prema imenu gosta. Nakon odabira rezervacije, računi se mogu generisati.

  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/IzaberiRezervacijuRac.png?raw=true" alt="IzaberiRezervacijuRac" width="500">

    Pregled računa: U drugom tabu nalaze se svi izdati računi, sa informacijama o ID računa, imenu gosta, datumu izdavanja, ID rezervacije i ukupnom iznosu. Ukupni iznos se izračunava na osnovu broja noći, cijene odabranog tipa sobe po noći, kao i svih korištenih servisa. Takođe, omogućena je pretraga računa prema imenu gosta.

  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/PregledRacuna.png?raw=true" alt="PregledRacuna" width="500">

---
### Administrator
<img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/AdminBar.png?raw=true" alt="AdminBar" width="600">
Administrator ima šire mogućnosti u odnosu na recepcionera. Uz sve opcije koje su dostupne recepcioneru, administrator može upravljati korisnicima, sobama, servisima i izdavati izvještaje.

- **Upravljanje korisnicima**  
Ova opcija sastoji se iz tri taba:

    Pregled korisnika: Tabelarni prikaz svih korisnika u sistemu, uključujući ID korisnika, ime, prezime, korisničko ime i ulogu koju korisnik ima na sistemu. Na dnu se nalazi pretraga na osnovu korisničkog imena.
  
  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/PregledKorisnika.png?raw=true" alt="PregledKorisnika" width="500">

    Dodavanje novog korisnika: Ovdje je moguće dodati novog korisnika. Za svakog korisnika potrebno je unijeti sva obavezna polja, kao i odabrati odgovarajuću ulogu (administrator ili recepcioner).
  
  <img src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/NoviKorisnik.png?raw=true" alt="NoviKorisnik" width="500">

    Izmjena korisnika: U ovom tabu, administratori mogu kliknuti na željeno polje (ime, prezime, korisničko ime) i direktno ga modifikovati u tabeli. Izmjene će biti automatski sačuvane u bazi podataka. Takođe, iz ovog prikaza je moguće obrisati korisnike. Na dnu se nalazi pretraga na osnovu korisničkog imena.

  <img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/UpravljajKorisnicima.png?raw=true" alt="UpravljajKorisnicima" width="500">

- **Upravljanje sobama**
Ova opcija sastoji se iz tri taba:
  Pregled soba: Ovdje se nalazi tabelarni prikaz svih soba, pri čemu svaka soba uključuje ID sobe, broj sobe, sprat i tip sobe.

  <img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/PregledSoba.png?raw=true" alt="PregledSoba" width="500">

  Dodavanje nove sobe: U ovom tabu, administrator može dodati novu sobu, pri čemu je obavezno unijeti broj sobe, sprat i tip sobe.
  
  <img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/NovaSoba.png?raw=true" alt="NovaSoba" width="500">

  Izmjena sobe: U ovom tabu administrator ima mogućnost direktne izmjene podataka o sobama, što uključuje promjene broja sobe i sprata.

  <img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/UpravljajSobama.png?raw=true" alt="UpravljajSobama" width="500">


- **Upravljanje gostima**  
Administrator u ovom slučaju ima iste mogućnosti kao i recepcioner.

- **Upravljanje servisima**  
  Pregled servisa: Ovdje se nalazi tabelarni prikaz svih dostupnih servisa, uz mogućnost pregleda svih relevantnih podataka o svakom servisu.
  
  <img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/PregledServisaAdmin.png?raw=true" alt="PregledServisa" width="500">

  Dodavanje novog servisa: U ovom tabu administrator može dodati novi servis. Potrebno je unijeti naziv servisa, kao i cijenu koja će biti dodijeljena tom servisu.

  <img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/DodajServisAdmin.png?raw=true" alt="NoviServis" width="500">

  Izmjena servisa: Ovdje administrator može izmijeniti postojeće servise, uz mogućnost promjene svih podataka koji su prethodno uneseni.

  <img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/UpravljajServisimaAdmin.png?raw=true" alt="UpravljajServisima" width="500">

- **Upravljanje izvještajima**  
  Administrator, za razliku od recepcionera, ima pristup detaljnijem tabelarnom prikazu rezervacija i računa. Samo administrator ima privilegiju da briše račune, dok obje vrste korisnika (recepcioner i administrator) imaju mogućnost izmjene rezervacija.

  <img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/PregledRezervacijaAdmin.png?raw=true" alt="PregledRezervacija" width="500">
  

  <img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/PregledRacunaAdmin.png?raw=true" alt="PregledRacuna" width="500">

---
## Korisnička podešavanja
Aplikacija omogućava promjenu jezika i teme. Podržani jezici su engleski i srpski. Dugmad za promjenu jezika i teme su dostupna u svakom trenutku korištenja aplikacije.

<img align="center" src="https://github.com/StefanMilakovic/HCI-HotelManagement/blob/master/Screenshots/TemaJezik.png?raw=true" alt="TemaJezik" width="500">
   
## Tehnologije

Za izradu ove aplikacije, korištene su sljedeće tehnologije
- **WPF (Windows Presentation Foundation)** - Za kreiranje korisničkog interfejsa aplikacije.
- **Material Design in XAML Toolkit** - Za primjenu Material Design stilova i ikonica.
- **C#** - Kao programski jezik za implementaciju logike aplikacije.
- **XAML (Extensible Application Markup Language)** - Za definisanje korisničkog interfejsa.
- **MySQL** - Baza podataka.

