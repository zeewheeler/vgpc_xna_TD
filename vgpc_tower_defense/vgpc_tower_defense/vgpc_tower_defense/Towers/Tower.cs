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


    public string TowerName; /*String to hold the name of this tower*/
    
    //sounds
    protected SoundEffect SoundShoot;   /*SoundEffect to be played when the tower shoots*/
    protected SoundEffect SoundUpgrade; /*SoundEffect, if any, to be played when tower is upgraded*/
    protected SoundEffect SoundBuild;   /*SoundEffect to be played when tower is built*/

    //projectiles
    protected Texture2D TextureProjectile; /*The texture of the tower's projectile*/
    //baseclass holds TextureCurrent which holds the currentTexture of the tower

    //Weapon Numbers
    protected float CurrentWeaponDamage;            /*Starting weapon damage of tower*/
    protected float CurrentWeaponAreaOfEffect;      /*Starting weapon area of effect of the tower's weapon */
    protected float CurrentWeaponAttacksPerSecond;  /*Starting weapon attacks per second*/
    protected float CurrentWeaponRange;             /*Starting weapon range of the weapon*/

    //Some towers may cause various effects, such as slow or damage over time. They will just be strings and will be copied over to mob
    // The mob will process it's own status effects
    protected List<Common.status_effect> StatusEffects;

    protected float DamageGainedPerLevel;           /*The amount of damage gained per level*/
    protected float AttacksPerSecondGainedPerLevel; /*The amount of weapon ranged gained per level*/
    protected float WeaponRangeGainedPerLevel;      /*The speed of the tower's projectiles*/

    //Build Cost and Level Related
    public int CostToBuild;                     /*Cost to build this tower*/
    protected int CurrentCostToUpgrade;         /*Initial cost to upgrade this tower*/
    protected int UpgradeCostIncreasePerLevel;  /*The amount the upgrade cost increased each level*/
    protected int MaxTowerLevel; /* The maximum allowed level of tower*/
        
    protected float ProjectileSpeed;            /*The speed of this tower's projectiles*/
    protected int MaxProjectiles;               /*The maximum amount of projectiles this tower can have active at any given time*/
    public List<Projectile> Projectiles;     /*A list that holds the projectiles associated with this tower*/ 
    protected TimeSpan WeaponShootTimer;        /*A TimeSpan that is used as this towers weapon attack timer*/

    //Base Class holds :  float Scale
    //Base Class holds :  float rotation


    protected bool is_point_blank_area_damage_tower;/*True if this tower is a point-blank area effect tower, false if it is a ranged single mob attack tower*/

    public bool IsDisabled;     /*Flag that is true if tower is disable and thus not upgrade, false if tower is active and is updated*/
    int current_tower_level;    /*The current level of this tower*/
        
        
        
//constructor
public Tower(Texture2D defaultTexture, Texture2D textureProjectile)
    : base(defaultTexture)
{
    //The projectile texture MUST be set before Intialize is called, else it will load a null texture 
    TextureProjectile = textureProjectile;

    this.Intialize();
            
    //sounds
    SoundShoot = null;
    SoundUpgrade = null;
    SoundBuild = null;

  

    //weapon metrics       
    CurrentWeaponDamage = 10;
    CurrentWeaponAreaOfEffect = 10;
    CurrentWeaponAttacksPerSecond = 1;
    CurrentWeaponRange = 10000;

    DamageGainedPerLevel = 10;
    AttacksPerSecondGainedPerLevel = 0;
    WeaponRangeGainedPerLevel = 20;

    ProjectileSpeed = 25;
  
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
    SetVarsFromConfigVars(TowerConfig.GetTowerConfigFromJsonFile(jsonConfigFile), assetManager);

}

/// <summary>
/// Takes a ConfigTowerVars and uses it to initilize the relavent class members
/// </summary>
/// <param name="configVars"></param>
/// <param name="assetManager"></param>
protected virtual void SetVarsFromConfigVars(ConfigTowerVars configVars, Managers.AssetManager assetManager)
{

    //Load Sounds from AssetManager
    if (configVars.SoundShoot != "") //If not empty string, then get appropriate  sound from assetManager
    {
        //this.SoundShoot = assetManager.LoadedSounds[configVars.SoundShoot];
    }

    if (configVars.SoundUpgrade != "")
    {
        //this.SoundUpgrade = assetManager.LoadedSounds[configVars.SoundUpgrade];
    }

    if (configVars.SoundBuild != "")
    {
        //this.SoundBuild = assetManager.LoadedSounds[configVars.SoundBuild];
    }

    this.TowerName = configVars.TowerName;
    
    //Load Textures from AssetManager
    this.TextureCurrent = assetManager.LoadedSprites[configVars.TextureTower];
    this.TextureProjectile = assetManager.LoadedSprites[configVars.TextureProjectile];

    //Must call Initialize after assigning texture values in each Tower constructor
    this.Intialize();

    //Set Weapon Numbers
    this.CurrentWeaponDamage = configVars.CurrentWeaponDamage;
    this.CurrentWeaponAreaOfEffect = configVars.CurrentWeaponAreaOfEffect;
    this.CurrentWeaponAttacksPerSecond = configVars.CurrentWeaponAttacksPerSecond;
    this.CurrentWeaponRange = configVars.CurrentWeaponRange;
    this.DamageGainedPerLevel = configVars.DamageGainedPerLevel;
    this.AttacksPerSecondGainedPerLevel = configVars.AttacksPerSecondGainedPerLevel;
    this.WeaponRangeGainedPerLevel = configVars.WeaponRangeGainedPerLevel;
    this.ProjectileSpeed = configVars.ProjectileSpeed;
    this.is_point_blank_area_damage_tower = configVars.is_point_blank_area_damage_tower;
    


    //Set Build Numbers
    this.CostToBuild = configVars.CostToBuild;
    this.CurrentCostToUpgrade = configVars.CurrentCostToUpgrade;
    this.UpgradeCostIncreasePerLevel = configVars.UpgradeCostIncreasePerLevel;
    this.MaxTowerLevel = configVars.MaxTowerLevel;

    //Set miscellaneous 
    this.StatusEffects = configVars.StatusEffects;
    this.Scale = configVars.scale;
    this.Rotation = configVars.rotation;


}
        

/// <summary>
/// Initializes reference-type and non-configurable class properties and fields. To be called in all Tower class constructors
/// </summary>
protected virtual void Intialize()
{
    
    //Initialize non-configurable members and properties
    this.current_tower_level = 1;
    this.MaxProjectiles = 20;
    
    
    
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


/// <summary>
/// This function "levels up" the tower, incrementing all appropriate tower variables by there "GainedPerLevel" values
/// </summary>
protected virtual bool level_up_tower()
{
    if (globals.PlayerCash >= CurrentCostToUpgrade && (current_tower_level < MaxTowerLevel))
    {
        current_tower_level += 1;
        globals.PlayerCash -= CurrentCostToUpgrade;

        CurrentWeaponDamage += DamageGainedPerLevel;
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
