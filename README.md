# Hotel Management System - Korisničko uputstvo

## Opis

Ova aplikacija je razvijena u sklopu projekta na predmetu Interakcija Čovjek-Računar na Elektrotehničkom fakultetu u Banjoj Luci. Aplikacija omogućava upravljanje osnovnim operacijama hotela, kao što su upravljanje gostima, rezervacijama, servisima i računima.

---
Aplikacija je dizajnirana tako da omogućava rad sa dva tipa korisnika: recepcionerima i administratorima. Aplikacija se sastoji od tri prozora: prozor za prijavu, prozor za recepcionera i prozor za administratora. U prozorima za administratora i recepcionera, u gornjem dijelu prozora nalazi se navigacioni bar. Na desnom kraju navigacione trake se nalazi dugme za odjavu. 

---
Sadržaj:

- [Korisnici](#korisnici)
  - [Recepcioner](#recepcioner)
  - [Administrator](#administrator)
- [Korisnička podešavanja](#instalacija)
- [Korištene tehnologije](#korišćenje)


## Korisnici
### Recepcionerski nalog

Recepcioner ima sljedeće opcije i funkcionalnosti u aplikaciji:

- **Upravljanje gostima**  
Ova opcija se sastoji od dva taba:

  Dodaj gosta: Za unos novog gosta, recepcioner je obavezan da popuni sva tražena polja, koja uključuju ime, prezime, broj pasoša, email i broj telefona. Aplikacija neće dozvoliti unos gosta dok sva polja ne budu popunjena. Nakon unosa podataka, potrebno je pritisnuti dugme Dodaj.

  Pregled gostiju: Ovaj tab omogućava tabelarni prikaz svih gostiju unesenih u bazu podataka hotela, sa svim relevantnim informacijama. Pored svakog gosta, nalazi se dugme za brisanje, čime je moguće ukloniti gosta iz sistema. Takođe, na dnu prozora nalazi se search box koji omogućava pretragu gostiju prema broju pasoša. Korišćenjem ovog pretraživača, lista gostiju će se automatski ažurirati kako korisnik kuca.

- **Upravljanje rezervacijama**  
Ova opcija se sastoji od dva taba:

    Nova rezervacija: Recepcioner mora izabrati gosta iz liste gostiju koji su već zavedeni u hotelskoj bazi podataka. Ako gosta nema na listi, postoji dugme koje vodi do prikaza za dodavanje novog gosta. Nakon odabira gosta, potrebno je izabrati tip sobe (Single room, Double room, Suite, Deluxe room ili Family room) i odabrati datum Check-in i Check-out. Na osnovu tih parametara, aplikacija će prikazati listu dostupnih soba.

    Pregled rezervacija: Ovaj tab omogućava tabelarni prikaz svih postojećih rezervacija, uz podatke o rezervaciji i ime recepcionera koji je napravio rezervaciju. Pored svake rezervacije nalazi se dugme za brisanje, čime se omogućava uklanjanje rezervacije. Na dnu prikaza se nalazi search box za pretragu rezervacija prema imenu gosta. Pretraga se automatski ažurira dok korisnik unosi tekst.

- **Upravljanje servisima**  
Ova opcija se sastoji od dva taba: 

    Odabir rezervacije: U prvom tabu, rezervacije su prikazane u obliku kartica, sa dugmetom za odabir pored svake kartice. Da bi dodijelio servis rezervaciji, recepcioner mora odabrati jednu rezervaciju. Nakon odabira, u donjem levom uglu prikazuje se ID odabrane rezervacije, a takođe je dostupna i opcija pretrage rezervacija prema imenu gosta putem search box-a.

    Odabir servisa: Nakon što je rezervacija odabrana, recepcioner prelazi na drugi tab, gdje se prikazuju svi dostupni servisi u bazi podataka hotela. Servisi su prikazani u obliku kartica. Da bi se servis dodelio rezervaciji, potrebno je odabrati željeni servis, unijeti količinu (broj puta koji će gost koristiti uslugu) i pritisnuti dugme Dodaj.

- **Upravljanje računima**  
Ova opcija omogućava izdavanje i pregled računa.

    Generisanje računa: U prvom tabu, recepcioner mora odabrati rezervaciju, a dostupna je i opcija pretrage rezervacija prema imenu gosta. Nakon odabira rezervacije, računi se mogu generisati.

    Pregled računa: U drugom tabu nalaze se svi izdati računi, sa informacijama o ID računa, imenu gosta, datumu izdavanja, ID rezervacije i ukupnom iznosu. Ukupni iznos se izračunava na osnovu broja noći, cijene odabranog tipa sobe po noći, kao i svih korištenih servisa. Takođe, omogućena je pretraga računa prema imenu gosta.

---
### Administratorski nalog

Administrator ima šire mogućnosti u odnosu na recepcionera. Uz sve opcije koje su dostupne recepcioneru, administrator može upravljati korisnicima, sobama, servisima i izdavati izvještaje.

- **Upravljanje korisnicima**  
Ova opcija sastoji se iz tri taba:

    Pregled korisnika: Tabelarni prikaz svih korisnika u sistemu, uključujući ID korisnika, ime, prezime, korisničko ime i ulogu koju korisnik ima na sistemu. Na dnu se nalazi pretraga na osnovu korisničkog imena.

    Dodavanje novog korisnika: Ovdje je moguće dodati novog korisnika. Za svakog korisnika potrebno je unijeti sva obavezna polja, kao i odabrati odgovarajuću ulogu (administrator ili recepcioner).

    Izmjena korisnika: U ovom tabu, administratori mogu kliknuti na željeno polje (ime, prezime, korisničko ime) i direktno ga modifikovati u tabeli. Izmjene će biti automatski sačuvane u bazi podataka. Takođe, iz ovog prikaza je moguće obrisati korisnike. Na dnu se nalazi pretraga na osnovu korisničkog imena.

- **Upravljanje sobama**
Ova opcija sastoji se iz tri taba:
  Pregled soba: Ovdje se nalazi tabelarni prikaz svih soba, pri čemu svaka soba uključuje ID sobe, broj sobe, sprat i tip sobe.

  Dodavanje nove sobe: U ovom tabu, administrator može dodati novu sobu, pri čemu je obavezno unijeti broj sobe, sprat i tip sobe.

  Izmjena sobe: U ovom tabu administrator ima mogućnost direktne izmjene podataka o sobama, što uključuje promjene broja sobe i sprata.

- **Upravljanje gostima**  
Administrator u ovom slučaju ima iste mogućnosti kao i recepcioner.

- **Upravljanje servisima**  
  Pregled servisa: Ovdje se nalazi tabelarni prikaz svih dostupnih servisa, uz mogućnost pregleda svih relevantnih podataka o svakom servisu.

  Dodavanje novog servisa: U ovom tabu administrator može dodati novi servis. Potrebno je unijeti naziv servisa, kao i cijenu koja će biti dodijeljena tom servisu.

  Izmjena servisa: Ovdje administrator može izmijeniti postojeće servise, uz mogućnost promjene svih podataka koji su prethodno uneseni.

- **Upravljanje izvještajima**  
  Administrator, za razliku od recepcionera, ima pristup detaljnijem tabelarnom prikazu rezervacija i računa. Samo administrator ima privilegiju da briše račune, dok obje vrste korisnika (recepcioner i administrator) imaju mogućnost izmene rezervacija.

---
## Korisnička podešavanja
Aplikacija omogućava promjenu jezika i teme. Podržani jezici su engleski i srpski. Dugmad za promjenu jezika i teme su dostupna u svakom trenutku korištenja aplikacije.
   
##Tehnologije

Za izradu ove aplikacije, korištene su sljedeće tehnologije
- **WPF (Windows Presentation Foundation)** - Za kreiranje korisničkog interfejsa aplikacije.
- **Material Design in XAML Toolkit** - Za primjenu Material Design stilova i ikonica.
- **C#** - Kao programski jezik za implementaciju logike aplikacije.
- **XAML (Extensible Application Markup Language)** - Za definisanje korisničkog interfejsa.
- **MySQL** - Baza podataka.

