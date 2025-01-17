using System.Collections.Generic;
using Civ2engine.Enums;
using Civ2engine.Improvements;
using Civ2engine.Production;

namespace Civ2engine
{
    public class AIPlayer : IPlayer
    {
        private readonly DifficultyType _level;

        public AIPlayer(DifficultyType level)
        {
            _level = level;
        }

        public void WeLoveTheKingCanceled(City city)
        {
        }

        public void CivilDisorder(City city)
        {
        }

        public void OrderRestored(City city)
        {
        }

        public void WeLoveTheKingStarted(City city)
        {
        }

        public void CantMaintain(City city, Improvement cityImprovement)
        {
        }

        public void SelectNewAdvance(Game game, Civilization activeCiv, IList<int> researchPossibilities)
        {
        }

        public void CantProduce(City city, ProductionOrder newItem)
        {
        }

        public void CityProductionComplete(City city)
        {
            
        }
    }
}