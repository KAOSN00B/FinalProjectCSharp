using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class NPC
    {

        private List<string> Dialogues;
        private Random rng = new Random();

        public NPC()
        {
            Dialogues = new List<string>
        {
            "Town hasn't been the same since.... the incident",
            "Back in my day adventurers had some real guts.",
            "If you're heading to the forest, keep your eyes open theres dangerous wolves and Hornets there.",
            "Be careful of Dire wolves. They're like regular wolves but dire.",
            "Heard a monster howling last night… gave me chills.",
            "Rumor has it that the giant castle is the source of all these monsters showing up everywhere"
        };
        }

        public void Speak()
        {
            int index = rng.Next(Dialogues.Count);
            Console.WriteLine($"Town Local: \"{Dialogues[index]}\"\n");
        }
    }

}
