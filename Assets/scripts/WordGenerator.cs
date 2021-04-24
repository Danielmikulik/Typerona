using System.IO;
using System.Linq;
using UnityEngine;

public class WordGenerator
{
    private static string[] wordList = SetWordList();

    public static string GetRandomWord()
	{
		int randomIndex = Random.Range(0, wordList.Length);
		string randomWord = wordList[randomIndex];

		return randomWord;
	}

    private static string[] SetWordList()
    {
        string filePath = Application.streamingAssetsPath + "/WordList" + ".txt";

        string[] newWordList = File.ReadAllLines(filePath).ToArray();

        //Debug.Log(newWordList.Count() + " -> " + newWordList.Distinct().Count());
        if (newWordList.Distinct().Count() >= 50)
        {
            if (newWordList.Distinct().Count() < newWordList.Length)
            {
                newWordList = newWordList.Distinct().ToArray();
            }

            for (int i = 0; i < newWordList.Length; i++)
            {
                newWordList[i] = newWordList[i].Trim();
            }   
            
            return newWordList;          
        }

        return new string[] {   "chodník", "klávesnica", "strom", "ochrániť", "periodický",
                                "pochmúrne", "majestátny", "skok", "pekný", "zranenie", "hudobný",
                                "pamäť", "pripojiť", "prasklina", "známka", "topánka", "oblačno", "chorý",
                                "hrnček", "horúca", "koláč", "nebezpečný", "mama", "rustikálny", "ekonomické",
                                "divý", "rezať", "paralelný", "drevo", "povzbudenie", "prerušenie",
                                "návod", "dlhý", "veliteľ", "otec", "signál", "spoľahlivý", "neúspešný",
                                "vlasy", "reprezentatívny", "zem", "grep", "pyšná", "pocit",
                                "zábavný", "prírastok", "tiché", "hrať", "podlaha", "početný",
                                "priateľ", "pizza", "budova", "organický", "minulosť", "stíšiť", "nezvyčajný",
                                "mäkký", "analyzovať", "bedňa", "jednoduchý", "protest", "starostlivo",
                                "spoločnosť", "hlava", "žena", "nedočkavý", "halda", "dramatický", "súčasnosť",
                                "hriech", "krabica", "koláče", "úžasná", "koreň", "dostupné", "dážďovka", "vosk",
                                "nudný", "ničiť", "hnev", "chutný", "navyše", "podnos", "blázon", "vzácny",
                                "účet", "miesto", "myšlienka", "vzdialený", "šikovný", "tréning", "krém",
                                "zapáliť", "nezmysel", "láska", "verdikt", "obor"    }; 
}

}
