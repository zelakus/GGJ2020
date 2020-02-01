using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NPC_Dialog
{
    class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;

        public void Trigger()
        {
            DialogueManager.StartDialogue(dialogue);
        }
    }
}
