using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Civ2engine.Production;
using Civ2engine.Statistics;

namespace Civ2engine.Advances
{
    public static class AdvanceFunctions
    {
        private static AdvanceResearch[] _researched;

        private static int _MapSizeAdjustment;
        
        public static void SetupTech(this Game game)
        {
            _researched = game.Rules.Advances.OrderBy(a=>a.Index).Select(a=> new AdvanceResearch()).ToArray();
            
            foreach (var civilization in game.AllCivilizations)
            {
                for (var index = 0; index < civilization.Advances.Length; index++)
                {
                    if (civilization.Advances[index])
                    {
                        _researched[index].DiscoveredBy = civilization.Id;
                    }
                }
            }

            _MapSizeAdjustment = game.TotalMapArea / 1000;
            
            ProductionPossibilities.InitializeProductionLists(game.AllCivilizations, game.Rules.ProductionItems);
            Power.CalculatePowerRatings(game);
        }
        
        public static bool HasAdvanceBeenDiscovered(this Game game, int advanceIndex, int byCiv = -1)
        {
            var research = _researched[advanceIndex];
            if (byCiv > -1)
            {
                return research.Discovered && game.AllCivilizations[byCiv].Advances[advanceIndex];
            }

            return research.Discovered;
        }

        public static void GiveAdvance(this Game game, int advanceIndex, int targetCiv)
        {
            var research = _researched[advanceIndex];
            var civilization = game.AllCivilizations[targetCiv];
            if(civilization.Advances[advanceIndex]) return;
            
            //TODO: here we'd look for a lua script to check for effeccts
            
            //TODO: check for default effect

            if (!research.Discovered)
            {
                research.DiscoveredBy = targetCiv;
                game.History.AdvanceDiscovered(advanceIndex, targetCiv);
            }

            if (civilization.ReseachingAdvance == advanceIndex)
            {
                civilization.ReseachingAdvance = -1;
            }

            civilization.Advances[advanceIndex] = true;
            ProductionPossibilities.AddItems(targetCiv,
                game.Rules.ProductionItems.Where(i => i.RequiredTech == advanceIndex && i.CanBuild(targetCiv)));
            ProductionPossibilities.RemoveItems(targetCiv, game.Rules.ProductionItems.Where(o => o.ExpiresTech == advanceIndex));
        }

        public static int TotalAdvances(this Game game, int targetCiv)
        {
            return game.AllCivilizations[targetCiv].Advances.Count(a => a);
        }

        /// <summary>
        ///  I'm not sure if this formula is correct I've just grabed if from https://forums.civfanatics.com/threads/tips-tricks-for-new-players.96725/
        /// </summary>
        /// <param name="game"></param>
        /// <param name="civ"></param>
        /// <returns></returns>
        public static int CalculateScienceCost(Game game, Civilization civ)
        {
            if (civ.ReseachingAdvance == -1) return -1;
            var techParadigm = game.Rules.Cosmic.TechParadigm;
            var ourAdvances = TotalAdvances(game, civ.Id);
            var keyCivAdvances = TotalAdvances(game, civ.PowerRank);
            var techLead = (ourAdvances - keyCivAdvances) / 3;
            var baseCost = techParadigm + techLead;

            if (ourAdvances > 20)
            {
                baseCost += _MapSizeAdjustment;
            }

            return baseCost * ourAdvances;

        }

        public static IList<int> CalculateAvailableResearch(Game game, Civilization activeCiv)
        {
            throw new System.NotImplementedException();
        }
    }
}