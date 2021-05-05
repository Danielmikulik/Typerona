using System.IO;
using System.Linq;
using UnityEngine;

public class WordGenerator
{
    private static string[] wordList = SetWordList();   //list of words used in game

    public static string GetRandomWord()
	{
		int randomIndex = Random.Range(0, wordList.Length);
		string randomWord = wordList[randomIndex];

		return randomWord;
	}

    private static string[] SetWordList()
    {
        string filePath = Application.streamingAssetsPath + "/WordList" + ".txt";

        string[] newWordList = File.ReadAllLines(filePath).ToArray();   //in file, there's one word at a line

        if (newWordList.Distinct().Count() >= 50)   //file must contain at least 50 distinct words. With too few words, the game might crash
        {
            if (newWordList.Distinct().Count() < newWordList.Length)
            {
                newWordList = newWordList.Distinct().ToArray();
            }

            for (int i = 0; i < newWordList.Length; i++)
            {
                newWordList[i] = newWordList[i].Trim();     //trims whitespaces at the begining or end of line
            }   
            
            return newWordList;          
        }

        //if the num of distinct words in file is too low, backup list is used
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
