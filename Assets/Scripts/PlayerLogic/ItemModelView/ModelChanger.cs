using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ModelChanger : MonoBehaviour
{
    [SerializeField] protected List<GameObject> _models;
    protected List<GameObject> EquippedModel = new List<GameObject>();

    public virtual void EquipModelByNames(string[] nameModels, bool isNacktModelUnload)
    {
        if(isNacktModelUnload)
            DisableDefaultModel();

        DisableEquipModels();

        foreach (var nameModel in nameModels)
        {
            EquipModelByName(nameModel);
        }
    }

    public virtual void EquipModelByName(string nameModel)
    {
        foreach (var model in _models)
        {
            if (model.name == nameModel)
            {
                EquippedModel.Add(model);
                model.SetActive(true);
            }
        }
    }

    public virtual void ActivateModelByName(string nameModel)
    {
        foreach (var model in _models)
        {
            if (model.name == nameModel)
            {
                model.SetActive(true);
            }
        }
    } 

    public virtual void DisableEquipModels()
    {
        foreach (var model in EquippedModel)
            model.SetActive(false);
    }

    protected abstract void DisableDefaultModel();

    public abstract void EnableDefaultModel();
}