using System.ComponentModel.DataAnnotations;

namespace DataLibrary;

public class Enums
{
    public enum LanguageSelection
    {
        [Display(Name = "English / Anglais")] English,
        [Display(Name = "French / Français")] French
    }

    public enum CountrySelection
    {
        [Display(Name = "Ethiopia")] Ethiopia,
        [Display(Name = "Tunisia")] Tunisia,
    }

    public enum SpeciesSelection
    {
        [Display(Name = "Sheep")] Sheep,
        [Display(Name = "Goat")] Goat
    }

    public enum SheepTypeSelection
    {
        [Display(Name = "Ewes")] Ewes,
        [Display(Name = "Weaned lambs")] WeanedLambs,
        [Display(Name = "Rams")] Rams
    }

    public enum GoatTypeSelection
    {
        [Display(Name = "Does")] Does,
        [Display(Name = "Kids")] Kids,
        [Display(Name = "Bucks")] Bucks
    }

    public enum GrazingSelection
    {
        [Display(Name = "None")] None,
        [Display(Name = "Grazing close-by")] GrazingCloseBy,
        [Display(Name = "Open range")] OpenRange,
        [Display(Name = "Rough mountain terrain")] RoughMountainTerrain
    }

    public enum DQESelection
    {
        [Display(Name = "Low (< 6.5 MJ/kg DM)")] Low,
        [Display(Name = "Medium (~7.5 MJ/kg DM)")] Medium,
        [Display(Name = "High (>8.5 MJ/kg DM)")] High
    }

    public enum NrSucklingKidsLambsSelection
    {
        [Display(Name = "1")] One,
        [Display(Name = "2")] Two,
        [Display(Name = "3")] Three,
        [Display(Name = "4")] Four,
        [Display(Name = "5")] Five
    }

    public enum BodyWeightSelection
    {
        [Display(Name = "5")] W5,
        [Display(Name = "10")] W10,
        [Display(Name = "15")] W15,
        [Display(Name = "20")] W20,
        [Display(Name = "25")] W25,
        [Display(Name = "30")] W30,
        [Display(Name = "35")] W35,
        [Display(Name = "40")] W40,
        [Display(Name = "45")] W45,
        [Display(Name = "50")] W50,
        [Display(Name = "55")] W55,
        [Display(Name = "60")] W60,
        [Display(Name = "65")] W65,
        [Display(Name = "70")] W70,
        [Display(Name = "75")] W75,
        [Display(Name = "80")] W80,
        [Display(Name = "85")] W85,
        [Display(Name = "90")] W90
    }
}