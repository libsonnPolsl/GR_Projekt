using System;
using System.Collections.Generic;
using System.Text;

namespace GR_Projekt.Utils.Dialogue
{
    class DialogueItem
    {
        private int id;
        private string content;
        private string character;
        private int nextId;

        public DialogueItem(int id, string content, string character, int nextId)
        {
            this.id = id;
            this.content = content;
            this.character = character;
            this.nextId = nextId;
        }

        public int Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }
        public string Character { get => character; set => character = value; }
        public int NextId { get => nextId; set => nextId = value; }
    }
}
