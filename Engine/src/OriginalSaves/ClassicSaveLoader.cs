using System.Collections.Generic;

namespace Civ2engine
{
    public static class ClassicSaveLoader
    {
        public static void LoadSave(Ruleset ruleset, string saveFileName, Rules rules)
        {
            GameData gameData = Read.ReadSAVFile(ruleset.FolderPath, saveFileName);

            var hydrator = new LoadedGameObjects(rules, gameData);
            
            // Make an instance of a new game
            Game.Create(rules, gameData, hydrator, ruleset);
        }
    }
}