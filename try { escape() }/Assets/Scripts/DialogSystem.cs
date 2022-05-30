// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Text;
// using DefaultNamespace;
// using TMPro;
// using UnityEngine;
// public class DialogSystem : MonoBehaviour
// {
//     private TextState textState;
//
//     void Start()
//     {
//     }
//
//
//     void Update()
//     {
//         
//         
//         textState ??= new TextState(GetComponent<TextMeshProUGUI>()); // а по другому никак(
//
//        
//
//         textState.Update();
//
//
//     }
// }
//
// public class TextState
// {
//     public bool onSaying = false;
//     private int idx;
//     private string phrase;
//     private StringBuilder stringBuilder;
//     private TextMeshProUGUI textMesh;
//     private Timer delayTimer;
//
//     public TextState(TextMeshProUGUI textMesh)
//     {
//         this.textMesh = textMesh;
//         delayTimer = new Timer();
//     }
//
//     public void SayPhrase(string phrase)
//     {
//         onSaying = true;
//         this.phrase = phrase;
//         stringBuilder = new StringBuilder();
//         idx = 0;
//     }
//
//     public void Update()
//     {
//         if (onSaying)
//         {
//             delayTimer.Update(Time.deltaTime);
//             if (delayTimer.Seconds < 0.05f) return;
//             
//             delayTimer.Reset();
//             stringBuilder.Append(phrase[idx]);
//             textMesh.text = stringBuilder.ToString();
//
//             idx++;
//             if (idx == phrase.Length) onSaying = false;
//         }
//     }
// }