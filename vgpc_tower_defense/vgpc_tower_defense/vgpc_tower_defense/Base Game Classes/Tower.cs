using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.Diagnostics;

namespace vgpc_tower_defense.GameObjects
{
    public class Tower : DrawableGameObject
    {

        //constructor
        public Tower(Texture2D default_texture, Texture2D projectile_texture)
            : base(default_texture)
        {
            //sounds
            sound_shoot = null;
            sound_upgrade = null;
            sound_build = null;

            //textures
            texture_projectile = projectile_texture;

            //weapon metrics       
            current_weapon_damage = 1;
            current_weapon_area_of_effect = 10;
            current_weapon_attacks_per_second = 1;
            current_weapon_range = 10000;

            damage_gained_per_level = 10;
            area_gained_per_level = 0;
            attacks_per_second_gained_per_level = 0;
            weapon_range_gained_per_level = 20;

            projectile_speed = 25;
            max_projectiles = 20;

            //build and upgrade
            cost_to_build = 1;
            current_cost_to_upgrade = 1;
            max_tower_level = 3;

            //Initializations
            projectiles = new List<Projectile>();
            status_effects = new List<Common.status_effect>();
            weapon_shoot_timer = TimeSpan.Zero;

            for (int i = 0; i < max_projectiles; i++)
            {
                Projectile new_projectile = new Projectile(this.texture_projectile);
                new_projectile.speed = projectile_speed;
                projectiles.Add(new_projectile);
            }
        }

        public Tower(String config_file_path)
            : base(null)
        {

        }



        public void load_projectile_texture(Texture2D projectile_texture)
        {
            texture_projectile = projectile_texture;
        }



        protected virtual bool level_up_tower()
        {
            if (globals.player_cash >= current_cost_to_upgrade && (current_tower_level < max_tower_level))
            {
                current_tower_level += 1;
                globals.player_cash -= current_cost_to_upgrade;

                current_weapon_damage += damage_gained_per_level;
                current_weapon_area_of_effect += area_gained_per_level;
                current_weapon_attacks_per_second += attacks_per_second_gained_per_level;
                current_weapon_range += weapon_range_gained_per_level;
                return true;
            }
            else
            {
                return false;
            }
        }



