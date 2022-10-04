using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slagalica_97_2018
{
    public class ScoreTip
    {
        public string nicnkame;
        public int score;
        public int potezi;
        public int vreme;

        public ScoreTip(string nickname, int score , int potezi , int vreme)
        {
            this.nicnkame = nickname;
            this.score = score;
            this.potezi = potezi;
            this.vreme = vreme;
        }
    }
}
