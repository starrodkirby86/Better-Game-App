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

	HybridDictionary firstNameDb = new HybridDictionary();
	HybridDictionary lastNameDb = new HybridDictionary();

	public RandomNames() {
		fillFirstNameDatabase ();
		fillLastNameDatabase ();
	}

	public string getName(firstNameList m, lastNameList n) {
		// Based on the enumerator, we're going to pick a name from the txt file thrown at us.
		// Randomly, of course. Because that's the beauty of it all.
		TextAsset firstNameList = Resources.Load ("NameLists/firstNames/" + firstNameDb[(int)m]) as TextAsset;
		//Debug.Log ("NameLists/" + nameDb[(int)n]);
		string[] lines = firstNameList.text.Split ('\n');
		string firstName = lines[ Random.Range (0, lines.Length)];

		// And now the last name.
		TextAsset lastNameList = Resources.Load ("NameLists/lastNames/" + lastNameDb[(int)n]) as TextAsset;
		//Debug.Log ("NameLists/" + nameDb[(int)n]);
		lines = lastNameList.text.Split ('\n');
		string lastName = lines[ Random.Range (0, lines.Length)];
		return firstName + " " + lastName;
	}

	public string getFirstName(firstNameList m) {
		// Based on the enumerator, we're going to pick a name from the txt file thrown at us.
		// Randomly, of course. Because that's the beauty of it all.
		TextAsset firstNameList = Resources.Load ("NameLists/firstNames/" + firstNameDb[(int)m]) as TextAsset;
		//Debug.Log ("NameLists/" + nameDb[(int)n]);
		string[] lines = firstNameList.text.Split ('\n');
		string firstName = lines[ Random.Range (0, lines.Length)];
		return firstName;
	}

	// Initialization function. Basically, we can recognize the existence of txt files here.
	public void fillFirstNameDatabase() {
		int counter = 0;
		firstNameDb.Add ( counter++, "firstNames" ); 
		firstNameDb.Add ( counter++, "generic" );
	}

	public void fillLastNameDatabase() {
		int counter = 0;
		lastNameDb.Add ( counter++, "lastNames" ); 
	}
}

/**
 * What list of names should we pick from?
 * Should correspond to the fillDatabase(), and vice versa.
 */
public enum firstNameList {
	firstNames,
	generic
};

/**
 * What list of names should we pick from?
 * Should correspond to the fillDatabase(), and vice versa.
 */
public enum lastNameList {
	lastNames
};