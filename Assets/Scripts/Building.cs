using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Building : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishBtn;
    private Transform buildingRepairBtn;

    private void Awake() {
        buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");
        buildingRepairBtn = transform.Find("pfBuildingRepairBtn");

        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();
    }
    private void Start()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;

        healthSystem.OnDied += HealthSystem_OnDied;
    }
    private void Update()
    {
        
    }

    private void HealthSystem_OnHealed(object sender, System.EventArgs e){
        if (healthSystem.IsFullHealth()) {
            HideBuildingRepairBtn();
        }
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e){
        ShowBuildingRepairBtn();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        CinemachineSnake.Instance.SnakeCamera(7f, .15f);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e){
        Instantiate(Resources.Load<Transform>("pfBuildingDestroyedParticles"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        CinemachineSnake.Instance.SnakeCamera(10f, .2f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
    }

    private void OnMouseEnter() {
        ShowBuildingDemolishBtn();
    }

    private void OnMouseExit() {
        HideBuildingDemolishBtn();
    }

    private void ShowBuildingDemolishBtn() {
        if (buildingDemolishBtn != null){
            buildingDemolishBtn.gameObject.SetActive(true);
        }
    }

    private void HideBuildingDemolishBtn() {
        if (buildingDemolishBtn != null){
            buildingDemolishBtn.gameObject.SetActive(false);
        }
    }

    private void ShowBuildingRepairBtn() {
        if (buildingRepairBtn != null){
            buildingRepairBtn.gameObject.SetActive(true);
        }
    }

    private void HideBuildingRepairBtn() {
        if (buildingRepairBtn != null){
            buildingRepairBtn.gameObject.SetActive(false);
        }
    }
}
