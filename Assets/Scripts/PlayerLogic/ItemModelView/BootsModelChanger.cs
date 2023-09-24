using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsModelChanger : ModelChanger
{
    private const string _nameNakedBootsLeft = "Chr_LegLeft_Male_00";
    private const string _nameNakedBootsRight = "Chr_LegRight_Male_00";

    private void Start()
    {
        EnableDefaultModel();
    }
    public override void EnableDefaultModel()
    {
        DisableEquipModels();
        EquipModelByName(_nameNakedBootsLeft);
        EquipModelByName(_nameNakedBootsRight);
    }
    protected override void DisableDefaultModel()
    {
        DisableModelByName(_nameNakedBootsLeft);
        DisableModelByName(_nameNakedBootsRight);
    }

    private void DisableModelByName(string nameModel)
    {
        foreach (var model in _models)
            if (model.name == nameModel)
                    model.SetActive(false);
    }
}
