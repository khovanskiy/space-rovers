using System;

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