using Galaxies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Controllers
{

    static class CombatAIController
    {

        private static EnemyShip Ai { get; set; }

        public static void Start(EnemyShip ai)
        {
            Ai = ai;
        }

        public static void Turn()
        {

        }

    }

}
