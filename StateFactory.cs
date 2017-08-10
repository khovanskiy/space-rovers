using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class StateFactory
    {
        private StateFactory()
        {
        }
        public static State create(String name)
        {
            switch (name)
            {
                case "null":
                    {
                        return new GameNullState();
                    }
                case "intro":
                    {
                        return new GameIntroState();
                    }
                case "menu":
                    {
                        return new GameMenuState();
                    }
                case "equipment":
                    {
                        return new PlayerEquipment();
                    }
                case "options":
                    {
                        return new GameOptionsState();
                    }
                case "new_game":
                    {
                        return new NewGameState();
                    }
                case "generate":
                    {
                        return new GenerateWorldState();
                    }
                case "test":
                    {
                        return new GameTestState();
                    }
                case "gameplay":
                    {
                        return new GameplayState();
                    }
                case "pause":
                    {
                        return new GamePauseState();
                    }
                case "exit":
                    {
                        return new GameExitState();
                    }
            }
            throw new Exception("error");
        }
    }
}
