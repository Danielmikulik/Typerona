using UnityEngine;

public class WordGenerator : MonoBehaviour
{

    private static string[] wordList = {   "chodník", "klávesnica", "strom", "ochrániť", "periodický",
                                    "pochmúrne", "majestátny", "skok", "pekný", "zranenie", "hudobný",
                                    "pamäť", "pripojiť", "prasklina", "známka", "topánka", "oblačno", "chorý",
                                    "hrnček", "horúca", "koláč", "nebezpečný", "mama", "rustikálny", "ekonomické",
                                    "divý", "rezať", "paralelný", "drevo", "povzbudenie", "prerušenie",
                                    "návod", "dlhý", "veliteľ", "otec", "signál", "spoľahlivý", "neúspešný",
                                    "vlasy", "representatívny", "zem", "grep", "pyšná", "pocit",
                                    "zábavný", "prírastok", "tiché", "hrať", "podlaha", "početný",
                                    "priateľ", "pizza", "budova", "organický", "minulosť", "stíšiť", "nezvyčaný",
                                    "mäkký", "analyzovať", "bedňa", "jednoduchý", "protest", "starostlivo",
                                    "spoločnosť", "hlava", "žena", "nedočkavý", "halda", "dramatický", "súčasnosť",
                                    "hriech", "krabica", "koláče", "úžasnáá", "koreň", "dostupné", "dážďovka", "vosk",
                                    "nudný", "ničiť", "hnev", "chutný", "navyše", "podnos", "blzon", "vzácny",
                                    "účet", "misto", "myšlienka", "vzdialený", "šikovný", "tréning", "krém",
                                    "zapáliť", "nezmysel", "láska", "verdikt", "obor"    };

    public static string GetRandomWord()
	{
		int randomIndex = Random.Range(0, wordList.Length);
		string randomWord = wordList[randomIndex];

		return randomWord;
	}

}
