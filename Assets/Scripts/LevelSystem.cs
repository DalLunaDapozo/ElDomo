
using UnityEngine;

public class LevelSystem 
{
    private int level;
    private int experience;
    private int experienceToNextlevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextlevel = 100;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if (experience >= experienceToNextlevel)
        {
            level++;
            experience -= experienceToNextlevel;
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public int GetExperience()
    {
        return experience;
    }
}
