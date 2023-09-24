using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Оружие
{
    class Bullet
    {
        public Bullet(int damage) 
        { 
            if (damage <= 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            Damage = damage;
        }

        public int Damage { get; private set; }
    }

    class Weapon
    {
        private Queue<Bullet> _bullets;

        public Weapon(Queue<Bullet> bullets)
        {
            if (bullets is null)
                throw new ArgumentNullException(nameof(bullets));

            _bullets = bullets;
        }

        public bool CanFire => _bullets.Count > 0;

        public void Fire(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player));

            if (CanFire == false)
                throw new InvalidOperationException();

            Bullet bullet = _bullets.Dequeue();

            player.TakeDamage(bullet.Damage);
        }
    }

    class Player
    {
        private int _health;

        public Player(int health)
        {
            if (_health <= 0)
                throw new ArgumentOutOfRangeException(nameof(health));

            _health = health;
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            _health -= damage;
        }
    }

    class Bot
    {
        private Weapon _weapon;

        public Bot(Weapon weapon)
        {
            if(_weapon is null)
                throw new ArgumentNullException(nameof(weapon));

            _weapon = weapon;
        }

        public void OnSeePlayer(Player player)
        {
            if (player is null)
                throw new ArgumentNullException(nameof(player));

            _weapon.Fire(player);
        }
    }
}
