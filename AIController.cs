using System.Collections.Generic;

namespace Game
{
    public class AIController : Controller
    {
        Player player;

        public AIController(Player player)
            : base(player)
        {
            this.player = player;
        }

        private void aiMomentary()
        {
            if (!player.ship.canMove())
            {
                Ships.Engine engine = player.ship.findBestEngine();
                if (engine != null)
                {
                    player.ship.removeComponent(engine);
                    player.ship.setEngine(engine);
                }
            }
            while (player.ship.cannons.Count < player.ship.getHull().number_of_cannons)
            {
                Ships.Cannon cannon = player.ship.findBestCannon();
                if (cannon == null)
                {
                    break;
                }
                player.ship.removeComponent(cannon);
                player.ship.addCannon(cannon);
            }
            for (int i = 0; i < player.attack.Count; i++)
            {
                player.ship.shot(player.attack[i]);
            }
        }

        private void findBestToAttack(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (player.id_frac != players[i].id_frac)
                {
                    player.attack.Add(players[i]);
                }
            }
        }

        private void aiLong(List<Player> players)
        {
            if (player.ship.canMove())
            {
                player.destination.x = Game.random.Next(-700, 700);
                player.destination.y = Game.random.Next(-700, 700);
                //findBestToAttack(players);
            }
        }

        public void nextTick(float dt)
        {
            if (player.ship.canMove())
            {
                player.ship.turnToPoint(player.destination, dt);
                if (!player.ship.isDestinationReached(player.destination, dt))
                {
                    player.ship.moveForward(dt);
                }
            }
        }

        public void move()
        {
            player.destination.x = Game.random.Next(-700, 700);
            player.destination.y = Game.random.Next(-700, 700);
        }

        public void nextStep(List<Player> players)
        {
            aiMomentary();
            aiLong(players);
        }
    }
}