        protected virtual void update_projectiles(GameTime game_time, Rectangle view_port, List<EnemyMob> enemy_mobs)
        {
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.is_active)
                {

                    //check if projectile is off the screen, if so, mark as inactive
                    if (!Util.vgpc_math.does_rectangle_contain(view_port, projectile.position))
                    {
                        projectile.is_active = false;
                        continue;
                    }

                    Rectangle projectile_bounding_box = new Rectangle((int)projectile.position.X, (int)projectile.position.Y,
                          this.texture_projectile.Width, this.texture_projectile.Height);

                    //check if projectile has collided with a mob
                    foreach (EnemyMob mob in enemy_mobs)
                    {
                        Rectangle mob_bounding_box = new Rectangle((int)mob.position.X, (int)mob.position.Y,
                            this.current_texture.Width, this.current_texture.Height);

                        //if mob is 
                        if (mob_bounding_box.Intersects(projectile_bounding_box))
                        {
                            projectile.is_active = false;
                            damage_and_affect_mop(mob);
                            break;
                        }
                    }

                    //finally,  update the position of each active projectile
                    projectile.update_position();



                }
            }
        }

        public virtual void update_tower(GameTime game_time, List<EnemyMob> enemy_mobs, Rectangle view_port)
        {
            update_weapon_fire(enemy_mobs, game_time);
            update_projectiles(game_time, view_port, enemy_mobs);
            update_animation(game_time);

        }



        protected virtual void update_animation(GameTime game_time)
        {
            //todo
        }

        protected virtual void fire_at_closest_mob(List<EnemyMob> enemy_mobs)
        {
            if (!is_disabled && (enemy_mobs.Count > 0))
            {


                Vector2 target_pos = Util.vgpc_math.find_nearest_mob(this.position, enemy_mobs);
                float distance_to_closest_mob = Util.vgpc_math.get_distance_between(this.position, target_pos);

                if (target_pos != null && (distance_to_closest_mob <= this.current_weapon_range))
                {
                    for (int i = 0; i < this.projectiles.Count; i++)
                    {
                        if (!projectiles[i].is_active)
                        {
                            projectiles[i].is_active = true;


                            //find the nearest mob to this tower



                            //fire away if A) there is any mobs to shoot at And B) they are within the towers weapon range

                            //create a vector from this tower to the nearest mob
                            projectiles[i].velocity = Util.vgpc_math.create_target_unit_vector(this.position, target_pos);

                            //since the function creates a unit vecor(lenth, which in this case is the speed portion of the vector), we need to multiply the vectoy
                            //by the turrent projectile speed
                            projectiles[i].velocity *= projectile_speed;

                            //set the projectile position equal to this tower's position
                            projectiles[i].position = this.position;
                            break;
                        }
                    }
                }
            }
        }

        //
        protected virtual void damage_and_affect_mop(EnemyMob mob)
        {
            mob.damage_me((int)current_weapon_damage);
            mob.add_status_effects(status_effects);

        }




        protected virtual void fire_point_blank_weapon(List<EnemyMob> enemy_mobs)
        {
            if (!is_disabled)
            {
                foreach (EnemyMob mob in enemy_mobs)
                {
                    float distance = Util.vgpc_math.get_distance_between(this.position, mob.position);
                    if (distance <= current_weapon_range)
                    {
                        damage_and_affect_mop(mob);
                    }
                }
            }
        }



        protected virtual void update_weapon_fire(List<EnemyMob> enemy_mobs, GameTime game_time)
        {
            weapon_shoot_timer += game_time.ElapsedGameTime;

            TimeSpan attack_interval = new TimeSpan(0, 0, 0, 0, (int)(1000 / current_weapon_attacks_per_second));

            if (weapon_shoot_timer > attack_interval)
            {
                weapon_shoot_timer = TimeSpan.Zero;
                if (!is_point_blank_area_damage_tower)
                {
                    fire_at_closest_mob(enemy_mobs);
                }
                else
                {
                    fire_point_blank_weapon(enemy_mobs);
                }

            }
        }

        //the base gameobject class just draws the default texture, but we the tower class to draw all if it's associated projectiles as well
        //thus we will override the base draw function and add this functionality
        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            foreach (Projectile projectile in this.projectiles)
            {
                projectile.draw(spriteBatch, this.texture_projectile);
            }
        }





        //sounds
        protected SoundEffect sound_shoot;
        protected SoundEffect sound_upgrade;
        protected SoundEffect sound_build;

        //tower level 
        protected int max_tower_level;




        //projectiles
        protected Texture2D texture_projectile; //texture

        //Weapon Numbers


        protected float current_weapon_damage;
        protected float current_weapon_area_of_effect;
        protected float current_weapon_attacks_per_second;
        protected float current_weapon_range;

        //Some towers may cause various effects, such as slow or damage over time. They will just be strings and will be copied over to mob
        // The mob will process it's own status effects
        protected List<Common.status_effect> status_effects;




        protected float damage_gained_per_level;
        protected float area_gained_per_level;
        protected float attacks_per_second_gained_per_level;
        protected float weapon_range_gained_per_level;

        public int cost_to_build { get; protected set; }

        protected int current_cost_to_upgrade;

        protected float projectile_speed;

        protected int max_projectiles;



        protected List<Projectile> projectiles;

        protected TimeSpan weapon_shoot_timer;

        //tower state variables

        //true if doesn't doesn't shoot projectile but rather damages a point blank area around it
        protected bool is_point_blank_area_damage_tower;
        //true if some effect is present that disables the tower(causes it not to fire)
        public bool is_disabled { get; set; }
        int current_tower_level;



    }
    
}
