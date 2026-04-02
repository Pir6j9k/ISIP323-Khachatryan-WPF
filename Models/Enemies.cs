using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP323_Khachatryan_WPF.Models
{
    public abstract class Enemy
    {
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public int MaxHP { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        protected Random random;

        public Enemy(string name, int hp, int attack, int defense)
        {
            Name = name; HP = hp; MaxHP = hp; Attack = attack; Defense = defense;
            random = new Random();
        }

        public abstract void PerformAttack(Player player, Action<string> log);
        public abstract void SpecialAbility(Player player, Action<string> log);

        public void TakeDamage(int damage, Action<string> log)
        {
            HP = Math.Max(0, HP - damage);
            log($"{Name} получил {damage} урона. Осталось HP: {HP}");
        }

        public bool IsAlive() => HP > 0;
        public virtual string GetInfo() => $"{Name} (HP: {HP}/{MaxHP}, Атака: {Attack}, Защита: {Defense})";
    }
    public class Goblin : Enemy
    {
        public double CritChance { get; private set; }
        public Goblin(string name, int hp, int attack, int defense, double critChance)
            : base(name, hp, attack, defense) => CritChance = critChance;
        public Goblin() : this("Гоблин", 30, 8, 3, 0.2) { }

        public override void PerformAttack(Player player, Action<string> log)
        {
            int damage = Attack;
            if (random.NextDouble() < CritChance)
            {
                damage *= 2;
                log($"{Name} наносит критический удар!");
            }
            player.TakeDamage(damage, log);
        }
        public override void SpecialAbility(Player player, Action<string> log) { }
        public override string GetInfo() => base.GetInfo() + $", Крит: {CritChance * 100}%";
    }

    public class Skeleton : Enemy
    {
        public Skeleton(string name, int hp, int attack, int defense) : base(name, hp, attack, defense) { }
        public Skeleton() : this("Скелет", 25, 10, 2) { }

        public override void PerformAttack(Player player, Action<string> log)
        {
            log($"{Name} игнорирует вашу защиту!");
            player.TakeDamage(Attack, log, ignoreArmor: true);
        }
        public override void SpecialAbility(Player player, Action<string> log) { }
    }

    public class Mage : Enemy
    {
        public double FreezeChance { get; private set; }
        public Mage(string name, int hp, int attack, int defense, double freezeChance)
            : base(name, hp, attack, defense) => FreezeChance = freezeChance;
        public Mage() : this("Маг", 20, 12, 1, 0.25) { }

        public override void PerformAttack(Player player, Action<string> log)
        {
            player.TakeDamage(Attack, log);
        }

        public override void SpecialAbility(Player player, Action<string> log)
        {
            if (random.NextDouble() < FreezeChance)
            {
                player.IsFrozen = true;
                log($"{Name} замораживает вас! Вы пропустите следующий ход.");
            }
        }
        public override string GetInfo() => base.GetInfo() + $", Заморозка: {FreezeChance * 100}%";
    }
    public class VVG : Goblin
    {
        public VVG() : base("ВВГ", 60, 12, 4, 0.3) { }
    }

    public class Kovalsky : Skeleton
    {
        public Kovalsky() : base("Ковальский", 69, 13, 3) { }
    }

    public class ArchmageCPP : Mage
    {
        public ArchmageCPP() : base("Архимаг C++", 36, 19, 2, 0.35) { }
    }

    public class PestovC : Skeleton
    {
        public double FreezeChance { get; private set; }
        public PestovC() : base("Пестов С--", 33, 18, 1)
        {
            FreezeChance = 0.3;
        }

        public override void PerformAttack(Player player, Action<string> log)
        {
            log($"{Name} пробивает вашу броню насквозь!");
            player.TakeDamage(Attack, log, ignoreArmor: true);
        }

        public override void SpecialAbility(Player player, Action<string> log)
        {
            if (random.NextDouble() < FreezeChance)
            {
                player.IsFrozen = true;
                log($"{Name} замораживает вас своей ледяной магией!");
            }
        }
    }
}
