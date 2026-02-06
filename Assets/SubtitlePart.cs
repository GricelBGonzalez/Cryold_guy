using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlePart : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI _speaker;
	[SerializeField] private TMPro.TextMeshProUGUI _dialogue;

	private void SetText(TMPro.TextMeshProUGUI __textController, string __text) => __textController.text = __text;

	public void Dialogue_Speaking_Joaquin(string __textDialogue)
	{
		SetText(_speaker, "Joaquin" + ":");
		SetText(_dialogue, __textDialogue);
	}
	public void Dialogue_Speaking_Construct(string __textDialogue)
	{
		SetText(_speaker, "Construct" + ":");
		SetText(_dialogue, __textDialogue);
	}
	public void Dialogue_Speaking_PokerPizzaGuy(string __textDialogue)
	{
		SetText(_speaker, "Poker pizza guy" + ":");
		SetText(_dialogue, __textDialogue);
	}
	public void Dialogue_Silence()
	{
		SetText(_speaker, "");
		SetText(_dialogue, "");
	}
}
