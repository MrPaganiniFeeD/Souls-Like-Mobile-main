using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorModelChanger : ModelChanger
{
    private const string _defaultTors = "Chr_Torso_Male_00";
    private const string _defaultArmLowerRight = "Chr_ArmLowerRight_Male_00";
    private const string _defaultArmLowerLeft = "Chr_ArmLowerLeft_Male_00";
    private const string _defaultArmUpperRight = "Chr_ArmUpperRight_Male_00";
    private const string _defaultArmUpperLeft = "Chr_ArmUpperLeft_Male_00";
    private const string _defaultRightHnad = "Chr_HandRight_Male_00";
    private const string _defaultLeftHand = "Chr_HandLeft_Male_00";
    private const string _defaultHips = "Chr_Hips_Male_00";
    
    public override void EnableDefaultModel()
    {
        DisableEquipModels();
        ActivateModelByName(_defaultTors);
        ActivateModelByName(_defaultArmLowerLeft);
        ActivateModelByName(_defaultArmLowerRight);
        ActivateModelByName(_defaultArmUpperLeft);
        ActivateModelByName(_defaultArmUpperRight);
        ActivateModelByName(_defaultRightHnad);
        ActivateModelByName(_defaultLeftHand);
        ActivateModelByName(_defaultHips);
    }
    protected override void DisableDefaultModel()
    {
        DisableModelByName(_defaultTors);
        DisableModelByName(_defaultArmLowerLeft);
        DisableModelByName(_defaultArmLowerRight);
        DisableModelByName(_defaultArmUpperLeft);
        DisableModelByName(_defaultArmUpperRight);
        DisableModelByName(_defaultRightHnad);
        DisableModelByName(_defaultLeftHand);
        DisableModelByName(_defaultHips);
    }

    private void DisableModelByName(string nameModel)
    {
        foreach (var model in _models)
            if (model.name == nameModel)
                model.SetActive(false);
    }
}
