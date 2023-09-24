using UnityEngine;

public class HamletModelChanger : ModelChanger
{
    private const string _nameDefaultHamlet = "Chr_Head_Male_03";
    private const string _nameDefaultEyebrows = "Chr_Eyebrow_Male_07";
    
    public override void EnableDefaultModel()
    {
        DisableEquipModels();
        ActivateModelByName(_nameDefaultHamlet);
        ActivateModelByName(_nameDefaultEyebrows);
    }
    protected override void DisableDefaultModel()
    {
        Debug.Log("DisableDefaultModel");
        DisableModelByName(_nameDefaultHamlet);
        DisableModelByName(_nameDefaultEyebrows);
    }

    private void DisableModelByName(string nameModel)
    {
        foreach (var model in _models)
            if (model.name == nameModel)
                model.SetActive(false);
    }
}
