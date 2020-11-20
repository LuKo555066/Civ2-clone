﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoskusCiv2.Enums;

namespace PoskusCiv2.Units
{
    internal class Partisans : BaseUnit
    {
        public Partisans() : base(50, 4, 4, 2, 1, 1)
        {
            Type = UnitType.Partisans;
            GAS = UnitGAS.Air;
            Name = "Partisans";
        }
    }
}