using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektGame
{
    class Wizard : Hero, ISpecialAttack
    {
        public Wizard(string name, int fullhp, Colors color) : base(name, fullhp, color)
        {
        }

        public override void DefoultAttack(Hero hero)
        {
            int hp = rnd.Next(100, 151);
            hero.ActualHP -= hp;
            Console.WriteLine($"\nGracz {Name} zadał {hp} punktów obrażeń graczowi {hero.Name}.");
        }

        public override void Heal()
        {
            int hp = rnd.Next(100, 201);
            ActualHP += hp;
            Console.WriteLine($"\nGracz {Name} uzdrowił się za {hp} punktów życia.");
        }

        public void SpecialAttack(Hero hero)
        {
            int hp = rnd.Next(250, 301);
            hero.ActualHP -= hp;
            Console.WriteLine($"\nAtak specialny Gracz {Name} zadał {hp} punktów obrażeń graczowi {hero.Name}.");
        }
    }
}
