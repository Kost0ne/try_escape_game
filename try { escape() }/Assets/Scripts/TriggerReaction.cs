using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DefaultNamespace;
using TMPro;
using UnityEngine;



public class TriggerReaction : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    private TextState textState;
    private Dictionary<string, string> TriggerToPhrase = new Dictionary<string, string>()
    {
        {"GeneratorTrigger", "О госпади, это же генератор!!!"},
        {"BoardTrigger", "Это же доска!!!"},
        {"StepaTrigger", "О, и Стёпа здесь"},
        {"KostyaTrigger", "?????"},
        {"LiftTrigger", "О, лифт! Какое счастье"},
        
    };
    

    // Start is called before the first frame update
    void Start()
    {
        textState = new TextState(textMesh);
    }

    // Update is called once per frame
    void Update()
    {
        textState.Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TriggerToPhrase.TryGetValue(other.name, out var phrase))
            textState.SayPhrase(phrase);
    }
}

public class TextState
{
    public bool onSaying = false;
    private int idx;
    private string phrase;
    private StringBuilder stringBuilder;
    private TextMeshProUGUI textMesh;
    private Timer delayTimer;
    private Timer phraseTimer;

    public TextState(TextMeshProUGUI textMesh)
    {
        this.textMesh = textMesh;
        delayTimer = new Timer();
        phraseTimer = new Timer();
    }

    public void SayPhrase(string phrase)
    {
        onSaying = true;
        this.phrase = phrase;
        stringBuilder = new StringBuilder();
        idx = 0;
    }

    public void Update()
    {
        if (onSaying)
        {
            delayTimer.Update(Time.deltaTime);
            if (delayTimer.Seconds < 0.05f) return;

            delayTimer.Reset();
            stringBuilder.Append(phrase[idx]);
            textMesh.text = stringBuilder.ToString();

            idx++;
            if (idx != phrase.Length) return;
            onSaying = false;
            phraseTimer = new Timer();
            return;
        }

        if (phrase == "") return;
        phraseTimer.Update(Time.deltaTime);
        if (phraseTimer.Seconds < 5) return;

        phrase = "";
        textMesh.text = phrase;
        phraseTimer.Reset();
    }
}