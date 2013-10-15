using System;
using System.IO;
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
using vgpc_tower_defense.Config;

namespace vgpc_tower_defense.GameObjects
{


    
public class Tower : DrawableGameObject
{


//sounds
protected SoundEffect SoundShoot;   /*SoundEffect to be played when the tower shoots*/
protected SoundEffect SoundUpgrade; /*SoundEffect, if any, to be played when tower is upgraded*/
protected SoundEffect SoundBuild;   /*SoundEffect to be played when tower is built*/

//tower level 
protected int MaxTowerLevel; /* The maximum allowed level of tower*/

//projectiles
protected Texture2D TextureProjectile; /*The texture of the tower's projectile*/

//Weapon Numbers


protected float CurrentWeaponDamage;            /*Starting weapon damage of tower*/
protected float CurrentWeaponAreaOfEffect;      /*Starting weapon area of effect of the tower's weapon */
protected float CurrentWeaponAttacksPerSecond;  /*Starting weapon attacks per second*/
protected float CurrentWeaponRange;             /*Starting weapon range of the weapon*/

//Some towers may cause various effects, such as slow or damage over time. They will just be strings and will be copied over to mob
// The mob will process it's own status effects
protected List<Common.status_effect> StatusEffects;

protected float DamageGainedPerLevel;           /*The amount of damage gained per level*/
protected float AreaOfEffectGainedPerLevel;     /*The amount of attacks per second gained per level*/
protected float AttacksPerSecondGainedPerLevel; /*The amount of weapon ranged gained per level*/
protected float WeaponRangeGainedPerLevel;      /*The speed of the tower's projectiles*/

public int CostToBuild;                     /*Cost to build this tower*/
protected int CurrentCostToUpgrade;         /*Initial cost to upgrade this tower*/
protected int UpgradeCostIncreasePerLevel;  /*The amount the upgrade cost increased each level*/
        
protected float ProjectileSpeed;            /*The speed of this tower's projectiles*/
protected int MaxProjectiles;               /*The maximum amount of projectiles this tower can have active at any given time*/
protected List<Projectile> Projectiles;     /*A list that holds the projectiles associated with this tower*/ 
protected TimeSpan WeaponShootTimer;        /*A TimeSpan that is used as this towers weapon attack timer*/



protected bool is_point_blank_area_damage_tower;/*True if this tower is a point-blank area effect tower, false if it is a ranged single mob attack tower*/

public bool IsDisabled;     /*Flag that is true if tower is disable and thus not upgrade, false if tower is active and is updated*/
int current_tower_level;    /*The current level of this tower*/
        
        
        
//constructor
public Tower(Texture2D defaultTexture, Texture2D textureProjectile)
    : base(defaultTexture)
{

    this.Intialize();
            
    //sounds
    SoundShoot = null;
    SoundUpgrade = null;
    SoundBuild = null;

    //textures
    TextureProjectile = textureProjectile;

    //weapon metrics       
    CurrentWeaponDamage = 10;
    CurrentWeaponAreaOfEffect = 10;
    CurrentWeaponAttacksPerSecond = 1;
    CurrentWeaponRange = 10000;

    DamageGainedPerLevel = 10;
    AreaOfEffectGainedPerLevel = 0;
    AttacksPerSecondGainedPerLevel = 0;
    WeaponRangeGainedPerLevel = 20;

    ProjectileSpeed = 25;
    MaxProjectiles = 20;

    //build and upgrade
    CostToBuild = 1;
    CurrentCostToUpgrade = 1;
    MaxTowerLevel = 3;

    Scale = .8f;
    }



/// <summary>
/// Constructor used to create a tower from a Json Config File. Parameter is the name of the Json tower config file which is assumed to be in /definitions/towers/
/// </summary>
/// <param name="jsonConfigFile"></param>
public Tower(string jsonConfigFile, Managers.AssetManager assetManager)
    : base(null)
{
    this.Intialize();
    // SetVarsFromConfigTowerVars(TowerConfig.GetTowerConfigFromJsonFile(jsonConfigFile), assetManager);

}

protected virtual void SetVarsFromConfigVars(ConfigTowerVars configVars, Managers.AssetManager assetManager)
{
    //sounds
    //protected SoundEffect SoundShoot;   /*SoundEffect to be played when the tower shoots*/
    if(configVars.SoundShoot != "")
    {
        this.SoundShoot = assetManager.LoadedSounds[configVars.SoundShoot];
    }
    //protected SoundEffect SoundUpgrade; /*SoundEffect, if any, to be played when tower is upgraded*/
    if (configVars.SoundUpgrade != "")
    {
        this.SoundUpgrade = assetManager.LoadedSounds[configVars.SoundUpgrade];
    }
    //protected SoundEffect SoundBuild;   /*SoundEffect to be played when tower is built*/
    if (configVars.SoundBuild != "")
    {
        this.SoundBuild = assetManager.LoadedSounds[configVars.SoundBuild];
    }

    //tower level 
    //protected int MaxTowerLevel; /* The maximum allowed level of tower*/
    this.MaxTowerLevel = configVars.MaxTowerLevel;

    //projectiles
    //protected Texture2D TextureProjectile; /*The texture of the tower's projectile*/
    this.TextureProjectile = assetManager.LoadedSprites[configVars.TextureProjectile];

    //Weapon Numbers


    //protected float CurrentWeaponDamage;            /*Starting weapon damage of tower*/
    this.CurrentWeaponDamage = configVars.CurrentWeaponDamage;
    //protected float CurrentWeaponAreaOfEffect;      /*Starting weapon area of effect of the tower's weapon */
    this.CurrentWeaponAreaOfEffect = configVars.CurrentWeaponAreaOfEffect;
    //protected float CurrentWeaponAttacksPerSecond;  /*Starting weapon attacks per second*/
    this.CurrentWeaponAttacksPerSecond = configVars.CurrentWeaponAttacksPerSecond;
    //protected float CurrentWeaponRange;             /*Starting weapon range of the weapon*/
    this.CurrentWeaponRange = configVars.CurrentWeaponRange;

    //Some towers may cause various effects, such as slow or damage over time. They will just be strings and will be copied over to mob
    // The mob will process it's own status effects
    //protected List<Common.status_effect> StatusEffects;
    this.StatusEffects = 

    //protected float DamageGainedPerLevel;           /*The amount of damage gained per level*/
    //protected float AreaOfEffectGainedPerLevel;     /*The amount of attacks per second gained per level*/
    //protected float AttacksPerSecondGainedPerLevel; /*The amount of weapon ranged gained per level*/
    //protected float WeaponRangeGainedPerLevel;      /*The speed of the tower's projectiles*/

    //public int CostToBuild;                     /*Cost to build this tower*/
    //protected int CurrentCostToUpgrade;         /*Initial cost to upgrade this tower*/
    //protected int UpgradeCostIncreasePerLevel;  /*The amount the upgrade cost increased each level*/
        
    //protected float ProjectileSpeed;            /*The speed of this tower's projectiles*/
    //protected int MaxProjectiles;               /*The maximum amount of projectiles this tower can have active at any given time*/
    //protected List<Projectile> Projectiles;     /*A list that holds the projectiles associated with this tower*/ 
    //protected TimeSpan WeaponShootTimer;        /*A TimeSpan that is used as this towers weapon attack timer*/



    //protected bool is_point_blank_area_damage_tower;/*True if this tower is a point-blank area effect tower, false if it is a ranged single mob attack tower*/

    //public bool IsDisabled;     /*Flag that is true if tower is disable and thus not upgrade, false if tower is active and is updated*/
    //int current_tower_level;    /*The current level of this tower*/
}
        

/// <summary>
/// Initializes reference-type class properties and fields.
/// </summary>
protected virtual void Intialize()
{
    Projectiles = new List<Projectile>();
    StatusEffects = new List<Common.status_effect>();
    WeaponShootTimer = TimeSpan.Zero;

    for (int i = 0; i < MaxProjectiles; i++)
    {
        Projectile NewProjectile = new Projectile(this.TextureProjectile);
        NewProjectile.speed = ProjectileSpeed;
        Projectiles.Add(NewProjectile);
    }
}

        
public virtual bool TowerConfigReader(String configString)
{
    return false;
}

        
public void load_projectile_texture(Texture2D projectile_texture)
{
    TextureProjectile = projectile_texture;
}


public void CreateExampleJsonTowerConfigFile()
{
         
    //string JsonFromFile = File.ReadAllText("JsonInput.txt");
    //TowerConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<TowerConfig>(JsonFromFile);
}



protected virtual bool level_up_tower()
{
    if (globals.PlayerCash >= CurrentCostToUpgrade && (current_tower_level < MaxTowerLevel))
    {
        current_tower_level += 1;
        globals.PlayerCash -= CurrentCostToUpgrade;

        CurrentWeaponDamage += DamageGainedPerLevel;
        CurrentWeaponAreaOfEffect += AreaOfEffectGainedPerLevel;
        CurrentWeaponAttacksPerSecond += AttacksPerSecondGainedPerLevel;
        CurrentWeaponRange += WeaponRangeGainedPerLevel;
        return true;
    }
    else
    {
        return false;
    }
}



protected virtual void UpdateProjectiles(GameTime gameTime)
{
    foreach (Projectile Projectile in Projectiles)
    {
        if (Projectile.IsActive)
        {

            //check if projectile is off the screen, if so, mark as inactive
            if (!Util.vgpc_math.DoesRectangleContainVector(globals.viewport_rectangle, Projectile.Position))
            {
                Projectile.IsActive = false;
                continue;
            }

            Rectangle ProjectileBoundingBox = Projectile.GetBoundingRectangle();

            //check if projectile has collided with a mob
            foreach (EnemyMob mob in globals.Mobs)
            {

                Rectangle mob_bounding_box = mob.GetBoundingRectangle();

                //if mob is 
                if (mob_bounding_box.Intersects(ProjectileBoundingBox))
                {
                    Projectile.IsActive = false;
                    DamageAndAffectMob(mob);
                    break;
                }
            }

            //finally,  update the position of each active projectile
            Projectile.update_position();

        }
    }
}

public virtual void Update(GameTime gameTime)
{
    UpdateWeapon(gameTime);
    UpdateProjectiles(gameTime);
    UpdateAnimation(gameTime);

}

protected virtual void UpdateAnimation(GameTime game_time)
{
    //todo
}

protected virtual void FireAtClosestMob(List<EnemyMob> enemyMobs)
{
    if (!IsDisabled && (enemyMobs.Count > 0))
    {

        List<Util.vgpc_math.FindNearestMobResult> Results = Util.vgpc_math.FindNearestMob(this.Position, enemyMobs);
        if (1 != Results.Count)
        {
            return;
        }
        if (Results[0].Distance > this.CurrentWeaponRange)
        {
            return;
        }


        for (int i = 0; i < this.Projectiles.Count; i++)
        {
            if (!Projectiles[i].IsActive)
            {
                Projectiles[i].IsActive = true;

                //create a vector from this tower to the nearest mob
                Projectiles[i].Velocity = Util.vgpc_math.create_target_unit_vector(this.Position, Results[0].EnemyMob.Position);

                //since the function creates a unit vecor(lenth, which in this case is the speed portion of the vector), we need to multiply the vectoy
                //by the turrent projectile speed
                Projectiles[i].Velocity *= ProjectileSpeed;

                //set the projectile position equal to this tower's position
                Projectiles[i].Position = this.Position;
                break;
            }
        }
    }
}

//
protected virtual void DamageAndAffectMob(EnemyMob mob)
{
    mob.damage_me((int)CurrentWeaponDamage);
    mob.AddStatusEffects(StatusEffects);

}


protected virtual void fire_point_blank_weapon(List<EnemyMob> enemy_mobs)
{
    if (!IsDisabled)
    {
        foreach (EnemyMob mob in enemy_mobs)
        {
            float distance = Util.vgpc_math.GetDistanceBetweenTwoVectors(this.Position, mob.Position);
            if (distance <= CurrentWeaponRange)
            {
                DamageAndAffectMob(mob);
            }
        }
    }
}


protected virtual void UpdateWeapon(GameTime game_time)
{
    WeaponShootTimer += game_time.ElapsedGameTime;

    TimeSpan attack_interval = new TimeSpan(0, 0, 0, 0, (int)(1000 / CurrentWeaponAttacksPerSecond));

    if (WeaponShootTimer > attack_interval)
    {
        WeaponShootTimer = TimeSpan.Zero;
        if (!is_point_blank_area_damage_tower)
        {
            FireAtClosestMob(globals.Mobs);
        }
        else
        {
            fire_point_blank_weapon(globals.Mobs);
        }

    }
}

//the base gameobject class just draws the default texture, but we the tower class to draw all if it's associated projectiles as well
//thus we will override the base draw function and add this functionality
public override void draw(SpriteBatch spriteBatch)
{
    base.draw(spriteBatch);
    foreach (Projectile projectile in this.Projectiles)
    {
        projectile.draw(spriteBatch, this.TextureProjectile);
    }
}
}
}
