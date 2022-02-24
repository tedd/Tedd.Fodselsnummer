using System;

namespace Tedd.Fodselsnummer;

[Flags]
public enum FodselsnummerType
{
#pragma warning disable CA1008 // Enums should have zero value
    Normal = 0,
#pragma warning restore CA1008 // Enums should have zero value
    /// <summary>
    /// Et D-nummer er ellevesifret, som ordinære fødselsnummer, og består av en modifisert sekssifret fødselsdato og et femsifret personnummer. Fødselsdatoen modifiseres ved at det legges til 4 på det første sifferet: en person født 1. januar 1980 får dermed fødselsdato 410180, mens en som er født 31. januar 1980 får 710180. De to kontrollsifrene er også på D-nummer riktige og de beregnes etter at 4 er lagt til i første siffer. Kontrollsifrene beregnes etter modulus 11. Siste siffer i individnummeret indikerer personens kjønn - ulike siffer for menn og like siffer for kvinner.
    /// 
    /// Når en innvandret person skal få tildelt fødselsnummer, gis det ofte først et midlertidig D-nummer. D-en kommer av at systemet opprinnelig var knyttet til utenlandske og utenlandsboende sjøfolk på norske skip: Direktoratet for sjømenn. For utlendinger som har lovlig opphold i Norge brukes dette i forbindelse med skatt og offentlige tjenester. Mange utlendinger, særlig fra Sverige og Danmark, arbeider en periode i Norge og trenger et nummer.
    /// 
    /// Det femsifrede personnummeret gis ikke i serier, men tildeles fortløpende. For barn som er født i Norge, tildeles regulært fødselsnummer så raskt som mulig. For utlendinger tildeles det normalt etter seks måneder dersom man søker om det.
    /// 
    /// Bruken av D-nummer medfører i dag en rekke ulemper, og påfører også folkeregisteret et til tider komplisert merarbeid. Det foreligger derfor forslag om å avvikle systemet, eller i det minste å avgrense det i betydelig utstrekning. I Sverige brukes samordningsnummer i slike situasjoner, og i Danmark Erstatningspersonnummer.
    /// 
    /// Per 2010 var det 1,1 millioner registrerte D-nummer i Norge.
    /// </summary>
    D = 1,
    /// <summary>
    /// Et H-nummer er ellevesifret, som ordinære fødselsnummer, og består av en modifisert sekssifret fødselsdato og et femsifret personnummer. Fødselsdatoen modifiseres ved at det legges til 4 på det tredje sifferet: en person født 1. januar 1980 får dermed fødselsdato 014180, mens en som er født 31. januar 1980 får 314180.
    ///
    /// Et H-nummer er et hjelpenummer, en virksomhetsintern, unik identifikasjon av en person som ikke har fødselsnummer eller D-nummer eller hvor dette er ukjent. Slike numre brukes blant annet innen helsevesenet i elektroniske pasientjournaler.
    /// </summary>
    H = 2,
    //DAndF = 3, // Not valid
    /// <summary>
    /// Helsevesenet har behov for unikt å identifisere pasienter som ikke har et kjent fødselsnummer eller D-nummer. Bruk av elektronisk kommunikasjon mellom virksomheter medfører at standarden for virksomhetsinterne H-nummer ikke er tjenlig. Nasjonal IKT som er spesialisthelsetjenesten sitt samarbeidsorgan for IKT har derfor bedt Norsk Helsenett SF om å utvikle og forvalte et nytt konsept for hjelpenummer til bruk for hele helsevesenet og deres samarbeidspartnere.
    /// 
    /// KITH har utarbeidet forslag til en oppdatert standard for felles hjelpenummer (FH-nummer) til bruk på tvers av virksomheter. Denne var ute på høring i november 2009 og ble publisert som standard 18. januar 2010. Algoritmen er ellevesifret med to kontrollsiffer, som i ordinære fødselsnummer, men med første siffer 8 eller 9. De resterende sifrene (posisjon to til ni) skal være et tilfeldig valgt nummer. Nummeret skal ikke være informasjonsbærende og vil derfor i motsetning til fødselsnummeret ikke vise fødselsdato, kjønn eller i hvilken rekkefølge nummeret er tildelt. Denne algoritmen vil gi ca. 160 millioner tilgjengelige nummer.
    /// 
    /// Eksempler på pasienter som vil bli tildelt FH-nummer er nyfødte, turister og andre med midlertidig opphold samt til pasienter som ikke kan gjøre rede for seg fordi de er bevisstløse eller på grunn av språkproblem. FH-nummer skal kun benyttes frem til fødselsnummer eller D-nummer er kjent.
    /// </summary>
    FH = 4,
    //DAndFG = 5, // Not valid
    //HAndFH = 6, // Not valid
    //DAndHAndFH = 7 // Not valid
}
