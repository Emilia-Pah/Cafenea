using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe
{
    public class GestiuneCafenea
    {
        public enum TipBautura
        {
            Espresso,
            Cappuccino,
            Latte,
            Americano,
            Ceai,
            Suc,
            Apa
        }

        //enumerare pentru starile comenzilor
        public enum StareComanda
        {
            Plasata,
            InPregatire,
            Gata,
            Servita,
            Anulata
        }
    }
}
