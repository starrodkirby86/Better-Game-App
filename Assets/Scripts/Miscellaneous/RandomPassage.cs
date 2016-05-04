/**
 * Random Passages script:
 * 
 * A script that generates a random sequence of sentences that create a passage.
 * Intended to make as an introduction sort of text.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Text;

public class RandomPassage : MonoBehaviour {

	// These can be dynamically sized.
	// For more information on an ArrayList, refer to
	// https://msdn.microsoft.com/en-us/library/system.collections.arraylist(v=vs.110).aspx
	// http://www.tutorialspoint.com/csharp/csharp_arraylist.htm
	public ArrayList sentences = new ArrayList();

	// TOKENS
	// If we happen to change the token labels, we can just use this.
	public const string d = "$$"; 		// delimiter
	public const string pToken = "PPL";	// person token
	public const string aToken = "ACT"; // action token

	/**
	 * This function, for each sentence into the sentences arrayList, converts
	 * any tokens it sees with the appropriate activities/people. It should ideally,
	 * when encountering a token, grab from either getRandomPerson() or getRandomAction(),
	 * and replace that bit with the random string it grabbed.
	 * 
	 * Sample input:
	 *    I encountered $$PPL$$ when I was $$ACT$$ yesterday.
	 *    They were with $$PPL$$, which surprised me so!
	 *    I quickly went home to $$ACT$$.
	 * Sample output:
	 * 	  I encountered Obama when I was wanking it yesterday.
	 * 	  They were with Donald Trump, which surprised me so!
	 *    I quickly went home to snuggle my dakimakura.
	 * 
	 * Of course, it's a void function, so what this really does is overwrite the
	 * item in the ArrayList sentences.
	 */
	public void replaceTokensInSentences() {
		// CODE HERE
		TextAsset passageline = Resources.Load ("Passages/passages/" + sentences) as TextAsset;
		string newpassage = passageline.text;
		string[] stringSeparators = new string[] { "$$" };
		string[] fullpassage = newpassage.Split(stringSeparators, System.StringSplitOptions.None); 
		for (int x = 0; x < fullpassage.Length; x++)
		{
			if (fullpassage[x] == "PPL")
			{
				fullpassage[x] = getRandomPerson();
			}
			else if (fullpassage[x] == "ACT")
			{
				fullpassage[x] = getRandomAction();
			}

		}
		for (int x = 0; x < fullpassage.Length; x++)
		{
			System.Console.Write(fullpassage[x]);
		}
	}

	public string getRandomPerson() {
		return "foo";
	}

	public string getRandomAction() {
		return "bar";
	}
		
}
