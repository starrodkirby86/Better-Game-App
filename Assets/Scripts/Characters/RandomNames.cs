using UnityEngine;
using System.Collections;
using System.Collections.Specialized;


/**
 * This gives a game a lot of heart.
 * We can create this cute random name generator that will
 * basically generate us some awesome player names for us.
 * 
 * Super easy.
 */
public class RandomNames {

	HybridDictionary nameDb = new HybridDictionary();

	public RandomNames() {
		fillDatabase ();
	}

	public string getName(nameList n) {
		// Based on the enumerator, we're going to pick a name from the txt file thrown at us.
		// Randomly, of course. Because that's the beauty of it all.
		TextAsset nameList = Resources.Load ("NameLists/" + nameDb[(int)n]) as TextAsset;
		//Debug.Log ("NameLists/" + nameDb[(int)n]);
		string[] lines = nameList.text.Split ('\n');
		return lines[ Random.Range (0, lines.Length)];
	}

	// Initialization function. Basically, we can recognize the existence of txt files here.
	public void fillDatabase() {
		int counter = 0;
		nameDb.Add ( counter++, "first-names" ); 
		nameDb.Add ( counter++, "funny" );
	}
}

/**
 * What list of names should we pick from?
 * Should correspond to the fillDatabase(), and vice versa.
 */
public enum nameList {
	firstNames,
	funny
};