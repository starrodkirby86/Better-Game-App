using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/**
 * This class acts as a layer to help provide a GameManager the desired ruleset.
 * It basically keeps the rulesets it knows and then when the RoomManager wants a specific
 * ruleset, it calls upon that. It acts a bit like the RandomNames hybrid dictionary.
 */
public class RuleManager
{

	private Dictionary<int,BaseRuleset> rulesetDictionary = new Dictionary<int,BaseRuleset>();

	public RuleManager ()
	{
		fillDatabase ();
	}

	public void fillDatabase(){
		var counter = 0;
		rulesetDictionary.Add (counter++, new PureRandom());
		rulesetDictionary.Add (counter++, new CellularAutomata());
		rulesetDictionary.Add (counter++, new SemiRandom());
		rulesetDictionary.Add (counter++, new Nazareth());
		rulesetDictionary.Add (counter++, new MoistLucifer());
	}

	public BaseRuleset getRule(Rulesets r) {
		return rulesetDictionary[(int)r];
	}
}

public enum Rulesets {
	PureRandom,
	CellularAutomata,
	SemiRandom,
	Nazareth,
	MoistLucifer
};